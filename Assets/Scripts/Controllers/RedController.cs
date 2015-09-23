using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RedController : MonoBehaviour {

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
        keys = new List<KeyCode> () { KeyCode.I, KeyCode.O, KeyCode.P, KeyCode.Alpha8, KeyCode.Alpha9, KeyCode.Alpha0, KeyCode.K, KeyCode.L, KeyCode.Semicolon };
        GetComponent<BaseController> ().keys = keys;
        GetComponent<BaseController> ().Team = "Red";
    }
}
