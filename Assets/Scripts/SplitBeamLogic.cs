using UnityEngine;
using System.Collections;

public class SplitBeamLogic : MonoBehaviour {

	bool startFade = false;
	bool startAngleMove = false;
	float fadeIncrement = 0;
	float angleIncrement = 1;

	int ind;

	LineRenderer line;

	void Awake () {
		line = GetComponent<LineRenderer> ();
	}

	void Update () {
		if (startAngleMove && angleIncrement > 0.5f) {
			line.SetPosition (1, new Vector3 (transform.parent.position.x+100, transform.parent.position.y+(transform.parent.position.x+100)*Mathf.Tan (-Mathf.Deg2Rad*(angleIncrement*5*(ind-3)))));
			angleIncrement -= 0.001f;
		}
		if (startFade && fadeIncrement <= 1.1f) {
			line.SetColors (new Color (1, 1, 1, (200/255)-fadeIncrement), new Color (1, 1, 1, (200/255)-fadeIncrement));
			fadeIncrement += 0.01f;
		}
	}
	void fade () {
		startFade = true;
	}
	void move () {
		startAngleMove = true;
	}
	void setIndex (int i) {
		ind = i;
	}
}
