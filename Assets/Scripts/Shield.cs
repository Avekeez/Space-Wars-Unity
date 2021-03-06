﻿using UnityEngine;
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
        GetComponent<CircleCollider2D> ().enabled = false;
        yield return new WaitForSeconds (1);
        GetComponent<CircleCollider2D> ().enabled = true;
        gameObject.SetActive (false);
	}
}
