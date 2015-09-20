using UnityEngine;
using System.Collections;

public class MissileManager : MonoBehaviour {

    public GameObject missileObj;
    public string Team;

    public void Launch () {
        StartCoroutine ("_Launch");
    }
    IEnumerator _Launch () {
        for (int i = 0; i < 5; i++) {
            Missile mis = missileObj.GetComponent<Missile> ();
            mis.Team = Team;
            if (Random.value > 0.5f) {
                mis.direction = -Vector3.up;
                mis.initialPosition = new Vector2 (transform.position.x + Random.Range (-1, 1), -1.5f);
            } else {
                mis.direction = Vector3.up;
                mis.initialPosition = new Vector2 (transform.position.x + Random.Range (-1, 1), 1.5f);
            }
            mis.parent = gameObject;
            Instantiate (missileObj);
            yield return new WaitForSeconds (0.2f);
        }
    }
}
