using UnityEngine;
using System.Collections;

public class ray : MonoBehaviour {

	Vector3 direction;
	Vector3 mouse;
	Ray2D raycast;

	public GameObject beam;

	void Awake () {
		Object.DontDestroyOnLoad (gameObject);
		GetComponent<LineRenderer> ().sortingLayerName = "Gui";
		GetComponent<LineRenderer> ().sortingOrder = 1;
	}

	void Update () {
		mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		mouse.z = 0;

		transform.LookAt (mouse);
		direction = transform.forward;

		RaycastHit2D hit;
		hit = Physics2D.Raycast (transform.position, direction, Mathf.Infinity, LayerMask.GetMask ("Ship"));

		Debug.DrawRay (transform.position, direction, Color.green);
		if (Input.GetMouseButton (0)) {
			GetComponent<LineRenderer> ().SetPosition (1, mouse * 100);
			if (hit.collider != null) {
				if (hit.collider.gameObject.GetComponent<BaseShip> () != null) {
					hit.collider.gameObject.GetComponent <BaseShip> ().explodeSingle ();
					hit.collider.gameObject.GetComponent <BaseShip> ().die ();
					Debug.Log ("ded");
				}
			}
		} else {
			GetComponent<LineRenderer> ().SetPosition (1, Vector3.zero);
		}
		if (Input.GetMouseButtonDown (1)) {
			Instantiate (beam, mouse, Quaternion.identity);
		}
		//Debug.Log (hit.collider.gameObject.tag.Contains ("Suicide") || hit.collider.gameObject.tag.Contains ("Shooter") || hit.collider.gameObject.tag.Contains ("Blocker"));

	}
}
