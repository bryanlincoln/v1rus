# V1RUS
Neste trabalho busca-se demonstrar de forma simples o uso de algoritmos genéticos para simular uma batalha entre vírus e o sistema imunológico. Estes algoritmos são utilizados em dois aspectos do jogo: a geração de oponentes (agentes do sistema imunológico) de acordo com a dificuldade e o controle de navegação de tais.

## Interface, aspectos e funcionamento

É disponibilizado ao usuário controles como a designação de células-alvo, mutação (desmarca agentes marcados como maliciosos) e disfarce (vírus já marcados como agentes maliciosos são ignorados por células de destruição). O sistema pode evoluir para obter características como capacidade de verificar e marcar células possivelmente maliciosas, destruir agentes marcados como maliciosos, limpar células maliciosas e transformá-las em aliados e englobar células, se expandindo. Essas habilidades do sistema podem aparecer como fenótipo caso a sequência genética correspondente esteja presente no código de cada indivíduo. Isso significa que mais de uma característica pode se manifestar no mesmo indivíduo, onde sua aptidão é calculada como a soma dos pesos de cada uma dessas manifestações. O objetivo do jogo é infectar todas as células da região do corpo presente. O jogador perde se todas as suas instâncias de vírus forem eliminadas.

## Materiais e métodos

Para facilitar a exibição a gráfica e exportação para diferentes plataformas foi utilizado o motor Unity3D e a linguagem C Sharp. Para a geração da população de oponentes e seu controle de navegação no espaço do jogo foram utilizados algoritmos genéticos.

### Algoritmos Genéticos

Sendo uma classe de algoritmos evolutivos, os algoritmos genéticos se assemelham à teoria de Darwin em vários pontos, tendo como operadores aspectos como seleçao, cruzamento e mutaçao.

- Gerações<br/>
O número de gerações (iterações) nesta aplicação varia de acordo com o nível de dificuldade do jogo. É observável que o número de características fenotípicas que se manifestam num mesmo indivíduo cresce ao longo das gerações.

- População e representação<br/>
A cada geração, é criado um conjunto de soluções (população) que visa ser mais adaptado que a população da geração anterior. Cada indivíduo é representado por um código genético (neste caso, um vetor booleano) que pode ser decodificado em uma instância que apresenta ou não certa característica. É considerado que tal característica está presente no indivíduo se uma certa sequência booleana (selecionada empiricamente) é encontrada em seu código.

- Seleção<br/>
A seleção dos melhores indivíduos é feita com base na aptidão. Como foi dito anteriormente, a aptidão de um indivíduo é calculada como a soma dos pesos de cada característica manifestada. Os pesos foram atribuídos empiricamente com base na dificuldade que cada característica impõe. Feito o cálculo da aptidão, a seleção é feita através de torneio. No torneio, são selecionados aleatoriamente dois indivíduos da população, onde o detentor do maior valor de aptidão é escolhido como apto para cruzamento.

- Cruzamento<br/>
Após a seleção, existe uma probabilidade de cruzamento de 60%. O cruzamento dos indivíduos aptos é feito escolhendo-se aleatoriamente um ponto de corte no código genético e fazendo-se a troca das caudas dos mesmos. Dessa forma, diferentes sequências presentes em diferentes indivíduos podem se manifestar em um único indivíduo filho. 

- Mutação<br/>
Após a geração dos indivíduos sucessores, executamos a operação de mutação. Para cada caractere do código genético existe uma probabilidade de 0.8% de <i>flip<i/>. Como os indivíduos são representados com um código binário, este flip alteraria o valor de uma casa <i>false</i> (0) para <i>true</i> (1) e vice-versa.

- Elitismo<br/>
Dados os fatores aleatórios presentes nos operadores de mutação, existe a possibilidade de degradação de uma boa solução. Para combater isto, selecionamos os dois melhores indivíduos em cada geração e os copiamos para a população da próxima geração.

<br/><br/>
Após a execução do algoritmo genético é feita a instanciação das células de defesa e o jogo inicia. A aplicação do algoritmo para navegação das células de defesa seguiu o mesmo esquema da geração dos indivíduos, porém esta era sujeita a alterações nos valores da quantidade de gerações e tamanho da população, de modo que permitisse movimentações mais precisas em direção às instâncias do jogador.

## Resultados
Durante a execução, os indivíduos instanciados ao fim da execução do algoritmo possuíam as características esperadas, onde as de menor peso eram mais comuns enquanto as de maior peso eram menos. A movimentação das células de defesa também se mostrou menos aleatória a medida que o jogador evoluía no jogo, visto que era dado mais tempo para o algoritmo evoluir suas estratégias para combater as células maliciosas.


## Conclusões

Com este trabalho, podemos concluir que o poder dos algoritmos genéticos em resolver problemas de busca como o proposto é aplicável em diversas áreas, não só da computação. Dados os resultados preliminares do projeto, vemos que é possível sua extensão para simulações mais realistas além da adição de maiores graus liberdade para o jogador.

## Para mais informações
Para esclarecimento do código e de dúvidas em geral, contate-me em bryanufg@gmail.com.
