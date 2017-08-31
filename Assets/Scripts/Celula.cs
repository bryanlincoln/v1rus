using System.Collections;
using UnityEngine;

public class Celula : MonoBehaviour {

	private bool[] habilidades;

	private Vector3 target;

	private float speed = 0.1f;

	[SerializeField]
	private GameObject prefabCelula;

    [SerializeField]
    private Celula_AG celAg;

	private void Awake() {
		StartCoroutine (MelhorCaminho());
	}

	private void Update() {
		transform.position = Vector3.Lerp (transform.position, target, speed * Time.deltaTime);
	}

	public void SetHabilidades(bool[] habilidades) {
		this.habilidades = habilidades;
	}

	private IEnumerator MelhorCaminho() {
		for(;;) {
            target = celAg.GetTarget() ;
			target.x += transform.position.x;
			target.z = target.y + transform.position.z;
			target.y = 232;
			yield return new WaitForSeconds(1);
		}
	}

	private void OnCollisionEnter(Collision col) {
		bool morto = false;
		// pode detectar
		if(habilidades[0]) {
			// detectou um virus
			if (col.transform.CompareTag ("Virus")) {
				col.transform.GetComponent<Virus> ().Marca ();
			}
		}
		// pode englobar
		if (habilidades [1]) {
			if (col.transform.CompareTag ("VirusDetectado")) {
				// pode multiplicar
				if (habilidades [2]) {
					GameObject.Instantiate (prefabCelula, col.transform);
				}
				if (!morto) {
					morto = true;
					col.transform.GetComponent<Virus> ().Mata ();
				}
			}
		}
		// pode coletar e aumentar
		if (habilidades [3]) {
			if (col.transform.CompareTag ("VirusDetectado") || col.transform.CompareTag ("Virus")) {
				if (transform.localScale.x < 5)
					transform.localScale = new Vector3 (transform.localScale.x * 1.3f, transform.localScale.y * 1.3f, transform.localScale.y * 1.3f);
				if (!morto) {
					morto = true;
					col.transform.GetComponent<Virus> ().Mata ();
				}
			}
		}
	}

}