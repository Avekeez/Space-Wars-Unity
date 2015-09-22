using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour {
    ParticleSystem particleShield;
    ParticleSystem particleField;

    void Awake () {
        particleShield = transform.GetChild (0).gameObject.GetComponent<ParticleSystem> ();
        particleField = transform.GetChild (1).gameObject.GetComponent<ParticleSystem> ();
    }
 
    void OnEnable () {
        particleShield.Play ();
        particleField.Play ();
    }

	void Disable () {

	}
}
