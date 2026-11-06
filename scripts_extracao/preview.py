import sys, math
from PIL import Image
obj, mtl, out = sys.argv[1:4]
kd={}; cur=None
for l in open(mtl):
    p=l.split()
    if p and p[0]=="newmtl": cur=p[1]
    elif p and p[0]=="Kd": kd[cur]=tuple(int(min(1,float(x))*255) for x in p[1:4])
V=[]; col={}; m=None
for l in open(obj):
    if l.startswith("v "): V.append(tuple(map(float,l.split()[1:4])))
    elif l.startswith("usemtl"): m=l.split()[1]
    elif l.startswith("f ") and m:
        i=int(l.split()[1].split("/")[0])-1; col[i]=kd.get(m,(200,200,200))
xs=[v[0] for v in V]; ys=[v[1] for v in V]; zs=[v[2] for v in V]
print("bbox X %.2f..%.2f  Y %.2f..%.2f  Z %.2f..%.2f"%(min(xs),max(xs),min(ys),max(ys),min(zs),max(zs)))
W,H=1400,800; a=math.radians(35); e=math.radians(30)
def proj(v):
    x,y,z=v; X=x*math.cos(a)+z*math.sin(a); Z=-x*math.sin(a)+z*math.cos(a)
    return X, y*math.cos(e)-Z*math.sin(e), y*math.sin(e)+Z*math.cos(e)
P=[proj(v) for v in V]
px=sorted(p[0] for p in P); py=sorted(p[1] for p in P); n=len(P)
x0,x1=px[n//200],px[-n//200]; y0,y1=py[n//200],py[-n//200]
s=min((W-40)/(x1-x0),(H-40)/(y1-y0))
img=Image.new("RGB",(W,H),(30,30,36)); zb={}
for i,(X,Y,D) in enumerate(P):
    u=int(20+(X-x0)*s); vv=int(H-20-(Y-y0)*s)
    if 0<=u<W and 0<=vv<H and D<zb.get((u,vv),1e9):
        zb[(u,vv)]=D; img.putpixel((u,vv),col.get(i,(170,170,170)))
img.save(out)
