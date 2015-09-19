using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DisplayStatistics : MonoBehaviour {

	void Awake () {
		for (int i = 0; i < transform.childCount; i ++) {
			GameObject obj = transform.GetChild (i).gameObject;
			obj.GetComponent<MeshRenderer> ().sortingLayerName = "GUI";
		}
		transform.GetChild (0).GetComponent<TextMesh> ().text = Global.stat.Stats[0].ToString () + " Suicide Ships";
		transform.GetChild (1).GetComponent<TextMesh> ().text = Global.stat.Stats[1].ToString () + " Shooter Ships";
		transform.GetChild (2).GetComponent<TextMesh> ().text = Global.stat.Stats[2].ToString () + " Blocker Ships";
		transform.GetChild (3).GetComponent<TextMesh> ().text = Global.stat.Stats[2].ToString () + " Kills";

		transform.GetChild (4).GetComponent<TextMesh> ().text = Global.stat.Stats[0].ToString () + " Suicide Ships";
		transform.GetChild (5).GetComponent<TextMesh> ().text = Global.stat.Stats[1].ToString () + " Shooter Ships";
		transform.GetChild (6).GetComponent<TextMesh> ().text = Global.stat.Stats[2].ToString () + " Blocker Ships";
		transform.GetChild (7).GetComponent<TextMesh> ().text = Global.stat.Stats[2].ToString () + " Kills";
	}
}