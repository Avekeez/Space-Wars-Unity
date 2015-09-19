using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

	public Slider slider;
	public GameObject ship;
	public BlueController blue;
	public RedController red;

	void Awake () {
		if (ship.tag == "BlueBase") {
			blue = ship.GetComponent<BlueController> ();
		} else if (ship.tag == "RedBase") {
			red = ship.GetComponent<RedController> ();
		}
	}
	
	void Update () {
		if (ship != null) {
			if (ship.tag == "BlueBase") {
				slider.value = (float)blue.health/(float)blue.getMaxHealth ();
			} else if (ship.tag == "RedBase") {
				slider.value = (float)red.health/(float)red.getMaxHealth ();
			}
		}
	}
}
