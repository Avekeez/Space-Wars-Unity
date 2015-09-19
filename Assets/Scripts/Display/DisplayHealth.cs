using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DisplayHealth : MonoBehaviour {
	public GameObject baseShip;
	public string team;

	void Update () {
		if (team == "Blue") {
			if (baseShip.GetComponent<BlueController> ().health > 0) {
				GetComponent<TextMesh> ().text = baseShip.GetComponent<BlueController>().health.ToString ();
			} else {
				GetComponent<TextMesh> ().text = "###";
			}
		}
		if (team == "Red") {
			if (baseShip.GetComponent<RedController> ().health > 0) {
				GetComponent<TextMesh> ().text = baseShip.GetComponent<RedController>().health.ToString ();
			} else {
				GetComponent<TextMesh> ().text = "###";
			}
		}
	}
}
