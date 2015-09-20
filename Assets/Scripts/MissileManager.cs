using UnityEngine;
using System.Collections;

public class MissileManager : MonoBehaviour {

    public GameObject missileObj;

	void Awake () {
        InvokeRepeating ("Launch", 0, 0.1f);
    }

    void Launch () {
        Missile mis = missileObj.GetComponent<Missile> ();
        mis.target = GameObject.Find ("Cursor").transform;
        mis.direction = Vector2.up;
        mis.initialPosition = new Vector2 (8*(Random.value - 0.5f), -8);
        Instantiate (missileObj);
    }
}
