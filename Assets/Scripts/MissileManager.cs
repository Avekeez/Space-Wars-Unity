using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MissileManager : MonoBehaviour {

    public GameObject missileObj;
    public string Team;

    public List<GameObject> missiles;

    void Awake () {
        missiles = new List<GameObject> ();
        for (int i = 0; i < 20; i++) {
            GameObject obj = (GameObject) Instantiate (missileObj);
            obj.GetComponent<Missile> ().CancelInvoke ();
            obj.SetActive (false);
            missiles.Add (obj);
        }
    }

    public void Launch () {
        StartCoroutine ("_Launch");
    }
    IEnumerator _Launch () {
        for (int i = 0; i < 5; i++) {
            if (GetMissile () != null) {
                Missile mis = GetMissile ().GetComponent<Missile> ();
                mis.Team = Team;
                if (Random.value > 0.5f) {
                    mis.direction = -Vector3.up;
                    mis.initialPosition = new Vector2 (transform.position.x + Random.Range (-1, 1), -1.5f);
                } else {
                    mis.direction = Vector3.up;
                    mis.initialPosition = new Vector2 (transform.position.x + Random.Range (-1, 1), 1.5f);
                }
                mis.parent = gameObject;
                mis.gameObject.SetActive (true);
                mis.GetComponent<AudioSource> ().Play ();
                yield return new WaitForSeconds (0.2f);
            }
        }
    }

    public GameObject GetMissile () {
        for (int i = 0; i < missiles.Count; i++) {
            if (!missiles[i].activeInHierarchy) {
                return missiles[i];
                break;
            }
        }
        return null;
    }
}
