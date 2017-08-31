using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Virus : MonoBehaviour {

	private const float infectDelay = 1;
	private const float mutateDelay = 5;
	private const float disguiseDelay = 3;

	private const int copies = 1;

	public static int level = 1;
	public static int infectedCells = 0;

	// pra não deixar usar mais de uma vez
	private static bool podeMutar = true;
	private static bool podeDisfarcar = true;

	private float speed = 2;
	private float collisionTimer = 0;

	private Transform target;

	[SerializeField]
	private Rigidbody rig;

	private bool colliding = false;

	[SerializeField]
	private MeshRenderer meshRenderer;

	[SerializeField]
	private Material infectedMaterial;
	[SerializeField]
	private Material normalMaterial;
	[SerializeField]
	private Material selectedMaterial;

	[SerializeField]
	private GameObject prefabVirus;

	private static List<Virus> instancias;

	private void Awake() {
		if(instancias == null) {
			instancias = new List<Virus>();
			instancias.Add (this);
		}
	}

	private void Start() {
		//Vector3 dir = new Vector3 (Random.Range (-1, 1), Random.Range (-1, 1));
		//rig.AddForce (dir/4);
	}

	private void Update() {
		if (target != null) {
			transform.position = Vector3.Lerp (transform.position, target.position, speed * Time.deltaTime);
			if (colliding) {
				collisionTimer += Time.deltaTime;
				if (collisionTimer > infectDelay) {
					Duplicate ();
					collisionTimer = 0;
				}
			}
			if (Vector3.Distance (transform.position, target.position) < 0.01f)
				target = null;
		}
		// disfarça
		if (level > 2 && podeDisfarcar && Input.GetKeyDown("n")) {
			StartCoroutine (Disfarca());
		}
		else if (level > 1 && podeMutar && Input.GetKeyDown ("m")) {
			Desmarca ();
		}
	}

	public static void Disguise() {
		if (level > 2) {
			foreach (Virus v in instancias) {
				v.StartCoroutine (v.Disfarca ());
			}
		}
	}
	public static void Mutate() {
		if (level > 1) {
			foreach (Virus v in instancias) {
				v.Desmarca ();
			}
		}
	}

	private IEnumerator Disfarca() {
		Debug.Log ("Disfarçou");
		string lastTag = transform.tag;
		transform.tag = "VirusDisfarcado";
		yield return new WaitForSeconds (0.05f);
		podeDisfarcar = false;
		yield return new WaitForSeconds (2);
		transform.tag = lastTag;
		StartCoroutine (WaitDisguise ());
	}

	private IEnumerator WaitDisguise() {
		UI_Controller.DisableDisguise (true);
		yield return new WaitForSeconds (disguiseDelay);
		UI_Controller.DisableDisguise (false);
		podeDisfarcar = true;
	}

	public void SetTarget(Transform target) {
		this.target = target;
	}

	private void OnTriggerEnter(Collider col) {
		if (col.transform.CompareTag ("Infectavel")) {
			SetTarget (col.transform);
			speed = 1.3f;
			colliding = true;
		}
	}
	private void OnTriggerExit(Collider col) {
		speed = 2;
		colliding = false;
	}

	private void Duplicate() {
		if (target.name == "MousePos")
			return;
		for (int i = 0; i < copies; i++) {
			Virus v = GameObject.Instantiate (prefabVirus, transform.position, target.rotation).GetComponent<Virus>();

			instancias.Add(v);
            
			UI_Controller.UpdateVirusPopulation (instancias.Count);
		}
		infectedCells++;
		if (infectedCells % 5 == 0) {
			level++;
            Celula_AG.generations += 10;
            Celula_AG.population += 10;
            UI_Controller.UpdateVirusLevel (level);
		}
		Destroy (target.gameObject);
		if (GameObject.FindGameObjectsWithTag ("Infectavel").Length < 2) {
			UI_Controller.Win ();
		}
	}

	public void Marca() {
		transform.tag = "VirusDetectado";
		meshRenderer.material = infectedMaterial;
	}
	public void Desmarca() {
		Debug.Log ("Mutou");
		transform.tag = "Virus";
		meshRenderer.material = normalMaterial;
		StartCoroutine (WaitMutation ());
	}

	private IEnumerator WaitMutation() {
		UI_Controller.DisableMutate (true);
		yield return new WaitForSeconds (0.05f);
		podeMutar = false;
		yield return new WaitForSeconds (mutateDelay);
		UI_Controller.DisableMutate (false);
		podeMutar = true;
	}

	public void Mata() {
		instancias.Remove (this);
		UI_Controller.UpdateVirusPopulation (instancias.Count);
		Destroy (this.gameObject);
	}

	public void Seleciona() {
		meshRenderer.material = selectedMaterial;
	}
	public void DesSeleciona() {
		meshRenderer.material = transform.CompareTag ("Virus") ? normalMaterial : infectedMaterial;
	}

}
