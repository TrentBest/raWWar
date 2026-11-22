import tempfile
import unittest
from pathlib import Path
from unittest.mock import patch

from tools.write_build_observation import file_record, parse_trx, summarize_outcomes


class BuildObservationContractTests(unittest.TestCase):
    def test_all_required_steps_success_is_success(self):
        self.assertEqual(
            "success",
            summarize_outcomes({"build": "success", "contracts": "success", "examples": "success"}),
        )

    def test_any_failed_step_is_failure_even_when_other_steps_are_skipped(self):
        self.assertEqual(
            "failure",
            summarize_outcomes({"build": "failure", "contracts": "skipped", "examples": "skipped"}),
        )

    def test_cancelled_step_is_not_reported_as_success(self):
        self.assertEqual("cancelled", summarize_outcomes({"build": "success", "contracts": "cancelled"}))

    def test_skipped_or_unknown_step_is_not_reported_as_success(self):
        self.assertEqual("unknown", summarize_outcomes({"build": "success", "contracts": "skipped"}))
        self.assertEqual("unknown", summarize_outcomes({"build": "success", "contracts": "neutral"}))

    def test_empty_step_set_is_unknown(self):
        self.assertEqual("unknown", summarize_outcomes({}))

    def test_invalid_step_outcome_shape_is_rejected(self):
        with self.assertRaises(ValueError):
            summarize_outcomes({"build": None})

    def test_trx_counters_are_preserved(self):
        with tempfile.TemporaryDirectory() as directory:
            path = Path(directory) / "results.trx"
            path.write_text(
                '<TestRun><ResultSummary><Counters total="3" executed="3" passed="2" failed="1" /></ResultSummary></TestRun>',
                encoding="utf-8",
            )
            result = parse_trx(path)
            self.assertTrue(result["present"])
            self.assertTrue(result["complete"])
            self.assertEqual(3, result["total"])
            self.assertEqual(2, result["passed"])
            self.assertEqual(1, result["failed"])

    def test_missing_trx_is_explicitly_incomplete(self):
        with tempfile.TemporaryDirectory() as directory:
            result = parse_trx(Path(directory) / "missing.trx")
            self.assertFalse(result["present"])
            self.assertFalse(result["complete"])

    def test_artifact_digest_and_size_are_reported(self):
        with tempfile.TemporaryDirectory() as directory:
            path = Path(directory) / "artifact.bin"
            path.write_bytes(b"trusted bytes")
            with patch("tools.write_build_observation.ROOT", Path(directory)):
                record = file_record(path)
            self.assertEqual(len(b"trusted bytes"), record["sizeBytes"])
            self.assertEqual(
                "6acd7c3c149b0fdbc542a20bb7ece8164ebf4b78148c3f65c2fddf208cc74e35",
                record["sha256"],
            )


if __name__ == "__main__":
    unittest.main()
