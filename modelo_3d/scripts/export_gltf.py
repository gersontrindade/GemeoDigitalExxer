"""Exporta a cena level0 do build Unity "Smart 4.0 (Senai)" para glTF binario (.glb),
preservando hierarquia, pivos (TRS locais), nomes, materiais PBR e texturas.

uso: python -I export_gltf.py <pasta _Data> <saida.glb> <grupo> [escala_m_por_unidade]
  grupo = planta | cenario

Conversao de eixos: Unity (mao esquerda, Y para cima) -> glTF (mao direita, Y para cima)
invertendo Z: pos (x,y,-z), quat (-x,-y,z,w), vertices/normais (x,y,-z), ordem dos triangulos invertida.
"""
import io, json, re, struct, sys
import numpy as np
import UnityPy
from PIL import Image
from UnityPy.helpers.MeshHelper import MeshHandler

DATA, OUT, GROUP = sys.argv[1], sys.argv[2], sys.argv[3]
SCALE = float(sys.argv[4]) if len(sys.argv) > 4 else 0.022
MAX_TEX = 2048

env = UnityPy.load(DATA)

# ---------------------------------------------------------------- cena
T, GO_COMPS = {}, {}
for o in env.objects:
    if o.assets_file.name != "level0":
        continue
    if o.type.name in ("Transform", "RectTransform"):
        T[o.path_id] = (o.read(), o.type.name)

children = {}
roots = []
for tid, (t, _) in T.items():
    f = t.m_Father.m_PathID
    if f in T:
        children.setdefault(f, []).append(tid)
    else:
        roots.append(tid)
# ordem dos filhos como no Unity
for tid, (t, _) in T.items():
    order = [c.m_PathID for c in t.m_Children]
    if tid in children:
        children[tid].sort(key=lambda c: order.index(c) if c in order else 1e9)


def comps(go):
    out = {}
    for c in go.m_Component:
        p = c.component if hasattr(c, "component") else c
        try:
            obj = p.deref() if hasattr(p, "deref") else None
            tn = obj.type.name if obj is not None else None
        except Exception:
            tn = None
        if tn is None:
            try:
                r = p.read(); tn = type(r).__name__
            except Exception:
                continue
        out.setdefault(tn, p)
    return out


SCENERY_ROOTS = {"CENARIO"}
SKIP_ROOTS = {"ETC", "COMUNICACAO", "(ADMIN)", "CAMERA (VISUAL)", "CAMERA_POS_BANCADAS", "SOMBRA_BANCADAS"}
SKIP_NAME = re.compile(r"COLISAO|TRIGGER|GLOW|SOMBRA|sombra|^TEXTO|VERS|MEDIDOR|referencia|cidade_1", re.I)
# no cenario as sombras/planos fazem parte do visual, entao filtramos menos
SKIP_NAME_CEN = re.compile(r"COLISAO|TRIGGER|GLOW|^TEXTO|referencia", re.I)

# ---------------------------------------------------------------- buffers glTF
bin_chunks = []
bin_len = 0
gl = {"asset": {"version": "2.0", "generator": "smart40 unity->gltf (UnityPy)"},
      "scene": 0, "scenes": [{"nodes": []}], "nodes": [], "meshes": [], "materials": [],
      "accessors": [], "bufferViews": [], "buffers": [], "textures": [], "images": [], "samplers": [
          {"magFilter": 9729, "minFilter": 9987, "wrapS": 10497, "wrapT": 10497}],
      "extensionsUsed": []}


def use_ext(name):
    if name not in gl["extensionsUsed"]:
        gl["extensionsUsed"].append(name)


def add_view(data: bytes, target=None):
    global bin_len
    pad = (-bin_len) % 4
    if pad:
        bin_chunks.append(b"\0" * pad); bin_len += pad
    bv = {"buffer": 0, "byteOffset": bin_len, "byteLength": len(data)}
    if target:
        bv["target"] = target
    bin_chunks.append(data); bin_len += len(data)
    gl["bufferViews"].append(bv)
    return len(gl["bufferViews"]) - 1


