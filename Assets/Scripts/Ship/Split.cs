using UnityEngine;
using System.Collections;

public class Split : MonoBehaviour {

	public GameObject boom;
	public string Team;

	void Awake () {
		transform.localScale = new Vector3 (1.5f, 1.5f);
		Rigidbody2D rb1 = transform.GetChild (0).gameObject.GetComponent<Rigidbody2D> ();
		Rigidbody2D rb2 = transform.GetChild (1).gameObject.GetComponent<Rigidbody2D> ();
		
		if (Team == "Blue") {
			rb1.velocity += new Vector2 (0, 0.2f);
			rb1.AddForceAtPosition (new Vector2 (0, 2), transform.GetChild (0).position + new Vector3 (0.16f, 0, 0), ForceMode2D.Impulse);
			
			rb2.velocity += new Vector2 (0, -0.2f);
			rb2.AddForceAtPosition (new Vector2 (0, -2), transform.GetChild (1).position + new Vector3 (0.16f, 0, 0), ForceMode2D.Impulse);
		} else if (Team == "Red") {
			rb1.velocity += new Vector2 (0, 0.2f);
			rb1.AddForceAtPosition (new Vector2 (0, 2), transform.GetChild (0).position + new Vector3 (-0.16f, 0, 0), ForceMode2D.Impulse);
			
			rb2.velocity += new Vector2 (0, -0.2f);
			rb2.AddForceAtPosition (new Vector2 (0, -2), transform.GetChild (1).position + new Vector3 (-0.16f, 0, 0), ForceMode2D.Impulse);
		}
		
		StartCoroutine (explode ());
	}

	IEnumerator explode () {
		boom.transform.localScale = new Vector3 (0.7f, 0.7f, 0.7f);
		yield return new WaitForSeconds (1);
		if (Random.value >= 0.5f) {
			if (transform.GetChild (0).gameObject.activeSelf) {
				GameObject obj = transform.GetChild (0).gameObject;
				Instantiate (boom, obj.transform.position, Quaternion.identity);
				obj.SetActive (false);
			}
			yield return new WaitForSeconds (0.2f*Random.value);
			if (transform.GetChild (1).gameObject.activeSelf) {
				GameObject obj = transform.GetChild (1).gameObject;
				Instantiate (boom, obj.transform.position, Quaternion.identity);
				obj.SetActive (false);
			}
		} else {
			if (transform.GetChild (1).gameObject.activeSelf) {
				GameObject obj = transform.GetChild (1).gameObject;
				Instantiate (boom, obj.transform.position, Quaternion.identity);
				obj.SetActive (false);
			}
			yield return new WaitForSeconds (0.2f*Random.value);
			if (transform.GetChild (0).gameObject.activeSelf) {
				GameObject obj = transform.GetChild (0).gameObject;
				Instantiate (boom, obj.transform.position, Quaternion.identity);
				obj.SetActive (false);
			}
		}
		yield return new WaitForSeconds (0.5f);
		Destroy (gameObject);
	}
}
