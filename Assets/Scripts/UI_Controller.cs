using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Controller : MonoBehaviour {

	public static bool inGame = false;

	private float gameTimer = 0;

	[SerializeField]
	private Camera_Controller camController;

	[SerializeField]
	private GameObject telaMenu;
	[SerializeField]
	private GameObject telaJogo;
	[SerializeField]
	private GameObject telaFim;
	[SerializeField]
	private GameObject btnMutar;
	[SerializeField]
	private GameObject btnDisfarcar;

	[SerializeField]
	private Animator animInicio;

	[SerializeField]
	private Text txtVirusLevel;
	[SerializeField]
	private Text txtPopulation;
	[SerializeField]
	private Text txtResumo;
	[SerializeField]
	private Text txtFimTitulo;
	[SerializeField]
	private Text txtVirusPopulation;
	[SerializeField]
	private Text txtSkills;

	private static UI_Controller eu;

	private void Awake() {
		if (eu == null)
			eu = this;

		Time.timeScale = 0;
	}

	private void Update() {
		if (inGame) {
			gameTimer += Time.deltaTime;
		}
	}

	public void IniciarJogo() {
		StartCoroutine(camController.Focus ());
		animInicio.SetTrigger ("Start");
		StartCoroutine (InitiateControllers ());
		Time.timeScale = 1;
		inGame = true;
	}

	private IEnumerator InitiateControllers() {
		yield return new WaitForSeconds (1.1f);
		telaMenu.SetActive (false);
		telaJogo.SetActive (true);
	}

	public static void UpdateVirusLevel(int level) {
		eu.UpdateVirusLevelMethod (level);
	}
	private void UpdateVirusLevelMethod(int level) {
		txtVirusLevel.text = "Virus Level: " + level;
		if (level == 2) {
			txtSkills.text = "Virus Skills: Duplicate, Mutate";
			btnMutar.SetActive (true);
		} else if (level == 3) {
			txtSkills.text = "Virus Skills: Duplicate, Mutate, Disguise";
			btnDisfarcar.SetActive (true);
		}
	}

	public static void UpdatePopulation(int population) {
		eu.txtPopulation.text = "White Cell Population: " + population;
	}

	public void RestartGame() {
		SceneManager.LoadScene (0);
	}
	public static void Win() {
		eu.GameOverMethod (true);
	}
	private void GameOverMethod(bool ganhou) {
		telaJogo.SetActive (false);
		telaFim.SetActive (true);
		if (ganhou) {
			txtFimTitulo.text = "YOU WIN!";
		}
		txtResumo.text = "you reached level " + Virus.level + "\nand infected " + Virus.infectedCells + " cells\nin " + gameTimer + " seconds";
		inGame = false;
	}

	public static void UpdateVirusPopulation (int virusPopulation) {
		eu.txtVirusPopulation.text = "Virus Population: " + virusPopulation;
		if (virusPopulation <= 0) {
			eu.GameOverMethod (false);
		}
	}

	public static void DisableMutate(bool now) {
		if (now) {
			eu.btnMutar.SetActive (false);
		} else {
			eu.btnMutar.SetActive (true);
		}
	}

	public static void DisableDisguise(bool now) {
		if (now) {
			eu.btnDisfarcar.SetActive (false);
		} else {
			eu.btnDisfarcar.SetActive (true);
		}
	}

	public void Disfarca() {
		Virus.Disguise ();
	}
	public void Muta() {
		Virus.Mutate ();
	}
}