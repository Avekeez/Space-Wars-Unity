using UnityEngine;
using System.Collections;

public class BaseShip : MonoBehaviour {
	public int lane;
	public string team;
	public int damage;
	public int maxLife;
	public int life;
	public int maxSpeed;

	private bool inRotation;
	private float startTime;
	private float distance;
	private Vector3 startRotation;

	private Rigidbody2D rb;
	private GameObject ship;

	public GameObject splitObj;
	public bool hasSplit;

    public bool targeted;

	void OnEnable () {
		rb = gameObject.GetComponent<Rigidbody2D> ();
		life = maxLife;
		hasSplit = false;
		if (team == "Blue") {
			ship = GameObject.Find("BlueBase");
			if (lane == 1) {
				transform.position = new Vector2 (-16, 1+ship.transform.position.y + ship.GetComponent<Rigidbody2D> ().velocity.magnitude * 0.5f);
			} else if (lane == 2) {
				transform.position = new Vector2 (-16, ship.transform.position.y + ship.GetComponent<Rigidbody2D> ().velocity.magnitude * 0.5f);
			} else if (lane == 3) {
				transform.position = new Vector2 (-16, -1+ship.transform.position.y + ship.GetComponent<Rigidbody2D> ().velocity.magnitude * 0.5f);
			}
		} else if (team == "Red") {
			ship = GameObject.Find("RedBase");
			if (lane == 1) {
				transform.position = new Vector2 (16, 1+ship.transform.position.y + ship.GetComponent<Rigidbody2D> ().velocity.magnitude * 0.5f);
			} else if (lane == 2) {
				transform.position = new Vector2 (16, ship.transform.position.y + ship.GetComponent<Rigidbody2D> ().velocity.magnitude * 0.5f);
			} else if (lane == 3) {
				transform.position = new Vector2 (16, -1+ship.transform.position.y + ship.GetComponent<Rigidbody2D> ().velocity.magnitude * 0.5f);
			}
		}
		StartCoroutine (ignoreBaseTemp ());
	}

	void OnCollisionEnter2D(Collision2D other) {
		if (team == "Blue") {
			if (other.gameObject.tag == "RedBase") {
				other.gameObject.GetComponent<BaseController> ().health -= damage;
				GameObject.Find ("RedStatusCam").SendMessage ("shake");
				explodeSingle ();
				die();
			}
			if (other.gameObject.tag == "BlueBase") {
				other.gameObject.GetComponent<BaseController> ().health -= damage;
				GameObject.Find ("BlueStatusCam").SendMessage ("shake");
				explodeSingle ();
				die();
			}
			if (other.gameObject.tag == "RedSuicide" || other.gameObject.tag == "RedShooter" || other.gameObject.tag == "RedBlocker") {
				other.gameObject.GetComponent<BaseShip> ().life -= damage;
				if (other.gameObject.GetComponent<BaseShip> ().life - damage <= 0 && life - other.gameObject.GetComponent<BaseShip> ().damage <= 0) {
					explode (other.transform.position);
				} else if (life - other.gameObject.GetComponent<BaseShip> ().damage <= 0) {
					explodeSingle ();
				}
			}
		} else if (team == "Red") {
			if (other.gameObject.tag == "BlueBase") {
				other.gameObject.GetComponent<BaseController> ().health -= damage;
				GameObject.Find ("BlueStatusCam").SendMessage ("shake");
				explodeSingle ();
				die();
			}
			if (other.gameObject.tag == "RedBase") {
				other.gameObject.GetComponent<BaseController> ().health -= damage;
				GameObject.Find ("RedStatusCam").SendMessage ("shake");
				explodeSingle ();
				die();
			}
			if (other.gameObject.tag == "BlueSuicide" || other.gameObject.tag == "BlueShooter" || other.gameObject.tag == "BlueBlocker") {
				other.gameObject.GetComponent<BaseShip> ().life -= damage;
				if (other.gameObject.GetComponent<BaseShip> ().life - damage <= 0 && life - other.gameObject.GetComponent<BaseShip> ().damage <= 0) {
					explode (other.transform.position);
				} else if (life - other.gameObject.GetComponent<BaseShip> ().damage <= 0) {
					explodeSingle ();
				}
			}
		}
	}

	void FixedUpdate() {
		checkOutsideBounds ();
		ignoreTeamCollisions ();
		if (team == "Blue") {
			if (rb.velocity.x <= maxSpeed) {
				rb.AddForce (new Vector2 (1000, 0));
			}
		} else if (team == "Red") {
			if (rb.velocity.x >= -maxSpeed) {
				rb.AddForce (new Vector2 (-1000, 0));
			}
		}
		if (life <= 0) {
			die ();
		}
        CorrectRotation ();
	}

	public void ignoreTeamCollisions() {
		foreach (GameObject i in GameObject.FindObjectsOfType (typeof (GameObject))) {
			if (i.tag.Contains("Blue") && team == "Blue") {
				Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D> (), i.GetComponent<BoxCollider2D> ());
			}
			if (i.tag.Contains("Red") && team == "Red") {
				Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D> (), i.GetComponent<BoxCollider2D> ());
			}
		}
	}

	public IEnumerator ignoreBaseTemp() {
		Physics2D.IgnoreCollision (gameObject.GetComponent<BoxCollider2D> (), ship.GetComponent<BoxCollider2D> (), true);
		yield return new WaitForSeconds (2);
		Physics2D.IgnoreCollision (gameObject.GetComponent<BoxCollider2D> (), ship.GetComponent<BoxCollider2D> (), false);
	}

	public void explode (Vector3 other) {
		ExplosionManager.current.createExplosion (transform.position, other);
	}

	public void explodeSingle () {
		ExplosionManager.current.createSingleExplosion (transform.position);
	}

	public void die() {
		if (ship != null) {
			if (GetComponent<Suicide> () != null) {
				ship.GetComponent<BaseController> ().activeSuicide --;
			} else if (GetComponent<Shooter> () != null) {
				ship.GetComponent<BaseController> ().activeShooter --;
			} else if (GetComponent<Blocker> () != null) {
				ship.GetComponent<BaseController> ().activeBlocker --;
			}
		}
		transform.eulerAngles = Vector3.zero;
        if (team == "Blue") {
            transform.position = new Vector3 (-16, 0);
        } else if (team == "Red") {
            transform.position = new Vector3 (16, 0);
        }
        gameObject.SetActive (false);
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.tag == "Shield") {
			GameObject ship = other.gameObject.transform.parent.gameObject;
			print ("ayy");
			if (other.GetComponent<BaseController> ().Team != team) {
				explodeSingle ();
				die ();
			}
		}
	}

	public void checkOutsideBounds() {
		if (!gameObject.GetComponent<BoxCollider2D> ().bounds.Intersects (GameObject.Find ("space").GetComponent<BoxCollider2D> ().bounds)) {
			gameObject.SetActive (false);
		}
	}

	void splitTime (float time) {
		hasSplit = true;
		Invoke ("activateSplit", time);
	}

	void activateSplit () {
		GameObject obj = (GameObject) Instantiate (splitObj, transform.position, transform.rotation);
		for (int i = 0; i < 2; i++) {
			obj.transform.GetChild (i).gameObject.GetComponent<Rigidbody2D> ().velocity += GetComponent<Rigidbody2D> ().velocity;
		}
		die ();
	}

    void CorrectRotation () {
        transform.rotation = (Quaternion.Lerp (transform.rotation, Quaternion.identity, 0.1f));
    }
}