def add_accessor(arr, ctype, typ, target, minmax=False):
    arr = np.ascontiguousarray(arr)
    bv = add_view(arr.tobytes(), target)
    acc = {"bufferView": bv, "componentType": ctype, "count": int(arr.shape[0]), "type": typ}
    if minmax:
        acc["min"] = np.atleast_1d(arr.min(0)).tolist(); acc["max"] = np.atleast_1d(arr.max(0)).tolist()
    gl["accessors"].append(acc)
    return len(gl["accessors"]) - 1


# ---------------------------------------------------------------- texturas
tex_cache = {}


def texture_index(ptr, need_alpha):
    pid = (ptr.m_FileID, ptr.m_PathID)
    if pid in tex_cache:
        return tex_cache[pid]
    try:
        tx = ptr.read()
        img = tx.image
    except Exception:
        tex_cache[pid] = None
        return None
    if max(img.size) > MAX_TEX:
        r = MAX_TEX / max(img.size)
        img = img.resize((max(1, int(img.width * r)), max(1, int(img.height * r))), Image.LANCZOS)
    has_alpha = img.mode in ("RGBA", "LA") and img.getchannel("A").getextrema()[0] < 250
    buf = io.BytesIO()
    if has_alpha or need_alpha:
        img.convert("RGBA").save(buf, "PNG", optimize=True); mime = "image/png"
    else:
        img.convert("RGB").save(buf, "JPEG", quality=90); mime = "image/jpeg"
    bv = add_view(buf.getvalue())
    gl["images"].append({"name": tx.m_Name, "bufferView": bv, "mimeType": mime})
    gl["textures"].append({"sampler": 0, "source": len(gl["images"]) - 1, "name": tx.m_Name})
    tex_cache[pid] = len(gl["textures"]) - 1
    return tex_cache[pid]


# ---------------------------------------------------------------- materiais
mat_cache = {}
INVISIBLE = object()


def srgb_to_lin(c):
    c = max(0.0, min(1.0, c))
    return c / 12.92 if c <= 0.04045 else ((c + 0.055) / 1.055) ** 2.4


