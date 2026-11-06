import json,struct,sys
b=open(sys.argv[1],'rb').read(); n=struct.unpack('<I',b[12:16])[0]; g=json.loads(b[20:20+n])
def node(i):
    nd=g['nodes'][i]; d={"nome":nd['name']}
    if 'mesh' in nd: d["malha"]=g['meshes'][nd['mesh']]['name']
    ex=nd.get('extras',{})
    if ex.get('unityAnimated'): d["animado_no_unity"]=True
    if ex.get('marcador'): d["marcador"]=True
    if nd.get('children'): d["filhos"]=[node(c) for c in nd['children']]
    return d
anims=[]
for a in g.get('animations',[]):
    t=max(g['accessors'][s['input']]['max'][0] for s in a['samplers'])
    anims.append({"nome":a['name'],"dono":a.get('extras',{}).get('dono'),"duracao_s":round(t,2),
                  "nos":sorted({g['nodes'][c['target']['node']]['name'] for c in a['channels']})})
json.dump({"arquivo":sys.argv[1].split('/')[-1],"metros_por_unidade_unity":g['nodes'][g['scenes'][0]['nodes'][0]].get('scale',[1])[0],
           "hierarquia":[node(r) for r in g['scenes'][0]['nodes']],"animacoes":anims},open(sys.argv[2],'w',encoding='utf8'),ensure_ascii=False,indent=1)
print(len(anims))
