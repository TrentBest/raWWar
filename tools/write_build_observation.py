#!/usr/bin/env python3
"""Create a provenance-bound, read-only raWWar build observation bundle for CiAi."""

from __future__ import annotations

import hashlib
import json
import os
import shutil
import subprocess
import urllib.error
import urllib.parse
import urllib.request
import xml.etree.ElementTree as ET
from pathlib import Path


ROOT = Path.cwd()
BUNDLE = ROOT / "TestResults" / "observation-bundle"


def env(name: str, default: str = "") -> str:
    return os.environ.get(name, default)


def git_value(*args: str) -> str:
    result = subprocess.run(
        ["git", *args], check=True, capture_output=True, text=True
    )
    return result.stdout.strip()


def digest(path: Path) -> str:
    value = hashlib.sha256()
    with path.open("rb") as stream:
        for chunk in iter(lambda: stream.read(1024 * 1024), b""):
            value.update(chunk)
    return value.hexdigest()


def file_record(path: Path) -> dict[str, object]:
    return {
        "path": path.relative_to(ROOT).as_posix(),
        "sizeBytes": path.stat().st_size,
        "sha256": digest(path),
    }


def parse_trx(path: Path) -> dict[str, object]:
    if not path.is_file():
        return {"present": False, "complete": False}
    try:
        root = ET.parse(path).getroot()
        counters = next(
            (node for node in root.iter() if node.tag.rsplit("}", 1)[-1] == "Counters"),
            None,
        )
        if counters is None:
            return {"present": True, "complete": False, "reason": "counters-missing"}
        values = {key: int(value) for key, value in counters.attrib.items()}
        return {
            "present": True,
            "complete": True,
            "total": values.get("total", 0),
            "passed": values.get("passed", 0),
            "failed": values.get("failed", 0),
            "notExecuted": values.get("notExecuted", 0),
            "counters": values,
        }
    except (ET.ParseError, OSError, ValueError) as error:
        return {"present": True, "complete": False, "reason": type(error).__name__}


def pull_request_head_sha() -> str:
    event_path = env("GITHUB_EVENT_PATH")
    if event_path:
        try:
            event = json.loads(Path(event_path).read_text(encoding="utf-8"))
            pull_request = event.get("pull_request")
            if isinstance(pull_request, dict):
                head = pull_request.get("head")
                if isinstance(head, dict) and isinstance(head.get("sha"), str):
                    return head["sha"]
        except (OSError, json.JSONDecodeError):
            pass
    return env("GITHUB_SHA")


def observe_badge(badge: dict[str, object], source_sha: str) -> dict[str, object]:
    workflow_file = str(badge.get("workflowFile", ""))
    result: dict[str, object] = {
        "id": badge.get("id"),
        "label": badge.get("label"),
        "workflowFile": workflow_file,
        "sourceSha": source_sha,
        "status": "unknown",
        "conclusion": None,
        "runId": None,
        "updatedAt": None,
    }
    token = env("GH_TOKEN")
    repository = env("GITHUB_REPOSITORY")
    if not token or not repository or not workflow_file:
        result["reason"] = "workflow-observation-context-unavailable"
        return result

    workflow_id = urllib.parse.quote(Path(workflow_file).name, safe="")
    query = urllib.parse.urlencode({"head_sha": source_sha, "per_page": 20})
    api = env("GITHUB_API_URL", "https://api.github.com").rstrip("/")
    url = f"{api}/repos/{repository}/actions/workflows/{workflow_id}/runs?{query}"
    request = urllib.request.Request(
        url,
        headers={
            "Accept": "application/vnd.github+json",
            "Authorization": f"Bearer {token}",
            "User-Agent": "raWWar-CiAi-BuildObserver/1.0",
            "X-GitHub-Api-Version": "2022-11-28",
        },
    )
    try:
        with urllib.request.urlopen(request, timeout=15) as response:
            payload = json.loads(response.read().decode("utf-8"))
        runs = payload.get("workflow_runs", [])
        matches = [
            run for run in runs
            if isinstance(run, dict)
            and str(run.get("head_sha", "")).lower() == source_sha.lower()
            and Path(str(run.get("path", "")).split("@", 1)[0]).name == Path(workflow_file).name
        ]
        if not matches:
            result["status"] = "not-found"
            result["reason"] = "no-run-for-exact-source-sha"
            return result
        run = max(matches, key=lambda item: str(item.get("updated_at", "")))
        result.update({
            "status": run.get("status", "unknown"),
            "conclusion": run.get("conclusion"),
            "runId": run.get("id"),
            "updatedAt": run.get("updated_at"),
            "url": run.get("html_url"),
        })
        return result
    except (urllib.error.URLError, TimeoutError, json.JSONDecodeError, OSError) as error:
        result["status"] = "unavailable"
        result["reason"] = type(error).__name__
        return result


