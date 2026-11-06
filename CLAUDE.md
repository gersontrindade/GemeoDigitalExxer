# Contexto: extração 3D da planta Smart 4.0 (SENAI)

Responda em **português**. Este arquivo resume o projeto para prompts futuros. Os detalhes de uso do modelo estão
em `modelo_3d/README.md`.

## Objetivo

Obter um modelo 3D fiel da planta didática **Smart 4.0** (SENAI / Exsto Labtronix), reutilizável em three.js,
Blender e outros. A fonte é o gêmeo digital em Unity, instalado em:

- App: `C:\Program Files\Smart 4.0 (Senai)\` (executável Unity, Mono, versão "2022", `app.info` = Exsto Labtronix)
- Dados: `C:\Program Files\Smart 4.0 (Senai)\Smart 4.0 (Senai)_Data\`. A cena principal é `level0`; malhas e
  texturas ficam em `sharedassets0.assets(.resS)`; o código fica em `Managed\Assembly-CSharp.dll`.

## Mapa desta pasta

| Pasta | O que é | Situação |
|---|---|---|
| `modelo_3d/` | **Entrega principal**: GLBs com hierarquia, 41 animações, visualizador three.js, README | atual |
| `modelo_3d/glb/smart40_planta.glb` | Planta (4 bancadas e 2 retornos) com compressão meshopt, 16,8 MB | uso em web |
| `modelo_3d/glb/smart40_planta_alta_resolucao.glb` | O mesmo conteúdo sem compressão, 70 MB | Blender e editores |
| `modelo_3d/glb/smart40_smartblock.glb`, `smart40_cenario.glb` | Produto SmartBlock e sala do simulador | |
| `modelo_3d/viewer/index.html` | Visualizador (three r170 local em `viewer/lib`) | abrir com `iniciar_visualizador.bat` |
| `modelo_3d/scripts/export_gltf.py` | Exportador Unity → GLB (UnityPy). Grupos: `planta`, `smartblock`, `cenario` | fonte da verdade |
| `modelo_3d/estrutura_planta.json` | Árvore de nós e animações, para consulta rápida sem abrir o GLB | |
| `codigo_csharp/` | C# descompilado (Assembly-CSharp, Modbus, etc.) | para entender a lógica e os movimentos |
| `cena_montada/`, `meshes_individuais/`, `texturas/`, `scripts_extracao/`, `preview.png` | Primeira extração (OBJ de 225 MB fundido, sem hierarquia) | **obsoleta**, só como referência |

## Fatos já levantados (não é preciso redescobrir)

- **Estrutura da cena:**
  - `Bancadas` contém `ESTOQUE_3D`, `PROCESSO_3D`, `QUALIDADE_3D` (fisicamente rotulada "MONTAGEM", módulo duplo,
    com o cobot), `EXPEDICAO_3D` e `RETORNO_ESQUERDA/DIREITA_BASE_3D`.
  - `CENARIO` é a sala; `ETC` guarda as câmeras e outros itens; o resto da cena é colisor, trigger, UI e
    instanciador.
- **Peças móveis** (cadeias preservadas):
  - robô `QUALIDADE_ROBO_PARTE_11_3D` (base) › 10 › 9 › … › 2 › `GARRA_ROBO_PONTA_3D` › pinças 1 e 2;
  - atuador do Processo: `PROCESSO_ATUADOR_HORIZONTAL_3D` › `_FRONTAL` › `_VERTICAL` › `PROCESSO_VENTOSA_3D`;
  - Estoque: `ESTOQUE_BRACO_3D` › `ELEVADOR` › `BRACO_2` › garras;
  - Expedição: `EXPEDICAO_BRACO_HORIZONTAL` › `ROTACAO` › `ALTURA` › `ALIMENTADOR` › `VENTOSA`;
  - alimentadores (deslizam no X local) e micro-cilindros 1 e 2 de cada bancada.
- **Animações:** só o robô (26 clips `QUALIDADE_<n>_<ação>`, de 1 a 27, sem o 23) e o atuador do Processo (15
  clips) têm `AnimationClip` legado. As outras peças são movidas por código em
  `codigo_csharp/Assembly-CSharp/Administracao.cs` a partir dos valores Modbus (`UModbusTCP.cs`).
- **SmartBlocks:** os do estoque são criados em tempo de execução (os prefabs ficam inativos). As 28 posições são
  os nós `ESTOQUE_INSTANCIADOR_LOCAL (n)`, exportados como marcadores (`extras.marcador`).
- **Escala:** 0,022 m por unidade Unity, aplicada só no nó raiz. É uma **estimativa**: supõe a esteira a ~0,95 m e
  dá bancada de 0,54 × 0,74 m e base do SmartBlock de ~69 mm. Nenhuma fonte pública traz as dimensões. Se o
  usuário fornecer uma medida real, recalcule. Os objetos `referencia_2 metros` e `MEDIDOR EM MM` da cena **não**
  servem para calibrar (são cubos de 2 e 1 unidade).
- **Layout:** no Unity existe um vão de ~24 cm entre Processo e Montagem, porque o simulador permite reordenar as
  bancadas (`SCRIPT_BANCADAS_MOVER.cs`). Na planta real os módulos ficam encostados (veja
  `modelo_3d/referencia_planta_real.jpg`). Os arquivos guardam o dado original; o visualizador tem a opção
  "Layout compacto".
- **Orientação no glTF:** Y para cima, frente das bancadas (painéis de botões) voltada para −X, bancadas alinhadas
  em Z.

## Regras de conversão do exportador (manter consistentes)

- **Eixos:** Unity (mão esquerda) → glTF (mão direita) invertendo Z.
  - posição `(x, y, -z)`;
  - quaternion `(-x, -y, z, w)`;
  - vértices e normais com Z negado;
  - ordem dos triângulos `[0, 2, 1]`;
  - UV com `v = 1 - v`.
- **Filtros:**
  - objetos inativos não entram (exceto no grupo `smartblock`, que força ativação);
  - submalhas com material de colisão ou trigger, alfa 0 ou shader de partícula/GUI/TextMeshPro são descartadas;
  - nomes que casam com `COLISAO|TRIGGER|GLOW|SOMBRA|TEXTO|VERS|MEDIDOR|referencia|cidade_1` são ignorados na
    planta;
  - nós vazios com nomes `INSTANCIADOR|HOLDER|LOCAL_LAMINA|CAMERA_POS|BANCADA_POSICAO` são mantidos como
    marcadores.
- **Materiais:**
  - Standard → PBR, com `roughness = 1 - _Glossiness`;
  - `_Mode` 1 → MASK e 2/3 → BLEND;
  - emissão HDR → `KHR_materials_emissive_strength`;
  - Unlit → `KHR_materials_unlit`;
  - texturas limitadas a 2048 px (JPEG se opacas, PNG se com alfa).
- **Animações:** curvas Hermite (valor e tangentes in/out) reamostradas a 30 fps em LINEAR; quaternions
  normalizados e com sinal contínuo.
- **Nomes repetidos** recebem o sufixo `__2`, `__3` etc.

## Ferramentas e comandos

- O venv da extração ficava num scratchpad temporário, que **não existe mais**. Para recriar:
  `python -m venv venv && venv\Scripts\pip install UnityPy numpy pillow` (testado com UnityPy 1.25.4 e
  Python 3.12).
- Ao rodar Python sobre os dados do Unity, use `python -I` e mantenha os scripts fora das pastas de dados.
- Exportar: `venv\Scripts\python -I modelo_3d\scripts\export_gltf.py "<_Data>" saida.glb planta [escala]`.
- Otimizar para web (Node 20 disponível): `npx @gltf-transform/cli dedup`, depois `weld`, depois
  `meshopt --level medium`. **Não** use `gltf-transform optimize` com os padrões: ele faz `join`/`flatten` e
  destrói a hierarquia.
- Validar: pacote npm `gltf-validator` (os GLBs atuais têm 0 erros e 0 avisos).
- Testar o visualizador sem interface: `puppeteer-core` com o Chrome em
  `C:\Program Files\Google\Chrome\Application\chrome.exe` e as flags
  `--use-angle=swiftshader --enable-unsafe-swiftshader`. O visualizador expõe `window.__smart40`
  (`scene`, `planta`, `play`, `frame`, `byName`, `clips`).
- O visualizador precisa de um servidor HTTP: o navegador bloqueia carregar `.glb` via `file://`. Use
  `python -m http.server 8040` dentro de `modelo_3d/`.

