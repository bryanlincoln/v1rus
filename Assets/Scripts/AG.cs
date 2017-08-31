using UnityEngine;
using System.Collections;

public class AG : MonoBehaviour {

	private const int TAX_CROSSOVER = 60;
	private const int TAX_ELIT = 0;
	private const float TAX_MUT = 0.008f;

	public static int tamanhoPopulacao = 15;
	private int geracoes = 100;
	private int geracoesCount = 0;

	private bool instanciado = false;

	[SerializeField]
	private GameObject prefabIndividuo;

	[SerializeField]
	private Transform[] transformMedula;

	private AG_Individuo[] populacao;

	private void Start () {
		// cria a população inicial
		populacao = new AG_Individuo[tamanhoPopulacao];
		for (int i = 0; i < tamanhoPopulacao; i++) {
			// instancia a célula pegando o elemento do AG
			populacao [i] = new AG_Individuo();
		}
		UI_Controller.UpdatePopulation (tamanhoPopulacao);
	}

	private void Update () {
		if (UI_Controller.inGame) {
			if (geracoesCount < geracoes) {
				// faz o trabalho do AG
				AG_Individuo[] pais = new AG_Individuo[tamanhoPopulacao - TAX_ELIT];
				Elitiza ();
				Seleciona (pais);
				Cruza (pais);
				Muta (true);
				Ativa ();
				// passa as gerações
				geracoesCount++;
			} else if (!instanciado) {
				instanciado = true;
				StartCoroutine (Spawna ());
			}
		}
	}

	private IEnumerator Spawna() {
		for (int i = 0; i < tamanhoPopulacao; i++) {
			int id = Random.Range(0, transformMedula.Length);
			Vector3 pos = transformMedula [id].position;
			pos.y = 232;
			// instancia o filho
			Celula globuloBranco = GameObject.Instantiate (prefabIndividuo, pos, transformMedula[id].rotation).GetComponent<Celula> ();
			globuloBranco.SetHabilidades (populacao [i].habilidades);
			globuloBranco.transform.position = pos;
			yield return new WaitForSeconds (1f);
		}
	}

	private void Elitiza() {
		// guarda os melhores fitness' em ordem
		int[] melhorFitness = new int[TAX_ELIT];
		// inicializa as variavies
		for (int i = 0; i < TAX_ELIT; i++) {
			melhorFitness [i] = 0;
		}
		AG_Individuo[] melhor = new AG_Individuo[TAX_ELIT];
		// pra cada indivíduo, verifica em qual posição seu fitness está
		for (int i = 0; i < tamanhoPopulacao; i++) {
			for (int j = 0; j < TAX_ELIT; j++) {
				if (populacao [i].fitness > melhorFitness [j]) {
					melhorFitness [j] = populacao [i].fitness;
					melhor[j] = populacao [i];
				}
			}
		}
		// pega os n melhores e faz eles sobreviverem
		for (int i = 0; i < TAX_ELIT; i++) {
			populacao [i] = melhor [i];
		}
	}
	private void Seleciona(AG_Individuo[] pais) {
		for (int i = 0; i < pais.Length; i++) {
			// seleciona o pai
			pais [i] = Roleta ();
		}
	}
	private AG_Individuo Roleta() {
		// guarda somas de fitness'
		int s = 0;
		// calcula a soma máxima
		for (int i = 0; i < tamanhoPopulacao; i++) {
			s += populacao [i].fitness;
		}
		// um valor aleatório entre 0 e máximo
		int r = Random.Range (0, s);
		// olha em que faixa acumulativa o r está
		for (int i = 0; i < tamanhoPopulacao; i++) {
			s += populacao [i].fitness;
			if (s >= r) {
				return populacao[i];
			}
		}
		// retorna o indivíduo
		return populacao [tamanhoPopulacao - 1];
	}
	private void Cruza(AG_Individuo[] pais) {
		// pra cada slot na população
		for (int i = TAX_ELIT; i < tamanhoPopulacao; i += 2) {
			AG_Individuo filho1;
			AG_Individuo filho2 = null;
			if (Random.Range (0, 100) < TAX_CROSSOVER && i < tamanhoPopulacao - 1) {
				// instancia o filho
				filho1 = new AG_Individuo();
				// instancia outro filho
				filho2 = new AG_Individuo();
				// um ponto aleatório de corte
				int ponto = Random.Range (0, pais [i].cromossoma.Length);
				// copia o código genético
				for (int j = 0; j < pais[i].cromossoma.Length; j++) {
					if (j < ponto) {
						filho1.cromossoma [j] = pais [i].cromossoma [j];
						filho2.cromossoma [j] = pais [i + 1].cromossoma [j];
					} else {
						filho1.cromossoma [j] = pais [i + 1].cromossoma [j];
						filho2.cromossoma [j] = pais [i].cromossoma [j];
					}
				}
			} else {
				// só diz que os pais vão continuar vivos
				filho1 = pais [i];
				if (i < tamanhoPopulacao - 1)
					filho2 = pais [i + 1];
			}
			// atualiza os resultados na população
			populacao [i] = filho1;
			if(i < tamanhoPopulacao - 1)
				populacao [i + 1] = filho2;	
		}
	}
	private void Muta(bool mutaElite) {
		// se podemos mutar a elite, começamos a mutação por eles, se não, os pulamos
		int ini = mutaElite ? 0 : TAX_ELIT;
		// pra cada indivíduo
		for (int i = ini; i < tamanhoPopulacao; i++) {
			// pra cada gene, muta de acordo com a probabilidade
			for (int j = 0; j < populacao [i].cromossoma.Length; j++) {
				if (Random.Range (0.0f, 1.0f) < TAX_MUT) {
					populacao [i].cromossoma [j] = !populacao [i].cromossoma [j];
				}
			}
		}
	}
	private void Ativa() {
		// ativa os novos genes de cada indivíduo da população, calculando seu fitness para a próxima iteração
		for(int i = 0; i < tamanhoPopulacao; i++) {
			populacao [i].DecodificaHabilidades ();
		}
	}
}
