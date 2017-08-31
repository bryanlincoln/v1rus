using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Veia_Fluxo : MonoBehaviour {

	[SerializeField]
	private Transform direção;

	private static float forca = 30;
	private static float pulsoDelay = 1f;
	private float pulsoTimer = 0;

	private void Awake() {
		Time.timeScale = 1;
	}

	private void Update() {
		pulsoTimer += Time.deltaTime;
	}

	private void OnTriggerStay(Collider obj) {
		if (pulsoTimer > pulsoDelay && obj.transform.tag == "Celula") {
			obj.GetComponent<Rigidbody> ().AddForce (forca * direção.forward);
			pulsoTimer = 0;
		}
	}

}
