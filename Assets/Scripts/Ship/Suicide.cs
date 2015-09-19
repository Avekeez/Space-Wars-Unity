using UnityEngine;
using System.Collections;

public class Suicide : MonoBehaviour {

	public BaseShip ship;

	private Rigidbody2D rb;
	
	void Awake () {
		ship = GetComponent<BaseShip> ();
		ship.maxLife = 5;
		ship.damage = 10;
		ship.maxSpeed = 10;
	}
}
