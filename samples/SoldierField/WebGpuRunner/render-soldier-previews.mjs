import { mkdir, writeFile } from "node:fs/promises";
import { dirname, resolve } from "node:path";
import { buildSoldierGeometry, SOLDIER_GEOMETRY_LAYOUT } from "./soldier-geometry.js";

const outputRoot = resolve("docs/images/soldier-field");
const views = [
  { file: "01-front.svg", title: "01 · Front silhouette", yaw: 0, zoom: 1, focus: [0, .95, 0], desc: "Front view of the procedural mesh generated from Soldier Field's 38 semantic parts." },
  { file: "02-three-quarter.svg", title: "02 · Three-quarter view", yaw: .62, zoom: 1, focus: [0, .95, 0], desc: "Three-quarter view of the procedural exoskeleton mesh used by the WebGPU lab." },
  { file: "03-profile.svg", title: "03 · Profile", yaw: 1.48, zoom: 1, focus: [0, .95, 0], desc: "Side profile of the exoskeleton, including life-support pack and held tool." },
  { file: "04-rear.svg", title: "04 · Rear assembly", yaw: Math.PI, zoom: 1, focus: [0, .95, 0], desc: "Rear view of the same procedural mesh, including life-support and heat exchanger parts." },
  { file: "05-armor-detail.svg", title: "05 · Armor and shunt detail", yaw: .35, zoom: 1.65, focus: [0, 1.42, 0], desc: "Close-up of the helmet, torso plate, shoulder hardpoints, and provisional neural-shunt endpoints." },
  { file: "06-field-formation.svg", title: "06 · Field formation", yaw: .48, zoom: .75, focus: [0, .95, 0], desc: "Repeated instances of the same procedural soldier mesh, previewing the scaling lab's population goal.", formation: true }
];

const geometry = buildSoldierGeometry();
const hex = values => "#" + values.map(value => Math.round(Math.max(0, Math.min(1, value)) * 255).toString(16).padStart(2, "0")).join("");

function render(view) {
  const width = 720, height = 760, centerX = 360, centerY = height * .78;
  const scale = 205 * view.zoom, cos = Math.cos(view.yaw), sin = Math.sin(view.yaw);
  const offsets = view.formation ? [-2.1, -1.4, -.7, 0, .7, 1.4, 2.1] : [0];
  const triangles = [];
  for (const offset of offsets) {
    for (let i = 0; i < geometry.length; i += 18) {
      const points = [];
      for (let v = 0; v < 3; v++) {
        const j = i + v * 6;
        const x = geometry[j] + offset - view.focus[0];
        const y = geometry[j + 1] - view.focus[1];
        const z = geometry[j + 2] - view.focus[2];
        points.push({
          x: centerX + (x * cos + z * sin) * scale,
          y: centerY - y * scale,
          z: -x * sin + z * cos
        });
      }
      const [a, b, c] = points;
      const ax = b.x - a.x, ay = b.y - a.y, az = b.z - a.z;
      const bx = c.x - a.x, by = c.y - a.y, bz = c.z - a.z;
      let nx = ay * bz - az * by, ny = az * bx - ax * bz, nz = ax * by - ay * bx;
      const length = Math.hypot(nx, ny, nz) || 1;
      nx /= length; ny /= length; nz /= length;
      const shade = .3 + .7 * Math.max(0, nx * -.35 + ny * .7 + nz * .62);
      const colorOffset = i + 3;
      const color = [geometry[colorOffset], geometry[colorOffset + 1], geometry[colorOffset + 2]].map(value => Math.min(1, value * shade));
      triangles.push({ points, depth: Math.round((points.reduce((sum, point) => sum + point.z, 0) / 3) * 1e6) / 1e6, order: triangles.length, color: hex(color) });
    }
  }
  triangles.sort((a, b) => a.depth - b.depth || a.order - b.order);
  const polygons = triangles.map(triangle =>
    `<polygon points="${triangle.points.map(point => `${point.x.toFixed(2)},${point.y.toFixed(2)}`).join(" ")}" fill="${triangle.color}" stroke="#b9d4df" stroke-opacity=".08" stroke-width=".5"/>`
  ).join("");
  const shadowRx = view.formation ? 300 : 95 * view.zoom;
  const shadowRy = view.formation ? 28 : 22 * view.zoom;
  return `<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 720 760" role="img" aria-labelledby="title desc">
<title id="title">raWWar Soldier Field — ${view.title}</title><desc id="desc">${view.desc}</desc>
<defs><linearGradient id="bg" x2="0" y2="1"><stop stop-color="#101b2a"/><stop offset="1" stop-color="#060a10"/></linearGradient><radialGradient id="halo"><stop stop-color="#20495b" stop-opacity=".7"/><stop offset="1" stop-color="#07101a" stop-opacity="0"/></radialGradient></defs>
<rect width="720" height="760" fill="url(#bg)"/><ellipse cx="360" cy="330" rx="280" ry="350" fill="url(#halo)"/><path d="M0 664H720" stroke="#496275" stroke-width="2"/><ellipse cx="360" cy="671" rx="${shadowRx}" ry="${shadowRy}" fill="#000" opacity=".58"/><g>${polygons}</g><path d="M32 36H688V724H32Z" fill="none" stroke="#395064" stroke-opacity=".65"/>
<text x="48" y="68" fill="#e5f0f8" font-family="sans-serif" font-size="20" font-weight="700" letter-spacing="1.5">${view.title}</text>
<text x="48" y="96" fill="#8fa9bb" font-family="monospace" font-size="12">ASSET ${SOLDIER_GEOMETRY_LAYOUT.partCount} semantic parts · ${SOLDIER_GEOMETRY_LAYOUT.nominalHeightMetres} m nominal height</text>
<text x="48" y="704" fill="#7de5dc" font-family="monospace" font-size="12">PROCEDURAL MESH PREVIEW · SILHOUETTE BLOCKOUT</text></svg>`;
}

for (const view of views) {
  const path = resolve(outputRoot, view.file);
  await mkdir(dirname(path), { recursive: true });
  await writeFile(path, render(view), "utf8");
}
console.log(`Rendered ${views.length} SVG previews from ${SOLDIER_GEOMETRY_LAYOUT.partCount} semantic parts.`);
