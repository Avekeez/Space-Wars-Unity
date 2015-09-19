using UnityEngine;
using System.Collections;

public class MenuButton : MonoBehaviour {

	public Sprite idle;
	public Sprite active;
	public GameObject cursor;

	void Start () {
		Object.DontDestroyOnLoad(gameObject);
		GetComponent<SpriteRenderer> ().sprite = idle;
	}

	void Update () {
		if (GetComponent<BoxCollider2D> ().bounds.Intersects (cursor.GetComponent<BoxCollider2D> ().bounds)) {
			GetComponent<SpriteRenderer> ().sprite = active;
			if (Input.GetMouseButtonDown (0)) {
				GameObject.Find ("title").SendMessage ("startEndSequence");
				GetComponent<SpriteRenderer> ().sprite = idle;
				active = null;
			}
		} else {
			GetComponent<SpriteRenderer> ().sprite = idle;
		}
	}
}