def material_index(ptr):
    pid = (ptr.m_FileID, ptr.m_PathID)
    if pid in mat_cache:
        return mat_cache[pid]
    try:
        m = ptr.read()
    except Exception:
        mat_cache[pid] = None
        return None
    try:
        shader = m.m_Shader.read().m_ParsedForm.m_Name
    except Exception:
        shader = "?"
    sp = m.m_SavedProperties
    cols = dict(sp.m_Colors); fl = dict(sp.m_Floats); te = dict(sp.m_TexEnvs)
    kw = getattr(m, "m_ShaderKeywords", "") or ""
    name = m.m_Name
    c = cols.get("_Color") or cols.get("_TintColor")
    col = [c.r, c.g, c.b, c.a] if c else [1, 1, 1, 1]
    mode = fl.get("_Mode", 0.0)
    if (("COLISAO" in name.upper() or "TRIGGER" in name.upper()) or
            (mode >= 2 and col[3] < 0.02) or shader.startswith(("Legacy Shaders/Particles", "Particles/", "GUI/", "TextMeshPro", "Hidden/"))):
        mat_cache[pid] = INVISIBLE
        return INVISIBLE

    def tex(slot, alpha=False):
        t = te.get(slot)
        if t is None or t.m_Texture.m_PathID == 0:
            return None, None
        idx = texture_index(t.m_Texture, alpha)
        if idx is None:
            return None, None
        info = {"index": idx}
        sx, sy = t.m_Scale.x, t.m_Scale.y; ox, oy = t.m_Offset.x, t.m_Offset.y
        if abs(sx - 1) > 1e-4 or abs(sy - 1) > 1e-4 or abs(ox) > 1e-4 or abs(oy) > 1e-4:
            # Unity: uv' = uv*scale + offset (origem embaixo); glTF tem V invertido
            info["extensions"] = {"KHR_texture_transform": {"scale": [sx, sy], "offset": [ox, 1 - sy - oy]}}
            use_ext("KHR_texture_transform")
        return idx, info

    blend = mode >= 2 or shader.startswith("Unlit/Transparent")
    pbr = {"baseColorFactor": [srgb_to_lin(col[0]), srgb_to_lin(col[1]), srgb_to_lin(col[2]), col[3]]}
    gloss = fl.get("_Glossiness", fl.get("_GlossMapScale", 0.5))
    if "Specular" in shader:
        sc = cols.get("_SpecColor")
        lum = (sc.r + sc.g + sc.b) / 3 if sc else 0.2
        metal = 0.85 if lum > 0.5 else 0.15
    else:
        metal = fl.get("_Metallic", 0.0)
    pbr["metallicFactor"] = float(max(0.0, min(1.0, metal)))
    pbr["roughnessFactor"] = float(max(0.05, min(1.0, 1.0 - gloss)))
    _, base = tex("_MainTex", blend or mode == 1)
    if base:
        pbr["baseColorTexture"] = base
    mat = {"name": name, "pbrMetallicRoughness": pbr}
    if blend:
        mat["alphaMode"] = "BLEND"
    elif mode == 1:
        mat["alphaMode"] = "MASK"; mat["alphaCutoff"] = float(fl.get("_Cutoff", 0.5))
    if "_EMISSION" in kw:
        ec = cols.get("_EmissionColor")
        e = [ec.r, ec.g, ec.b] if ec else [0, 0, 0]
        _, emi = tex("_EmissionMap")
        if emi and max(e) < 0.01:
            e = [1, 1, 1]
        if max(e) > 0.01:
            peak = max(e)
            mat["emissiveFactor"] = [srgb_to_lin(min(1, v / peak)) for v in e] if peak > 1 else [srgb_to_lin(v) for v in e]
            if peak > 1:
                mat.setdefault("extensions", {})["KHR_materials_emissive_strength"] = {"emissiveStrength": float(peak)}
                use_ext("KHR_materials_emissive_strength")
            if emi:
                mat["emissiveTexture"] = emi
    if shader.startswith("Unlit/"):
        mat.setdefault("extensions", {})["KHR_materials_unlit"] = {}
        use_ext("KHR_materials_unlit")
    if blend or "Cull" in shader:
        mat["doubleSided"] = True
    mat["extras"] = {"unityShader": shader}
    gl["materials"].append(mat)
    mat_cache[pid] = len(gl["materials"]) - 1
    return mat_cache[pid]


# ---------------------------------------------------------------- malhas
mesh_cache = {}
stats = {"verts": 0, "tris": 0, "meshes": 0}


