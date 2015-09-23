using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour {
    ParticleSystem particleShield;
    ParticleSystem particleField;

    void Awake () {
        particleShield = transform.GetChild (0).gameObject.GetComponent<ParticleSystem> ();
        particleField = transform.GetChild (1).gameObject.GetComponent<ParticleSystem> ();
        gameObject.SetActive (false);
    }
 
    void OnEnable () {
        particleShield.Play ();
        particleField.Play ();
        Invoke ("Disable", 5);
    }

    void Disable () {
        StartCoroutine (_Disable ());
    }

	IEnumerator _Disable () {
        particleShield.Stop ();
        particleField.Stop ();
        yield return new WaitForSeconds (1);
        gameObject.SetActive (false);
	}
}
