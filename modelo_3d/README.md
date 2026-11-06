# Planta Smart 4.0: modelo 3D (glTF/GLB + three.js)

Modelo 3D da planta didática **Smart 4.0 (SENAI / Exsto Labtronix)**, extraído do gêmeo digital Unity
instalado em `C:\Program Files\Smart 4.0 (Senai)` (cena `level0`, versão 2022).

Diferente do `cena_montada/cena_smart40.obj` anterior (225 MB, tudo fundido numa malha só, sem hierarquia),
estes arquivos mantêm **cada peça como um nó separado, com o pivô original**, então dá para animar braços, garras
e atuadores depois.

## Como abrir

- **Visualizador:** dê dois cliques em `iniciar_visualizador.bat`. Ele sobe um servidor local na porta 8040 e abre
  `http://localhost:8040/viewer/`. Funciona offline, porque o three.js r170 está em `viewer/lib`.
- **Blender e outros editores:** use File › Import › glTF 2.0 e escolha
  `glb/smart40_planta_alta_resolucao.glb`. O `smart40_planta.glb` usa compressão meshopt, que só vale para web
  (three.js, Babylon) e que vários importadores não leem.
- **Outras ferramentas:** Unity (glTFast), Unreal, Godot e o https://gltf-viewer.donmccurdy.com aceitam `.glb`
  direto.

## Arquivos

| Arquivo | Conteúdo | Tamanho |
|---|---|---|
| `glb/smart40_planta.glb` | 4 bancadas, 2 retornos de esteira e acessórios, com **41 animações**. Comprimido com meshopt, para web. | 16,8 MB |
| `glb/smart40_planta_alta_resolucao.glb` | O mesmo conteúdo sem compressão, mais compatível com editores 3D | 70 MB |
| `glb/smart40_smartblock.glb` | Produto SmartBlock: montado (3 andares), base, tampa e lâminas nas 6 cores | 0,6 MB |
| `glb/smart40_cenario.glb` | Sala do simulador: vidros, faixas no piso, galeria, vasos e logos | 6,9 MB |
| `estrutura_planta.json` | Árvore de nós e lista de animações (nome, duração, nós afetados) | |
| `scripts/export_gltf.py` | Exportador Unity → glTF usado para gerar tudo (UnityPy) | |
| `previews/` | Capturas do visualizador | |
| `referencia_planta_real.jpg` | Foto da planta física (Portal da Indústria), usada na comparação | |

## O que foi extraído

- **Geometria:** 1,14 milhão de triângulos em 83 malhas, sem colisores, triggers, sombras falsas nem *glows*.
- **Materiais PBR:** cor, metalicidade e rugosidade convertidas do shader Standard do Unity, 20 texturas
  (logos das bancadas, painéis, botões) e transparência dos vidros e acrílicos.
- **Hierarquia cinemática** (nomes originais do Unity):
  - **Estoque:** `ESTOQUE_BRACO_3D` (giro) › `ESTOQUE_ELEVADOR_3D` (sobe e desce) › `ESTOQUE_BRACO_2_3D` ›
    `ESTOQUE_GARRA_DIREITA/ESQUERDA_3D`. Também `ESTOQUE_ALIMENTADOR_3D` › `ESTOQUE_VENTOSA_3D` (desliza no X
    local) e os micro-cilindros 1 e 2.
  - **Processo:** `PROCESSO_ATUADOR_HORIZONTAL_3D` › `_FRONTAL_3D` › `_VERTICAL_3D` › `PROCESSO_VENTOSA_3D`
    (cartesiano XYZ) e o magazine de lâminas nas 6 cores.
  - **Qualidade/Montagem:** cobot com 11 elos, de `QUALIDADE_ROBO_PARTE_11_3D` (base) até
    `GARRA_ROBO_PONTA_3D`, que tem as pinças `GARRA_ROBO_PONTA_PINCA_1/2_3D`. Também `QUALIDADE_ALIMENTADOR_3D`.
  - **Expedição:** `EXPEDICAO_BRACO_HORIZONTAL_3D` › `_ROTACAO_3D` › `_ALTURA_3D` › `EXPEDICAO_ALIMENTADOR_3D`
    › `EXPEDICAO_VENTOSA_3D`.
  - **Retornos:** `RETORNO_ESQUERDA/DIREITA_BASE_3D` › `_CIRCULO_3D` (o disco que gira).
