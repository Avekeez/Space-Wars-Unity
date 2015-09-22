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

    public GameObject parent;

	public float speed;

    void OnEnable () {
        dead = false;
        rb = GetComponent<Rigidbody2D> ();
        rb.velocity = direction.normalized * speed;
        transform.position = initialPosition;
        //Invoke ("Die", 10);
        GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, 1);
        GetComponent<AudioSource> ().volume = 0.03f * Global.stat.SoundModifier;
    }

    void FixedUpdate () {
        if (!dead) {
			BaseController opp = parent.GetComponent<BaseController> ().Opponent.GetComponent<BaseController> ();
            if (target == null) {
                target = opp.transform;
            }
            for (int i = 0; i < opp.AllSuicide.Count; i++) {
                if (opp.AllSuicide[i].activeInHierarchy) {
                    target = opp.AllSuicide[i].transform;
                    break;
                }
            }
            for (int i = 0; i < opp.AllShooter.Count; i++) {
                if (opp.AllShooter[i].activeInHierarchy) {
                    target = opp.AllShooter[i].transform;
                    break;
                }
            }
            for (int i = 0; i < opp.AllBlocker.Count; i++) {
                if (opp.AllBlocker[i].activeInHierarchy) {
                    target = opp.AllBlocker[i].transform;
                    break;
                }
            }
            /*
            if (opp.health > 0) {
                target = opp.gameObject.transform;
            }
            */
            /*
            foreach (GameObject ship in GameObject.FindObjectsOfType (typeof (GameObject))) {
                if (Team == "Red" && (ship.tag == "BlueSuicide" || ship.tag == "BlueShooter" || ship.tag == "BlueBlocker")) {
                    target = ship.transform;
                    ship.GetComponent<BaseShip> ().targeted = true;
                    break;
                } else if (Team == "Blue" && (ship.tag == "RedSuicide" || ship.tag == "RedShooter" || ship.tag == "RedBlocker")) {
                    target = ship.transform;
                    ship.GetComponent<BaseShip> ().targeted = true;
                    break;
                }
            }
            */

            Vector3 relPoint;
            if (target == opp.transform) {
                relPoint = transform.InverseTransformPoint (target.position + Vector3.up * Random.Range (-2, 2));
            } else {
                relPoint = transform.InverseTransformPoint (target.position + new Vector3 (Random.Range (-0.5f, 0.5f), Random.Range (-0.5f, 0.5f)));
            }
            

            if (relPoint.y > 0) {
                rb.AddForce (transform.up * 8000);
            } else if (relPoint.y < 0) {
                rb.AddForce (-transform.up * 8000);
            }
            Vector3 dir = rb.velocity;
            float angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
            rb.velocity = rb.velocity.normalized * speed;
        } else {
            rb.velocity = Vector3.zero;
        }
    }

    void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.tag.Contains (parent.GetComponent<BaseController> ().Opponent.GetComponent<BaseController> ().Team)) {
            if (other.gameObject.tag.Contains ("Suicide") || other.gameObject.tag.Contains ("Shooter") || other.gameObject.tag.Contains ("Blocker")) {
				StartCoroutine (DestroyObj ());
				other.GetComponent<BaseShip> ().life -= 20;
			}
			if (other.gameObject.tag.Contains ("Base")) {
				StartCoroutine (DestroyObj ());
				other.GetComponent<BaseController> ().health -= 5 + Random.Range (-1, 1);
			}
            if (other.gameObject.tag.Contains ("Bullet")) {
                StartCoroutine (DestroyObj ());
                other.GetComponent<Bullet> ().die ();
            }
		}
	}

    void Die () {
        if (gameObject.activeInHierarchy) {
            StartCoroutine (DestroyObj ());
        }
    }
 
    IEnumerator DestroyObj () {
        boom.transform.localScale = Vector3.one / 2;
        Instantiate (boom, transform.position, Quaternion.identity);
        dead = true;
        GetComponent<SpriteRenderer> ().color = new Color (0, 0, 0, 0);
        transform.GetChild (0).gameObject.GetComponent<ParticleSystem> ().Stop ();
        yield return new WaitForSeconds (1);
        gameObject.SetActive (false);
    }
}
