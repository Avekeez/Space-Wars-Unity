using UnityEngine;
using System.Collections;

public class Status : MonoBehaviour {
	private bool inShake;
	void Awake () {
		inShake = false;
	}
	void shake () {
		StartCoroutine (shakeSequence ());
	}
	public IEnumerator shakeSequence() {
		if (!inShake) {
			inShake = true;
			transform.position += new Vector3 (0.05f, 0.05f, 0);
			transform.eulerAngles += new Vector3 (0, 0, 2);
			yield return new WaitForSeconds (0.03f);
			transform.position += new Vector3 (-0.05f, -0.05f, 0);
			transform.eulerAngles += new Vector3 (0, 0, -2);
			yield return new WaitForSeconds (0.03f);
			transform.position += new Vector3 (0.05f, -0.05f, 0);
			transform.eulerAngles += new Vector3 (0, 0, -2);
			yield return new WaitForSeconds (0.03f);
			transform.position += new Vector3 (-0.05f, 0.05f, 0);
			transform.eulerAngles += new Vector3 (0, 0, 2);
			inShake = false;
		}
	}
}
