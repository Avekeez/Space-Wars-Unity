using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LaserLogic : MonoBehaviour {

	ParticleSystem particles;
	ParticleSystem beam;
	LineRenderer line;

	private bool inLaserSequence;
	private bool inSplitSequence;
	private bool isActive;

	public Vector3 target;
	public GameObject laserObj;
	public string Team;

	private List<bool> isLaserDrawn = new List<bool> () {false, false, false, false, false, false, false};
	GameObject splitter;

	private bool startFadeOut;
	private bool startFadeIn;

	void Awake () {
		particles = transform.GetChild (0).gameObject.GetComponent<ParticleSystem> ();
		beam = transform.GetChild (1).gameObject.GetComponent<ParticleSystem> ();
		line = transform.GetChild (2).gameObject.GetComponent<LineRenderer> ();
		line.SetPosition (0, Vector3.zero);
		line.SetPosition (1, Vector3.zero);
		inLaserSequence = false;
		inSplitSequence = false;
		isActive = true;
		startFadeOut = false;
		startFadeIn = false;

		line.sortingLayerName = "Explosion/Fizz";

		particles.Play ();
		Invoke ("beginBeam", 0.5f);
		Invoke ("drawLine", 1);
		Invoke ("setLooping", 2);
		Invoke ("setInactive", 1.5f);
		Invoke ("destroy", 3);
	}

	void Update () {
		if (isActive) {
			if (GameObject.Find ("Splitter") != null) {
				splitter = GameObject.Find ("Splitter");
				target = splitter.transform.position-transform.position;
			} else {
				if (Team == "Blue") {
					target = new Vector3 (-transform.position.x + 15.27977f, 0, 0);
				} else if (Team == "Red") {
					target = new Vector3 (-transform.position.x - 14.7405f, 0, 0);
				}
			}
			if (inLaserSequence) {
				line.SetPosition (1, target);
				foreach (GameObject obj in GameObject.FindObjectsOfType (typeof (GameObject))) {
					if (obj.tag.Contains ("Suicide") || obj.tag.Contains ("Shooter") || obj.tag.Contains ("Blocker")) {
						if (obj.GetComponent<BaseShip> ().lane == 2 && !obj.GetComponent<BaseShip> ().hasSplit) {
							obj.SendMessage ("splitTime", Vector3.Distance (transform.position, obj.transform.position)/16f);
						}
					}
				}
				if (splitter != null && !inSplitSequence) {
					inSplitSequence = true;
					StartCoroutine (splitSequence ());
				}
				/*
				if (!startFadeIn) {
					startFadeIn = true;
					StartCoroutine (fadeIn ());
				}
				*/
			}
		} else {
			if (splitter != null) {
				for (int i = 0; i < 7; i++) {
					splitter.transform.GetChild (i).gameObject.SendMessage ("fade");
				}
			} else {
				if (!startFadeOut) {
					startFadeOut = true;
					StartCoroutine (fadeOut ());
				}
			}
		}
	}
	void beginBeam () {
		beam.Play ();
	}
	void drawLine () {
		inLaserSequence = true;
	}
	void setLooping () {
		particles.loop = false;
		beam.loop = false;
	}
	void setInactive () {
		isActive = false;
	}
	IEnumerator splitSequence () {
		yield return new WaitForSeconds (0.5f);
		for (int i = 0; i < 7; i++) {
			if (!isLaserDrawn[i]) {
				GameObject obj = (GameObject) Instantiate (laserObj);
				obj.GetComponent<LineRenderer> ().SetPosition (0, splitter.transform.position);
				obj.GetComponent<LineRenderer> ().SetPosition (1, new Vector3 (splitter.transform.position.x+100, splitter.transform.position.y+(splitter.transform.position.x+100)*Mathf.Tan (-Mathf.Deg2Rad*(5*(i-3)))));
				obj.transform.parent = splitter.transform;
				isLaserDrawn[i] = true;
				obj.SendMessage ("setIndex", i);
				yield return new WaitForSeconds (0.1f);
			}
		}
		yield return new WaitForSeconds (1);
		for (int i = 0; i < 7; i++) {
			splitter.transform.GetChild (i).SendMessage ("move");
		}
	}

	IEnumerator fadeIn () {
		for (int i = 0; i < 11; i++) {
			Color c;
			if (Team == "Blue") {
				c = new Color (0, 0, 1, (float) (i*15)/255);
				line.SetColors (c, c);
			} else if (Team == "Red") {
				c = new Color (1, 0, 0, (float) (i*15)/255);
				line.SetColors (c, c);
			}
			Debug.Log ((float) (i*15)/255);
			yield return new WaitForEndOfFrame ();
		}
	}

	IEnumerator fadeOut () {
		for (int i = 0; i < 8; i ++) {
			Color c;
			if (Team == "Blue") {
				c = new Color (0, 0, 1, (float) (150-i*15)/255);
				line.SetColors (c, c);
			} else if (Team == "Red") {
				c = new Color (1, 0, 0, (float) (150-i*15)/255);
				line.SetColors (c, c);
			}
			line.SetWidth ((float) 0.4f - i*0.05f, (float) 0.4f - i*0.05f);
			yield return new WaitForEndOfFrame ();
		}
	}

	void destroy () {
		Destroy (gameObject);
	}
}