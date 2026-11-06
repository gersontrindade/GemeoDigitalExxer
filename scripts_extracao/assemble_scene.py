"""Monta a cena level0 de um build Unity em um unico OBJ+MTL com as pecas posicionadas.
uso: python -I assemble_scene.py <pasta _Data> <pasta saida>"""
import os, re, sys
import UnityPy
from UnityPy.helpers.MeshHelper import MeshHandler

data_dir, out = sys.argv[1], sys.argv[2]
os.makedirs(os.path.join(out, "textures"), exist_ok=True)
env = UnityPy.load(data_dir)


def deref(ptr):
    if ptr is None or getattr(ptr, "m_PathID", 0) == 0:
        return None
    try:
        return ptr.read()
    except Exception:
        try:
            return ptr.deref().read()
        except Exception:
            return None


def safe(n):
    return re.sub(r"[^\w\-.]", "_", n or "sem_nome")


# --- matrizes 4x4 (linha-maior) ---
def trs(p, q, s):
    x, y, z, w = q.x, q.y, q.z, q.w
    r = [[1 - 2 * (y * y + z * z), 2 * (x * y - z * w), 2 * (x * z + y * w)],
         [2 * (x * y + z * w), 1 - 2 * (x * x + z * z), 2 * (y * z - x * w)],
         [2 * (x * z - y * w), 2 * (y * z + x * w), 1 - 2 * (x * x + y * y)]]
    sc = (s.x, s.y, s.z)
    return [[r[i][0] * sc[0], r[i][1] * sc[1], r[i][2] * sc[2], (p.x, p.y, p.z)[i]] for i in range(3)] + [[0, 0, 0, 1]]


def mul(a, b):
    return [[sum(a[i][k] * b[k][j] for k in range(4)) for j in range(4)] for i in range(4)]


def normal_matrix(m):
    a = [row[:3] for row in m[:3]]
    det = (a[0][0] * (a[1][1] * a[2][2] - a[1][2] * a[2][1]) - a[0][1] * (a[1][0] * a[2][2] - a[1][2] * a[2][0])
           + a[0][2] * (a[1][0] * a[2][1] - a[1][1] * a[2][0]))
    if abs(det) < 1e-12:
        return a, det
    inv = [[(a[(j + 1) % 3][(i + 1) % 3] * a[(j + 2) % 3][(i + 2) % 3] - a[(j + 1) % 3][(i + 2) % 3] * a[(j + 2) % 3][(i + 1) % 3]) / det
            for j in range(3)] for i in range(3)]
    return [[inv[j][i] for j in range(3)] for i in range(3)], det  # transposta da inversa


# --- hierarquia ---
transforms = {}
for o in env.objects:
    if o.type.name in ("Transform", "RectTransform") and o.assets_file.name == "level0":
        transforms[o.path_id] = o.read()

world_cache = {}


def world(tid):
    if tid in world_cache:
        return world_cache[tid]
    t = transforms[tid]
    m = trs(t.m_LocalPosition, t.m_LocalRotation, t.m_LocalScale)
    fid = t.m_Father.m_PathID
    if fid and fid in transforms:
        m = mul(world(fid), m)
    world_cache[tid] = m
    return m


def active_in_hierarchy(tid):
    while tid and tid in transforms:
        go = deref(transforms[tid].m_GameObject)
        if go is None or not go.m_IsActive:
            return False
        tid = transforms[tid].m_Father.m_PathID
    return True


go_to_transform = {}
for tid, t in transforms.items():
    go_to_transform[t.m_GameObject.m_PathID] = tid

# --- materiais ---
materials = {}  # path_id -> nome
mtl_lines = []
saved_tex = {}


def props(lst):
    if isinstance(lst, dict):
        return lst
    return {k: v for k, v in lst}


def material_name(ptr):
    pid = getattr(ptr, "m_PathID", 0)
    if pid in materials:
        return materials[pid]
    mat = deref(ptr)
    name = safe(mat.m_Name if mat else "padrao") + f"_{abs(pid) % 100000}"
    materials[pid] = name
    mtl_lines.append(f"newmtl {name}")
    color, tex = (0.8, 0.8, 0.8, 1.0), None
    if mat is not None:
        sp = mat.m_SavedProperties
        cols = props(sp.m_Colors)
        c = cols.get("_Color") or cols.get("_BaseColor")
        if c is not None:
            color = (c.r, c.g, c.b, c.a)
        envs = props(sp.m_TexEnvs)
        te = envs.get("_MainTex") or envs.get("_BaseMap")
        if te is not None:
            tobj = deref(te.m_Texture)
            if tobj is not None:
                tid = te.m_Texture.m_PathID
                if tid not in saved_tex:
                    fn = f"{safe(tobj.m_Name)}_{abs(tid) % 100000}.png"
                    try:
                        tobj.image.save(os.path.join(out, "textures", fn))
                        saved_tex[tid] = fn
                    except Exception:
                        saved_tex[tid] = None
                tex = saved_tex[tid]
    mtl_lines.append(f"Kd {color[0]:.4f} {color[1]:.4f} {color[2]:.4f}")
    if color[3] < 0.999:
        mtl_lines.append(f"d {color[3]:.4f}")
    if tex:
        mtl_lines.append(f"map_Kd textures/{tex}")
    mtl_lines.append("")
    return name