## Armadilhas conhecidas

- **GLTFLoader:**
  - malhas com vários materiais viram um `Group` com filhos `NOME_1`, `NOME_2`…, às vezes num `Group` sem nome;
  - para identificar os nós reais, use `gltf.parser.associations.get(obj).nodes !== undefined`;
  - os nomes são sanitizados: espaço vira `_` e `[ ] . : /` são removidos, mas os parênteses ficam;
  - se a cena e o nó raiz têm o mesmo nome, o nó vira `Smart40_planta_1`.
- **Meshopt:** o `smart40_planta.glb` (comprimido) não abre no Blender e em vários importadores. Para editores,
  use a versão `_alta_resolucao`.
- **Escalas internas:** os corpos das bancadas têm escala 0,01 e eixos locais herdados do Blender. Não suponha que
  o eixo de rotação de uma junta seja o Y local; verifique antes.
- **Bash no Windows:** heredocs longos com aspas mistas falham no Git Bash. Prefira gravar o script num arquivo e
  depois executá-lo.

## Ideias de próximos passos (ainda não feitas)

- Movimentar por código os braços do Estoque e da Expedição, os alimentadores e os discos dos retornos, a partir
  da lógica de `Administracao.cs` (limites `LIMITE_POSICAO_*`, velocidades `VELOCIDADE_*`).
- Ligar o visualizador a dados reais (Modbus TCP, OPC UA ou Node-RED) para virar um gêmeo digital na web.
- Calibrar a escala com uma medida física e recalcular.
- Animar SmartBlocks percorrendo a esteira usando os marcadores `SMARTBLOCK_HOLDER (POS n)`.

## Preferências do usuário

- Fazer da melhor forma possível, com fidelidade ao Unity e à planta real.
- Quando algo diverge, manter o dado original e oferecer um ajuste opcional (como o "Layout compacto"), sem
  alterar a geometria por conta própria.
- Entregas em formatos reutilizáveis (GLB, three.js) e documentadas em português.
