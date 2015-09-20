using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DisplayHealth : MonoBehaviour {
	public GameObject baseShip;
	public string team;

	void Update () {
        if (baseShip.GetComponent<BaseController> ().health > 0) {
            GetComponent<TextMesh> ().text = baseShip.GetComponent<BaseController> ().health.ToString ();
        } else {
            GetComponent<TextMesh> ().text = "###";
        }
	}
}
