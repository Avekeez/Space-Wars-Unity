using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlueController : MonoBehaviour {

    public List<KeyCode> keys;

    void Awake () {
        /* [0] Build Suicide
         * [1] Build Shooter
         * [2] Build Blocker
         * [3] Lane 1
         * [4] Lane 2
         * [5] Lane 3
         * [6] Cannon
         * [7] Missiles
         */
        keys = new List<KeyCode> () { KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.A, KeyCode.S };
        GetComponent<BaseController> ().keys = keys;
        GetComponent<BaseController> ().Team = "Blue";
    }
}
