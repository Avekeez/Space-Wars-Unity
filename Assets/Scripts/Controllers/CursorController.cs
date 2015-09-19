using UnityEngine;
using System.Collections;

public class CursorController : MonoBehaviour {
	Vector3 mouse;

	public bool frameEnd;
	public bool allowCursor;

	void Awake () {
		Object.DontDestroyOnLoad(gameObject);
		frameEnd = false;
		allowCursor = false;
	}

	void Update () {
		if (!frameEnd) {
			frameEnd = true;
			StartCoroutine (allowSceneLoad ());
		}
		if (allowCursor) {
			mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			mouse.z = 0;
			transform.position = mouse;
		}
	}

	public IEnumerator allowSceneLoad () {
		yield return new WaitForEndOfFrame ();
		allowCursor = true;

	}
}