def mesh_index(mesh_ptr, mat_ptrs):
    mats = [material_index(p) if p.m_PathID else None for p in mat_ptrs]
    key = (mesh_ptr.m_FileID, mesh_ptr.m_PathID, tuple(id(m) if m is INVISIBLE else m for m in mats))
    if key in mesh_cache:
        return mesh_cache[key]
    mesh = mesh_ptr.read()
    h = MeshHandler(mesh)
    h.process()
    if not h.m_Vertices:
        mesh_cache[key] = None
        return None
    V = np.array(h.m_Vertices, dtype=np.float32)[:, :3]
    V[:, 2] *= -1
    attrs = {"POSITION": None}
    N = np.array(h.m_Normals, dtype=np.float32)[:, :3] if h.m_Normals else None
    UV = np.array(h.m_UV0, dtype=np.float32)[:, :2] if h.m_UV0 else None
    prims = []
    tris_all = h.get_triangles()
    used = np.zeros(len(V), bool)
    sub = []
    for si, tris in enumerate(tris_all):
        mi = mats[min(si, len(mats) - 1)] if mats else None
        if mi is INVISIBLE or not len(tris):
            continue
        idx = np.array(tris, dtype=np.uint32).reshape(-1, 3)[:, [0, 2, 1]]
        used[idx.ravel()] = True
        sub.append((idx, mi))
    if not sub:
        mesh_cache[key] = None
        return None
    # compacta vertices nao usados (submeshes invisiveis)
    remap = np.cumsum(used) - 1
    V = V[used]
    if N is not None:
        N = N[used]; N[:, 2] *= -1
        ln = np.linalg.norm(N, axis=1, keepdims=True); ln[ln == 0] = 1; N = N / ln
    if UV is not None:
        UV = UV[used].copy(); UV[:, 1] = 1.0 - UV[:, 1]
    attrs["POSITION"] = add_accessor(V, 5126, "VEC3", 34962, True)
    if N is not None:
        attrs["NORMAL"] = add_accessor(N.astype(np.float32), 5126, "VEC3", 34962)
    if UV is not None:
        attrs["TEXCOORD_0"] = add_accessor(UV.astype(np.float32), 5126, "VEC2", 34962)
    big = len(V) > 65535
    for idx, mi in sub:
        ii = remap[idx].astype(np.uint32 if big else np.uint16).ravel()
        p = {"attributes": attrs, "indices": add_accessor(ii, 5125 if big else 5123, "SCALAR", 34963), "mode": 4}
        if mi is not None:
            p["material"] = mi
        prims.append(p)
        stats["tris"] += len(idx)
    gl["meshes"].append({"name": mesh.m_Name, "primitives": prims})
    stats["verts"] += len(V); stats["meshes"] += 1
    mesh_cache[key] = len(gl["meshes"]) - 1
    return mesh_cache[key]


# ---------------------------------------------------------------- nos
name_count = {}
node_of = {}  # unity transform path_id -> indice do no glTF
anim_owners = []
ANCHOR_RE = re.compile(r"INSTANCIADOR|HOLDER|LOCAL_LAMINA|CAMERA_POS|BANCADA_POSICAO", re.I)
FORCE_ACTIVE = False


def uniq(n):
    n = n.strip() or "sem_nome"
    name_count[n] = name_count.get(n, 0) + 1
    return n if name_count[n] == 1 else f"{n}__{name_count[n]}"


def build(tid, skip_re):
    """Retorna indice do no glTF ou None se o ramo nao tem nada visivel."""
    t, tn = T[tid]
    if tn == "RectTransform":
        return None
    go = t.m_GameObject.read()
    if not go.m_IsActive and not FORCE_ACTIVE:
        return None
    name = go.m_Name
    if skip_re.search(name) and not children.get(tid):
        return None
    cs = comps(go)
    mesh_i = None
    if "MeshFilter" in cs and "MeshRenderer" in cs and not skip_re.search(name):
        r = cs["MeshRenderer"].read()
        if r.m_Enabled or "SINALEIRO" in name.upper():
            mf = cs["MeshFilter"].read()
            if mf.m_Mesh.m_PathID:
                try:
                    mesh_i = mesh_index(mf.m_Mesh, list(r.m_Materials))
                except Exception as e:
                    print("falha malha", name, repr(e)[:100])
    kids = [k for k in (build(c, skip_re) for c in children.get(tid, [])) if k is not None]
    animated = "Animation" in cs or "Animator" in cs
    anchor = bool(ANCHOR_RE.search(name)) and GROUP == "planta"
    if mesh_i is None and not kids and not animated and not anchor:
        return None
    p, q, s = t.m_LocalPosition, t.m_LocalRotation, t.m_LocalScale
    node = {"name": uniq(name)}
    if abs(p.x) + abs(p.y) + abs(p.z) > 0:
        node["translation"] = [p.x, p.y, -p.z]
    if not (abs(q.x) < 1e-7 and abs(q.y) < 1e-7 and abs(q.z) < 1e-7):
        node["rotation"] = [-q.x, -q.y, q.z, q.w]
    if (s.x, s.y, s.z) != (1, 1, 1):
        node["scale"] = [s.x, s.y, s.z]
    if mesh_i is not None:
        node["mesh"] = mesh_i
    if kids:
        node["children"] = kids
    extras = {"unityPathId": int(tid)}
    if animated:
        extras["unityAnimated"] = True
    if anchor and mesh_i is None and not kids:
        extras["marcador"] = True
    node["extras"] = extras
    gl["nodes"].append(node)
    node_of[tid] = len(gl["nodes"]) - 1
    if "Animation" in cs:
        anim_owners.append((tid, cs["Animation"]))
    return len(gl["nodes"]) - 1


