using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour {

	public float shootCooldown = 0.2f;
	private int currentBarrel;
	public BaseShip ship;
	private string team;

	void Awake () {
		ship = GetComponent<BaseShip> ();
		ship.maxLife = 10;
		ship.damage = 5;
		ship.maxSpeed = 3;
		currentBarrel = 0;
		if (gameObject.tag.Contains ("Blue")) {
			team = "Blue";
		} else if (gameObject.tag.Contains ("Red")) {
			team = "Red";
		}
	}

	void OnEnable () {
		InvokeRepeating ("Shoot", 1, shootCooldown);
	}

	void OnCollisionEnter2D(Collision2D other) {
		if (team == "Blue") {
			if (other.gameObject.tag == "RedSuicide" || other.gameObject.tag == "RedShooter" || other.gameObject.tag == "RedBlocker") {
				other.gameObject.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (5000, 0));
			}
		} else if (team == "Red") {
			if (other.gameObject.tag == "BlueSuicide" || other.gameObject.tag == "BlueShooter" || other.gameObject.tag == "BlueBlocker") {
				other.gameObject.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (-5000, 0));
			}
		}
	}
	public void Shoot() {
		GameObject obj = BulletPool.current.getBullet (team);
		if (currentBarrel == 0) {
			if (gameObject.tag.Contains ("Blue")) obj.transform.position = this.transform.position + this.transform.up.normalized * 0.13f;
			if (gameObject.tag.Contains ("Red")) obj.transform.position = this.transform.position - this.transform.up.normalized * 0.13f;
			currentBarrel = 1;
		} else if (currentBarrel == 1) {
			if (gameObject.tag.Contains ("Blue")) obj.transform.position = this.transform.position - this.transform.up.normalized * 0.13f;
			if (gameObject.tag.Contains ("Red")) obj.transform.position = this.transform.position + this.transform.up.normalized * 0.13f;
			currentBarrel = 0;
		}
		obj.GetComponent<Bullet> ().parentTransform = transform;
		obj.SetActive (true);
	}
	void OnDisable () {
		CancelInvoke ();
	}
}
