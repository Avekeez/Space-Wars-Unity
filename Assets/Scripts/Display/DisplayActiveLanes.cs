using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DisplayActiveLanes : MonoBehaviour {
	List<GameObject> obj;
	BaseController Blue;
	BaseController Red;
	
	void Awake () {
		obj = new List<GameObject> () {null, null, null, null, null, null};
		Blue = GameObject.FindGameObjectWithTag ("BlueBase").GetComponent<BaseController> ();
		Red = GameObject.FindGameObjectWithTag ("RedBase").GetComponent<BaseController> ();
		for (int i = 0; i < 6; i++) {
			obj [i] = transform.GetChild (i).gameObject;
			obj [i].SetActive (false);
		}
	}

	void Update () {
		for (int i = 0; i < 3; i++) {
			obj [i].SetActive (Blue.selectedLanes [i]);
		}
		for (int i = 0; i < 3; i++) {
			obj [i+3].SetActive (Red.selectedLanes [i]);
		}
	}
}
