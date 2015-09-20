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

    void OnEnable () {
        dead = false;
        rb = GetComponent<Rigidbody2D> ();
        rb.velocity = direction.normalized * 10;
        transform.position = initialPosition;
	}

    void FixedUpdate () {
        if (!dead) {
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
        if (other.gameObject.tag != "Missile") {
            Instantiate (boom, transform.position, Quaternion.identity);
            StartCoroutine (DestroyObj ());
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
