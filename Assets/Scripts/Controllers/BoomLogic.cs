using UnityEngine;
using System.Collections;

public class BoomLogic : MonoBehaviour {

	float rnd;

	void Start() {
		rnd = Random.value;
		transform.localScale += new Vector3 (transform.localScale.x*(rnd-0.5f)/2, transform.localScale.x*(rnd-0.5f)/2, 0);
		gameObject.GetComponent<AudioSource> ().pitch += (rnd-0.5f)/10;
		gameObject.GetComponent<AudioSource> ().Play ();
		StartCoroutine (KillOnEnd ());
	}
	private IEnumerator KillOnEnd() {
		yield return new WaitForSeconds (1);
		Destroy (gameObject);
	}
}
