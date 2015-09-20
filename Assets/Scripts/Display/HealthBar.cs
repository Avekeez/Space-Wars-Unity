using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

	public Slider slider;
	public GameObject ship;
	public BaseController Base;

	void Awake () {
        Base = ship.GetComponent<BaseController> ();
	}
	
	void Update () {
		if (ship != null) {
            slider.value = (float) Base.health / (float) Base.getMaxHealth ();
        }
	}
}
