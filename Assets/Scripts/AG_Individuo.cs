using UnityEngine;

public class Habil {
	public bool[] codigo;

	public Habil (int tam, bool[] codigo) {
		this.codigo = new bool[tam];
		// copia o código
		for(int i = 0; i < tam; i++) {
			this.codigo [i] = codigo[i];
		}	
	}
}

public class AG_Individuo {

	// constantes
	private static int TAM_CROM = 38; // 46 possibilitaria ter todas as habilidades
	// habilidades
	private static Habil[] HABIL = {
		new Habil(5, new bool[]{true, false, true, false, true}), // detectar e marcar
		new Habil(6, new bool[]{true, false, true, false, false, false}), // englobar
		new Habil(8, new bool[]{true, false, true, false, true, true, false, true}), // multiplicar
		new Habil(8, new bool[]{true, false, true, false, false, false, false, false}) // coletar nutrientes e aumentar o tamanho
	};

	// atributos
	public bool[] cromossoma;
	public bool[] habilidades;

	public int fitness = 0;

	// métodos
	public AG_Individuo() {
		cromossoma = new bool[TAM_CROM];
		SetCromossoma ();
		habilidades = new bool[HABIL.Length];
		MostraHabilidades ();
	}

	private void SetCromossoma() {
		for(int i = 0; i < cromossoma.Length; i++) {
			cromossoma [i] = (Random.Range (0, 2) == 0) ? true : false;
		}
	}

	private bool ContemHabilidade(bool[] habilidade) {
		for (int i = 0; i + habilidade.Length - 1 < cromossoma.Length; i++) {
			int j = 0;
			for (; j < habilidade.Length; j++) {
				if (cromossoma [i + j] != habilidade [j]) {
					break;
				}
			}
			if (j == habilidade.Length)
				return true;
		}

		return false;
	}

	public void DecodificaHabilidades() {
		for(int i = 0; i < habilidades.Length; i++) {
			if (ContemHabilidade (HABIL[i].codigo)) {
				habilidades [i] = true;
				fitness += HABIL [i].codigo.Length;
			}
		}
	}

	private void MostraHabilidades() {
		string strHabil = "";
		for (int i = 0; i < habilidades.Length; i++) {
			if (habilidades [i]) {
				switch(i) {
				case 0 :
					strHabil += "Detectar";
					break;
				case 1 :
					strHabil += "Englobar";
					break;
				case 2 :
					strHabil += "Multiplicar";
					break;
				case 3 :
					strHabil += "Coletar";
					break;
				}
			}
		}
	}
}