using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody2D))]
public class Missile : MonoBehaviour {

    Rigidbody2D rb;
    public Vector2 initialPosition;
    public Vector2 direction;

    public Transform target;

    public GameObject boom;

    bool dead;

    public string Team;

    bool hasTarget;

    public GameObject parent;

    void OnEnable () {
        dead = false;
        hasTarget = false;
        rb = GetComponent<Rigidbody2D> ();
        rb.velocity = direction.normalized * 10;
        transform.position = initialPosition;
	}

    void FixedUpdate () {
        if (!dead) {
            if (target == null) {
                foreach (GameObject ship in GameObject.FindObjectsOfType (typeof (GameObject))) {
                    if (Team == "Red" && (ship.tag == "BlueSuicide" || ship.tag == "BlueShooter" || ship.tag == "BlueBlocker") && !ship.GetComponent<BaseShip> ().targeted) {
                        target = ship.transform;
                        ship.GetComponent<BaseShip> ().targeted = true;
                        break;
                    } else if (Team == "Blue" && (ship.tag == "RedSuicide" || ship.tag == "RedShooter" || ship.tag == "RedBlocker") && !ship.GetComponent<BaseShip> ().targeted) {
                        target = ship.transform;
                        ship.GetComponent<BaseShip> ().targeted = true;
                        break;
                    } else {
                        target = parent.GetComponent<BaseController> ().Opponent.transform;
                        target.position += Vector3.up * Random.Range (-1.5f, 1.5f);
                    }
                }
            }
            Vector3 relPoint = transform.InverseTransformPoint (target.position);
            if (relPoint.y > 0) {
                rb.AddForce (transform.up * 5000);
            } else if (relPoint.y < 0) {
                rb.AddForce (-transform.up * 5000);
            }
            Vector3 dir = rb.velocity;
            float angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
            rb.velocity = rb.velocity.normalized * 10;
        } else {
            rb.velocity = Vector3.zero;
        }
    }

    void OnTriggerEnter2D (Collider2D other) {
        if (other.gameObject.tag.Contains ("Base") || other.gameObject.tag.Contains ("Suicide") || other.gameObject.tag.Contains ("Shooter") || other.gameObject.tag.Contains ("Blocker")) {
            Instantiate (boom, transform.position, Quaternion.identity);
            StartCoroutine (DestroyObj ());
            other.SendMessage ("Die");
        }
    }

    IEnumerator DestroyObj () {
        dead = true;
        GetComponent<SpriteRenderer> ().color = new Color (0, 0, 0, 0);
        transform.GetChild (0).gameObject.GetComponent<ParticleSystem> ().Stop ();
        yield return new WaitForSeconds (1);
        Destroy (gameObject);
    }
}
