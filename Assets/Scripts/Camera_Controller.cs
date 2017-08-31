using UnityEngine;
using System.Collections;

public class Camera_Controller : MonoBehaviour {

	private bool selected = false;

	private Virus virus;

	[SerializeField]
	private Transform mousePos;

	[SerializeField]
	private Camera cam;

	private void Update() {
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		if (Input.GetMouseButtonUp (0))  {
			if (selected) {
				selected = false;
				if (Physics.Raycast (ray, out hit, 100) && virus != null) {
					Vector3 target = hit.point;
					target.y = virus.transform.position.y;
					mousePos.position = target;
					virus.SetTarget (mousePos);
					virus.DesSeleciona ();
				}
			}
			else if (Physics.Raycast (ray, out hit, 100) && hit.transform.tag.Contains("Virus")) {
				Select(hit.transform);
			}
		}

		//transform.Translate (new Vector3(Input.GetAxis ("Horizontal"), Input.GetAxis("Vertical"), 0));
	}

	private void Select(Transform obj) {
		selected = true;
		virus = obj.GetComponent<Virus>();
		virus.Seleciona ();
	}

	public IEnumerator Focus() {
		while(cam.fieldOfView < 77) {
			cam.fieldOfView += 1.5f + 2/cam.fieldOfView;
			yield return new WaitForSeconds(0.02f);
		}
	}
}
