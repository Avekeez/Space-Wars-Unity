using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public string team;
	public int damage = 1;

	public bool rapidFire;

	public GameObject boom;

	private Rigidbody2D rb;

	public Transform parentTransform;

	private TrailRenderer trail;

	void OnEnable () {
		rb = gameObject.GetComponent<Rigidbody2D> ();
		if (parentTransform != null) {
			if (team == "Blue") {
				rb.velocity = parentTransform.right*50 + new Vector3 (3, 0);
			} else if (team == "Red") {
				rb.velocity = -parentTransform.right*50 + new Vector3 (-3, 0);
			}
		} 
		trail = GetComponent<TrailRenderer> ();
		trail.sortingLayerName = "Bullets";
		trail.sortingOrder = 0;
		GetComponent<SpriteRenderer> ().sortingOrder = 1;
	}

	void OnDisable () {
		parentTransform = null;
	}

	void OnCollisionEnter2D (Collision2D other) {
		if (other.gameObject.GetComponent<BaseShip> () != null && other.gameObject.activeInHierarchy && !other.gameObject.tag.Contains ("Blocker")) {
			other.gameObject.GetComponent<BaseShip> ().life -= damage;
			die ();
		}
		if (team == "Blue") {
			if (other.gameObject.tag == "RedBase") {
				other.gameObject.GetComponent<BaseController> ().health -= damage;
				GameObject.Find ("RedStatusCam").SendMessage ("shake");
				die ();
			} else if (other.gameObject.tag == "BlueBase") {
				other.gameObject.GetComponent<BaseController> ().health -= damage;
				GameObject.Find ("BlueStatusCam").SendMessage ("shake");
				die ();
			} else if (other.gameObject.tag == "RedBullet") {
				if (Random.value >= 0.5f) {
					rb.velocity = new Vector2 (-50, rb.velocity.y);
				} else {
					die ();
				}
			} else if (other.gameObject.tag == "RedBlocker") {
				rb.velocity = new Vector2 (-50, rb.velocity.y);
			}
		} else if (team == "Red") {
			if (other.gameObject.tag == "BlueBase") {
				other.gameObject.GetComponent<BaseController> ().health -= damage;
				GameObject.Find ("BlueStatusCam").SendMessage ("shake");
				die ();
			} else if (other.gameObject.tag == "RedBase") {
				other.gameObject.GetComponent<BaseController> ().health -= damage;
				GameObject.Find ("RedStatusCam").SendMessage ("shake");
				die ();
			} else if (other.gameObject.tag == "BlueBullet") {
				if (Random.value >= 0.5f) {
					rb.velocity = new Vector2 (50, rb.velocity.y);
				} else {
					die ();
				}
			} else if (other.gameObject.tag == "BlueBlocker") {
				rb.velocity = new Vector2 (50, rb.velocity.y);
			}
		}
        if (tag == "Boundary") {
            gameObject.SetActive (false);
        }
	}
	void FixedUpdate () {
        damage = Mathf.FloorToInt (rb.velocity.magnitude / 20f);
        ignoreTeamCollision ();
		checkOutsideBounds ();
		if (team == "Blue") {
			foreach (GameObject i in GameObject.FindGameObjectsWithTag("BlueShooter")) {
				Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D> (), i.GetComponent<BoxCollider2D> ());
			}
		} else if (team == "Red") {
			foreach (GameObject i in GameObject.FindGameObjectsWithTag("RedShooter")) {
				Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D> (), i.GetComponent<BoxCollider2D> ());
			}
		}
	}
	void ignoreTeamCollision () {
		if (team == "Blue") {
			foreach (GameObject i in GameObject.FindGameObjectsWithTag ("BlueBullet")) {
				Physics2D.IgnoreCollision (GetComponent<BoxCollider2D> (), i.GetComponent<BoxCollider2D> ());
			}
		} else if (team == "Red") {
			foreach (GameObject i in GameObject.FindGameObjectsWithTag ("RedBullet")) {
				Physics2D.IgnoreCollision (GetComponent<BoxCollider2D> (), i.GetComponent<BoxCollider2D> ());
			}
		}
	}
	public void die() {
		boom.transform.position = this.transform.position;
		boom.transform.localScale = new Vector3 (0.5f, 0.5f, 0);
		Instantiate(boom);
		boom.transform.position = new Vector2 (0, 0);
		boom.transform.localScale = new Vector3 (1, 1, 0);
		gameObject.SetActive (false);
	}
	public void checkOutsideBounds() {
		if (!gameObject.GetComponent<BoxCollider2D> ().bounds.Intersects (GameObject.Find ("space").GetComponent<BoxCollider2D> ().bounds)) {
			gameObject.SetActive (false);
		}
	}
}