# --- exportacao ---
obj_path = os.path.join(out, "cena_smart40.obj")
vbase = tbase = nbase = 1
n_obj = n_skip_inactive = n_fail = 0
with open(obj_path, "w") as f:
    f.write("mtllib cena_smart40.mtl\n")
    for o in env.objects:
        if o.type.name != "MeshFilter" or o.assets_file.name != "level0":
            continue
        try:
            mf = o.read()
            go_id = mf.m_GameObject.m_PathID
            tid = go_to_transform.get(go_id)
            if tid is None:
                continue
            if not active_in_hierarchy(tid):
                n_skip_inactive += 1
                continue
            mesh = deref(mf.m_Mesh)
            if mesh is None:
                continue
            go = deref(mf.m_GameObject)
            renderer = None
            for comp in go.m_Component:
                cptr = comp.component if hasattr(comp, "component") else comp[1] if isinstance(comp, tuple) else comp
                c = deref(cptr)
                if c is not None and type(c).__name__ == "MeshRenderer":
                    renderer = c
                    break
            if renderer is not None and not renderer.m_Enabled:
                continue
            h = MeshHandler(mesh)
            h.process()
            verts = h.m_Vertices
            if not verts:
                continue
            M = world(tid)
            N, det = normal_matrix(M)
            flip = det < 0
            buf = []  # so grava se o objeto inteiro der certo, para nao dessincronizar os indices
            w = buf.append
            w(f"o {safe(go.m_Name)}_{abs(go_id) % 100000}\n")
            for v in verts:
                vx, vy, vz = v[0], v[1], v[2]
                x = M[0][0] * vx + M[0][1] * vy + M[0][2] * vz + M[0][3]
                y = M[1][0] * vx + M[1][1] * vy + M[1][2] * vz + M[1][3]
                z = M[2][0] * vx + M[2][1] * vy + M[2][2] * vz + M[2][3]
                w(f"v {-x:.5f} {y:.5f} {z:.5f}\n")  # Unity (mao esquerda) -> OBJ: inverte X
            uvs = h.m_UV0
            if uvs:
                for uv in uvs:
                    w(f"vt {uv[0]:.5f} {uv[1]:.5f}\n")
            norms = h.m_Normals
            if norms:
                for nv in norms:
                    nx, ny, nz = nv[0], nv[1], nv[2]
                    a = N[0][0] * nx + N[0][1] * ny + N[0][2] * nz
                    b = N[1][0] * nx + N[1][1] * ny + N[1][2] * nz
                    c = N[2][0] * nx + N[2][1] * ny + N[2][2] * nz
                    ln = (a * a + b * b + c * c) ** 0.5 or 1.0
                    w(f"vn {-a / ln:.4f} {b / ln:.4f} {c / ln:.4f}\n")
            mats = renderer.m_Materials if renderer is not None else []
            for si, tris in enumerate(h.get_triangles()):
                w(f"usemtl {material_name(mats[min(si, len(mats) - 1)] if mats else None)}\n")
                for tri in tris:
                    # inverter X inverte o sentido das faces
                    i0, i1, i2 = (tri[0], tri[1], tri[2]) if flip else (tri[0], tri[2], tri[1])
                    def ref(i):
                        s = str(vbase + i)
                        if uvs and norms:
                            return f"{s}/{tbase + i}/{nbase + i}"
                        if uvs:
                            return f"{s}/{tbase + i}"
                        if norms:
                            return f"{s}//{nbase + i}"
                        return s
                    w(f"f {ref(i0)} {ref(i1)} {ref(i2)}\n")
            f.write("".join(buf))
            vbase += len(verts)
            tbase += len(uvs) if uvs else 0
            nbase += len(norms) if norms else 0
            n_obj += 1
        except Exception as e:
            n_fail += 1
            print("falha:", o.path_id, repr(e)[:120])

with open(os.path.join(out, "cena_smart40.mtl"), "w") as f:
    f.write("\n".join(mtl_lines))
print(f"objetos exportados: {n_obj}  inativos ignorados: {n_skip_inactive}  falhas: {n_fail}")
print(f"vertices: {vbase - 1}  materiais: {len(materials)}  texturas: {sum(1 for v in saved_tex.values() if v)}")
