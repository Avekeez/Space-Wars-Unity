using UnityEngine;
using System.Collections;

public class TitleLogic : MonoBehaviour {

	private bool inEndSequence;
	private Rigidbody2D rb;

	void Awake () {
		Object.DontDestroyOnLoad(gameObject);
		rb = gameObject.GetComponent<Rigidbody2D> ();
		Application.LoadLevel (1);
		inEndSequence = false;
	}

	void Update () {
		if (inEndSequence) {
			inEndSequence = false;
			rb.gravityScale = 5;
			rb.AddForce (new Vector2 (0, 800));
		}
		if (transform.position.y < -20) {
			Destroy (gameObject);
			Destroy (GameObject.Find ("Cursor"));
			GameObject.FindGameObjectWithTag ("BlueBase").SendMessage ("allowControl", true);
			GameObject.FindGameObjectWithTag ("RedBase").SendMessage ("allowControl", true);
			GameObject.FindGameObjectWithTag ("MainCamera").SendMessage ("allowControl", true);
		}
	}
	void startEndSequence () {
		inEndSequence = true;
	}
}
