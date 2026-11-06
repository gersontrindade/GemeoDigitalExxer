import sys, collections, UnityPy
env = UnityPy.load(sys.argv[1])
c = collections.Counter(o.type.name for o in env.objects)
for k,v in c.most_common(): print(f"{v:6d} {k}")
meshes=[]
for o in env.objects:
    if o.type.name=="Mesh":
        try:
            m=o.read(); meshes.append((m.m_Name, getattr(m,'m_VertexCount',0) or 0))
        except Exception as e: meshes.append(("<erro>",0))
print("\nMeshes:",len(meshes))
for n,vc in sorted(meshes,key=lambda x:-x[1])[:40]: print(f"  {vc:8d} verts  {n}")