def find_by_name(name):
    for tid, (t, tn) in T.items():
        if tn == "Transform" and t.m_GameObject.read().m_Name == name:
            return tid
    raise KeyError(name)


def root_name(tid):
    return T[tid][0].m_GameObject.read().m_Name


top_children = []
if GROUP == "smartblock":
    # produto: pecas que no Unity sao prefabs inativos, dispostas lado a lado
    FORCE_ACTIVE = True
    pecas = [("SMARTBLOCK_BASE (PARA VIDEOS) (4)", "SmartBlock_Montado_3_andares"),
             ("SMARTBLOCK_BASE TESTE MONTAGEM (5)", "SmartBlock_Base"),
             ("SMARTBLOCK_TAMPA", "SmartBlock_Tampa")] + [
        (f"SMARTBLOCK_LAMINA_{c}", f"Lamina_{c.title()}") for c in ("BRANCA", "PRETA", "VERMELHA", "AMARELO", "AZUL", "VERDE")]
    for i, (src, nm) in enumerate(pecas):
        k = build(find_by_name(src), re.compile(r"COLISAO|TRIGGER|GLOW", re.I))
        if k is None:
            print("sem geometria:", src)
            continue
        nd = gl["nodes"][k]
        nd["name"] = nm
        nd["translation"] = [5.0 * i, 0.0, 0.0]
        nd.pop("rotation", None)
        top_children.append(k)
else:
    if GROUP == "planta":
        sel = [r for r in roots if T[r][1] == "Transform" and root_name(r) not in SCENERY_ROOTS | SKIP_ROOTS]
        skip_re = SKIP_NAME
    else:
        sel = [r for r in roots if root_name(r) in SCENERY_ROOTS]
        skip_re = SKIP_NAME_CEN
    # Bancadas primeiro, resto agrupado em "Acessorios"
    for r in [r for r in sel if root_name(r) == "Bancadas"]:
        i = build(r, skip_re)
        if i is not None:
            top_children.append(i)
    acc = [k for k in (build(r, skip_re) for r in sel if root_name(r) != "Bancadas") if k is not None]
    if acc and GROUP == "planta":
        gl["nodes"].append({"name": "Acessorios_e_Pecas", "children": acc})
        top_children.append(len(gl["nodes"]) - 1)
    else:
        top_children += acc

gl["nodes"].append({"name": "Smart40_" + GROUP, "children": top_children, "scale": [SCALE] * 3,
                    "extras": {"metrosPorUnidadeUnity": SCALE,
                               "nota": "Hierarquia e pivos do gemeo digital Unity 'Smart 4.0 (Senai)'. Escala estimada."}})
gl["scenes"][0]["nodes"] = [len(gl["nodes"]) - 1]
gl["scenes"][0]["name"] = "Smart40_" + GROUP

# ---------------------------------------------------------------- animacoes (clips legados do Unity)
FPS = 30.0


