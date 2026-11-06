import sys, os, re, collections, UnityPy
env = UnityPy.load(sys.argv[1]); out = sys.argv[2]
for d in ("meshes","textures"): os.makedirs(os.path.join(out,d), exist_ok=True)
seen = collections.Counter(); stats = collections.Counter(); fails=[]
def fname(n):
    n = re.sub(r'[^\w\-. ]','_', n or "sem_nome"); seen[n]+=1
    return n if seen[n]==1 else f"{n}_{seen[n]}"
for o in env.objects:
    t=o.type.name
    try:
        if t=="Mesh":
            m=o.read(); data=m.export()
            if data and data.count("\nv ")>0:
                open(os.path.join(out,"meshes",fname(m.m_Name)+".obj"),"w").write(data); stats["mesh"]+=1
        elif t in("Texture2D",):
            tx=o.read(); img=tx.image
            img.save(os.path.join(out,"textures",fname(tx.m_Name)+".png")); stats["tex"]+=1
    except Exception as e: fails.append((t,str(e)[:80]))
print(stats, "falhas:",len(fails)); [print(" ",f) for f in fails[:10]]
