using UnityEngine;
using System.Collections;

public class Blocker : MonoBehaviour {

	public GameObject fizz;
	public BaseShip ship;
	
	void Awake () {
		ship = GetComponent<BaseShip> ();
		ship.maxLife = 50;
		ship.damage = 5;
		ship.maxSpeed = 1;
	}
	void OnCollisionEnter2D(Collision2D other) {
		if (gameObject.tag.Contains ("Blue")) {
			if (other.gameObject.tag == "RedSuicide" || other.gameObject.tag == "RedShooter" || other.gameObject.tag == "RedBlocker") {
				if (other.gameObject.GetComponent<BaseShip> ().life-gameObject.GetComponent<BaseShip> ().damage >= 0) {
					other.gameObject.GetComponent<Rigidbody2D> ().AddForce (new Vector2(15000, 0));
					if (other.gameObject.tag == "RedBlocker") {
						fizz.transform.position = new Vector3((gameObject.transform.position.x+other.gameObject.transform.position.x)/2, (gameObject.transform.position.y+other.gameObject.transform.position.y)/2);
						Instantiate(fizz);
					}
				}
			}
		} else if (gameObject.tag.Contains ("Red")) {
			if (other.gameObject.tag == "BlueSuicide" || other.gameObject.tag == "BlueShooter" || other.gameObject.tag == "BlueBlocker") {
				if (other.gameObject.GetComponent<BaseShip> ().life-gameObject.GetComponent<BaseShip> ().damage >= 0) {
					other.gameObject.GetComponent<Rigidbody2D> ().AddForce (new Vector2(-15000, 0));
					if (other.gameObject.tag == "BlueBlocker") {
						fizz.transform.position = new Vector3((gameObject.transform.position.x+other.gameObject.transform.position.x)/2, (gameObject.transform.position.y+other.gameObject.transform.position.y)/2);
						Instantiate(fizz);
					}
				}
			}
		}
	}
}