- **Animações:** os clips do próprio Unity foram reamostrados a 30 fps a partir das curvas Hermite:
  - 26 clips do robô, numerados na ordem do processo. Exemplos: `QUALIDADE_1_PEGAR_BLOCO_ESTEIRA_COLOCAR_B1`,
    `QUALIDADE_9_ANEXANDO_LAMINA_LADO_1_B1`, `QUALIDADE_24_HOME`.
  - 15 clips do atuador do Processo, como `..._BUSCAR_LAMINA_1..6` e `..._LARGAR_LAMINA_1..6`.

  No simulador, os braços do Estoque e da Expedição e os alimentadores não têm clips: o script
  `codigo_csharp/Assembly-CSharp/Administracao.cs` os move a partir dos valores Modbus. No modelo eles já estão
  com o pivô correto, prontos para receber movimento por código.
- **Marcadores:** nós vazios com `extras.marcador = true` guardam os pontos de instanciação e os *holders* do
  simulador. Exemplos: as 28 posições de `ESTOQUE_INSTANCIADOR_LOCAL`, as 6 de
  `PROCESSO_INSTANCIADOR_LAMINA_POS_1..6` e `SMARTBLOCK_HOLDER (POS n)`. O visualizador usa esses pontos para
  encher o estoque de SmartBlocks.

## Escala e eixos

- O nó raiz `Smart40_planta` tem **escala 0,022 m por unidade Unity**. Esse valor foi estimado, porque nenhuma
  fonte pública traz as medidas da planta. Ele assume a esteira a cerca de 0,95 m do piso e resulta em:
  - bancada simples: 0,54 m de largura × 0,74 m de profundidade;
  - altura do piso ao topo da estrutura: cerca de 1,2 m;
  - base do SmartBlock: cerca de 69 mm.

  Se você tiver uma medida real, por exemplo a largura de uma bancada com trena, corrija só a escala desse nó:
  `escala = medida_real_m / medida_atual_m × 0,022`. Também dá para regerar os arquivos com
  `python export_gltf.py <_Data> saida.glb planta <escala>`.
- Eixos glTF: Y para cima. A frente das bancadas (painéis de botões) fica voltada para **−X**, e as bancadas
  estão alinhadas ao longo de Z.

## Comparação com a planta real

A foto em `referencia_planta_real.jpg` confere com o modelo em vários pontos:

- a ordem dos módulos: Estoque, Processo, Montagem (Qualidade), Expedição;
- gabinetes pretos com moldura cinza e faixa verde;
- painel inclinado com botões;
- esteira em perfil de alumínio no topo;
- cobot branco com anéis azuis;
- módulo de Montagem com largura dupla.

A única diferença é que, no gêmeo digital, há um **vão de cerca de 24 cm entre Processo e Montagem**. O simulador
permite reordenar as bancadas em 4 posições (`SCRIPT_BANCADAS_MOVER.cs`). Na planta física os módulos ficam
encostados. Os arquivos guardam a posição original do Unity, e a opção **"Layout compacto"** do visualizador
fecha o vão.

## Uso em three.js (mínimo)

```js
import * as THREE from 'three';
import { GLTFLoader } from 'three/addons/loaders/GLTFLoader.js';
import { MeshoptDecoder } from 'three/addons/libs/meshopt_decoder.module.js';

const gltf = await new GLTFLoader().setMeshoptDecoder(MeshoptDecoder).loadAsync('glb/smart40_planta.glb');
scene.add(gltf.scene);

// tocar um clip do robô
const mixer = new THREE.AnimationMixer(gltf.scene);
mixer.clipAction(THREE.AnimationClip.findByName(gltf.animations, 'QUALIDADE_24_HOME')).play();
// no loop de render: mixer.update(dt)

// mover uma junta por código (ex.: girar o braço do Estoque)
gltf.scene.getObjectByName('ESTOQUE_BRACO_3D').rotation.y += 0.1;
```

O `GLTFLoader` troca espaços nos nomes por `_` e remove os caracteres `[ ] . : /`. Assim,
`SMARTBLOCK_HOLDER (POS 1)` vira `SMARTBLOCK_HOLDER_(POS_1)`. Os nomes das peças móveis não têm esses caracteres
e não mudam. Quando um nome se repete na cena, o exportador acrescenta `__2`, `__3` etc.

## Como regerar

```
python -m venv venv && venv\Scripts\pip install UnityPy numpy pillow
venv\Scripts\python -I scripts\export_gltf.py "C:\Program Files\Smart 4.0 (Senai)\Smart 4.0 (Senai)_Data" planta.glb planta
venv\Scripts\python -I scripts\export_gltf.py "...\Smart 4.0 (Senai)_Data" smartblock.glb smartblock
venv\Scripts\python -I scripts\export_gltf.py "...\Smart 4.0 (Senai)_Data" cenario.glb cenario
npx @gltf-transform/cli meshopt planta.glb smart40_planta.glb     (opcional: compressão para web)
```
