# V1RUS
Neste trabalho busca-se demonstrar de forma simples o uso de algoritmos genéticos para simular uma batalha entre vírus e o sistema imunológico. Estes algoritmos são utilizados em dois aspectos do jogo: a geração de oponentes (agentes do sistema imunológico) de acordo com a dificuldade e o controle de navegação de tais.

## Interface, aspectos e funcionamento

É disponibilizado ao usuário controles como a designação de células-alvo, mutação (desmarca agentes marcados como maliciosos) e disfarce (vírus já marcados como agentes maliciosos são ignorados por células de destruição). O sistema pode evoluir para obter características como capacidade de verificar e marcar células possivelmente maliciosas, destruir agentes marcados como maliciosos, limpar células maliciosas e transformá-las em aliados e englobar células, se expandindo. Essas habilidades do sistema podem aparecer como fenótipo caso a sequência genética correspondente esteja presente no código de cada indivíduo. Isso significa que mais de uma característica pode se manifestar no mesmo indivíduo, onde sua aptidão é calculada como a soma dos pesos de cada uma dessas manifestações. O objetivo do jogo é infectar todas as células da região do corpo presente. O jogador perde se todas as suas instâncias de vírus forem eliminadas.

# Materiais e métodos
Para facilitar a exibição a gráfica e exportação para diferentes plataformas foi utilizado o motor Unity3D e a linguagem C Sharp. Para a geração da população de oponentes e seu controle de navegação no espaço do jogo foram utilizados algoritmos genéticos.
