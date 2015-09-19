using UnityEngine;
using System.Collections;

public class EndLogic : MonoBehaviour {
	public string winner;
	public Sprite blueWins;
	public Sprite redWins;
	void Awake () {
		Object.DontDestroyOnLoad (gameObject);
	}
	void Update () {
		if (GetComponent<Rigidbody2D> ().velocity.magnitude <= 0.5f && Input.GetKeyDown (KeyCode.Return)) {
			Application.LoadLevel (3);
			GetComponent<Rigidbody2D> ().drag = 5;
			GetComponent<SpringJoint2D> ().connectedAnchor = new Vector2 (0, -15);
		}
	}
	void setWinScreen (string win) {
		if (win == "Blue") {
			GetComponent<SpriteRenderer> ().sprite = blueWins;
		} else if (win == "Red") {
			GetComponent<SpriteRenderer> ().sprite = redWins;
		} else {
			Debug.Log ("ayy u done boofed");
		}
		GetComponent<Rigidbody2D> ().WakeUp ();
		transform.position = new Vector2 (0, 10);
	}
}