using UnityEngine;
using System.Collections;

public class Half : MonoBehaviour {
    void OnCollisionEnter2D (Collision2D other) {
        GameObject boom = transform.parent.gameObject.GetComponent<Split> ().boom;
        if (other.gameObject.tag.Contains ("Suicide") || other.gameObject.tag.Contains ("Shooter") || other.gameObject.tag.Contains ("Blocker")) {
            other.gameObject.GetComponent<BaseShip> ().life -= 9;
            Instantiate (boom, transform.position, Quaternion.identity);
            gameObject.SetActive (false);
        }
        if (other.gameObject.tag.Contains ("Base")) {
            other.gameObject.GetComponent<BaseController> ().health -= 3;
            Instantiate (boom, transform.position, Quaternion.identity);
            gameObject.SetActive (false);
        }
    }
}