def main() -> int:
    outcomes = json.loads(env("RAWWAR_STEP_OUTCOMES_JSON", "{}"))
    if not isinstance(outcomes, dict) or any(
        not isinstance(key, str) or not isinstance(value, str)
        for key, value in outcomes.items()
    ):
        raise ValueError("RAWWAR_STEP_OUTCOMES_JSON must be an object of string outcomes.")

    values = list(outcomes.values())
    if not values or any(value == "failure" for value in values):
        conclusion = "failure" if "failure" in values else "unknown"
    elif any(value == "cancelled" for value in values):
        conclusion = "cancelled"
    elif all(value == "success" for value in values):
        conclusion = "success"
    else:
        conclusion = "unknown"

    provenance_path = ROOT / "TestResults" / "provenance" / "workflow-provenance.json"
    provenance = {}
    if provenance_path.is_file():
        try:
            provenance = json.loads(provenance_path.read_text(encoding="utf-8"))
        except json.JSONDecodeError:
            provenance = {"parseError": True}

    repository_manifest_path = ROOT / ".ciai" / "repository.json"
    repository_manifest = {}
    if repository_manifest_path.is_file():
        repository_manifest = json.loads(repository_manifest_path.read_text(encoding="utf-8"))

    source_sha = pull_request_head_sha()
    badges = repository_manifest.get("badges", [])
    if not isinstance(badges, list):
        badges = []
    badge_observations = [
        observe_badge(item, source_sha)
        for item in badges if isinstance(item, dict)
    ]

    tracked_tree = git_value("ls-tree", "-r", "--full-tree", "HEAD")
    tree_sha = git_value("rev-parse", "HEAD^{tree}")
    snapshot = {
        "commitSha": git_value("rev-parse", "HEAD"),
        "treeSha": tree_sha,
        "trackedFileCount": sum(1 for line in tracked_tree.splitlines() if line),
        "trackedTreeListingSha256": hashlib.sha256(tracked_tree.encode("utf-8")).hexdigest(),
        "repositoryManifestPath": ".ciai/repository.json",
        "repositoryManifestSha256": digest(repository_manifest_path) if repository_manifest_path.is_file() else None,
    }

    candidates = [
        ROOT / ".ciai" / "repository.json",
        ROOT / "TestResults" / "results.trx",
        provenance_path,
        ROOT / "TestResults" / "evidence" / "results.trx",
        ROOT / "TestResults" / "evidence" / "provenance.json",
        ROOT / "assets" / "sith-policy.json",
        ROOT / "assets" / "sith-policy.svg",
    ]
    candidates.extend(sorted((ROOT / "docs" / "images" / "soldier-field").glob("*.svg")))
    candidates.extend(sorted((ROOT / "artifacts" / "packages").glob("*.nupkg")))

    existing = []
    seen = set()
    for path in candidates:
        if path.is_file() and path.resolve() not in seen:
            existing.append(path)
            seen.add(path.resolve())

    trx = parse_trx(ROOT / "TestResults" / "results.trx")
    observation = {
        "schema": "ciai.build-observation.v1",
        "repository": env("GITHUB_REPOSITORY"),
        "workflow": {
            "name": env("GITHUB_WORKFLOW"),
            "ref": env("GITHUB_WORKFLOW_REF"),
            "sha": env("GITHUB_WORKFLOW_SHA"),
            "runId": int(env("GITHUB_RUN_ID", "0") or "0"),
            "runAttempt": int(env("GITHUB_RUN_ATTEMPT", "0") or "0"),
            "url": f"https://github.com/{env('GITHUB_REPOSITORY')}/actions/runs/{env('GITHUB_RUN_ID')}",
        },
        "event": {
            "name": env("GITHUB_EVENT_NAME"),
            "ref": env("GITHUB_REF"),
            "sha": env("GITHUB_SHA"),
            "sourceSha": source_sha,
        },
        "checkout": {
            "ref": env("GITHUB_REF"),
            "sha": snapshot["commitSha"],
        },
        "outcome": conclusion,
        "stepOutcomes": outcomes,
        "tests": trx,
        "repositorySnapshot": snapshot,
        "badgeObservations": badge_observations,
        "artifacts": [file_record(path) for path in existing],
        "provenance": provenance,
        "authority": {
            "ciVerdictOwner": "CiAi",
            "llmOwner": "HeadlessAi",
            "thisArtifactAuthorizesWrites": False,
            "thisArtifactAuthorizesMergeOrPublication": False,
        },
    }

    if BUNDLE.exists():
        shutil.rmtree(BUNDLE)
    BUNDLE.mkdir(parents=True)
    for path in existing:
        destination = BUNDLE / path.relative_to(ROOT)
        destination.parent.mkdir(parents=True, exist_ok=True)
        shutil.copy2(path, destination)
    (BUNDLE / "observation.json").write_text(
        json.dumps(observation, indent=2, sort_keys=True) + "\n",
        encoding="utf-8",
    )
    print(json.dumps({
        "schema": observation["schema"],
        "outcome": conclusion,
        "trackedFileCount": snapshot["trackedFileCount"],
        "artifactCount": len(existing),
        "badgeCount": len(badge_observations),
        "bundle": str(BUNDLE.relative_to(ROOT)),
    }, sort_keys=True))
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
