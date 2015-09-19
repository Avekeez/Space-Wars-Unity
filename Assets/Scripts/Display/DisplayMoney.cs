using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DisplayMoney : MonoBehaviour {

	public GameObject baseShip;
	public string team;
	
	void Update () {
		if (team == "Blue") {
			if (baseShip.GetComponent<BlueController> ().health > 0) {
				GetComponent<TextMesh>().text = baseShip.GetComponent<BlueController>().money.ToString ();
			} else {
				GetComponent<TextMesh>().text = "#####";
			}
		} 
		if (team == "Red") {
			if (baseShip.GetComponent<RedController> ().health > 0) {
				GetComponent<TextMesh>().text = baseShip.GetComponent<RedController>().money.ToString ();
			} else {
				GetComponent<TextMesh>().text = "#####";
			}
		}
	}
}