def hermite(keys, t, cmp):
    """Avalia a curva Hermite do Unity (valor + tangentes in/out) no tempo t."""
    if t <= keys[0].time:
        return [getattr(keys[0].value, c) for c in cmp]
    if t >= keys[-1].time:
        return [getattr(keys[-1].value, c) for c in cmp]
    lo, hi = 0, len(keys) - 1
    while hi - lo > 1:
        mid = (lo + hi) // 2
        if keys[mid].time <= t:
            lo = mid
        else:
            hi = mid
    k0, k1 = keys[lo], keys[hi]
    dt = k1.time - k0.time
    if dt <= 0:
        return [getattr(k1.value, c) for c in cmp]
    u = (t - k0.time) / dt
    h00 = 2 * u ** 3 - 3 * u ** 2 + 1; h10 = u ** 3 - 2 * u ** 2 + u
    h01 = -2 * u ** 3 + 3 * u ** 2; h11 = u ** 3 - u ** 2
    out = []
    for c in cmp:
        m0, m1 = getattr(k0.outSlope, c), getattr(k1.inSlope, c)
        v0, v1 = getattr(k0.value, c), getattr(k1.value, c)
        if not (np.isfinite(m0) and np.isfinite(m1)):
            out.append(v0)  # tangente "constant"
        else:
            out.append(h00 * v0 + h10 * dt * m0 + h01 * v1 + h11 * dt * m1)
    return out


def resolve(tid, path):
    for part in [p for p in path.split("/") if p]:
        nxt = next((c for c in children.get(tid, []) if T[c][0].m_GameObject.read().m_Name == part), None)
        if nxt is None:
            return None
        tid = nxt
    return node_of.get(tid)


animations = []
for owner, ptr in anim_owners:
    try:
        anim = ptr.read()
    except Exception:
        continue
    for cptr in anim.m_Animations:
        if not cptr.m_PathID:
            continue
        clip = cptr.read()
        samplers, channels = [], []
        for kind, cmp, target in (("m_PositionCurves", "xyz", "translation"), ("m_RotationCurves", "xyzw", "rotation")):
            for cv in getattr(clip, kind, []):
                keys = cv.curve.m_Curve
                ni = resolve(owner, cv.path) if keys else None
                if ni is None:
                    continue
                t0, t1 = keys[0].time, keys[-1].time
                times = np.linspace(t0, t1, max(2, int(round((t1 - t0) * FPS)) + 1)).astype(np.float32)
                vals = np.array([hermite(keys, float(t), cmp) for t in times], dtype=np.float32)
                if target == "translation":
                    vals[:, 2] *= -1
                else:
                    vals[:, 0] *= -1; vals[:, 1] *= -1
                    vals /= np.linalg.norm(vals, axis=1, keepdims=True)
                    for i in range(1, len(vals)):
                        if np.dot(vals[i], vals[i - 1]) < 0:
                            vals[i] *= -1
                ia = add_accessor(times, 5126, "SCALAR", None, True)
                oa = add_accessor(vals, 5126, "VEC3" if target == "translation" else "VEC4", None)
                samplers.append({"input": ia, "output": oa, "interpolation": "LINEAR"})
                channels.append({"sampler": len(samplers) - 1, "target": {"node": ni, "path": target}})
        if channels:
            animations.append({"name": clip.m_Name, "samplers": samplers, "channels": channels,
                               "extras": {"dono": gl["nodes"][node_of[owner]]["name"]}})
if animations:
    gl["animations"] = animations

if not gl["extensionsUsed"]:
    del gl["extensionsUsed"]
for k in ("textures", "images"):
    if not gl[k]:
        del gl[k]

binary = b"".join(bin_chunks)
binary += b"\0" * ((-len(binary)) % 4)
gl["buffers"] = [{"byteLength": len(binary)}]
js = json.dumps(gl, ensure_ascii=False, separators=(",", ":")).encode("utf8")
js += b" " * ((-len(js)) % 4)
with open(OUT, "wb") as f:
    f.write(struct.pack("<III", 0x46546C67, 2, 12 + 8 + len(js) + 8 + len(binary)))
    f.write(struct.pack("<II", len(js), 0x4E4F534A)); f.write(js)
    f.write(struct.pack("<II", len(binary), 0x004E4942)); f.write(binary)
print(f"{OUT}: animacoes={len(gl.get('animations', []))} nos={len(gl['nodes'])} malhas={stats['meshes']} vertices={stats['verts']} tris={stats['tris']} "
      f"materiais={len(gl['materials'])} texturas={len(gl.get('textures', []))} bytes={12 + 16 + len(js) + len(binary)}")
