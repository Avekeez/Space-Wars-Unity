using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BulletPool : MonoBehaviour {
	public static BulletPool current;
	public GameObject BlueBullet;
	public GameObject RedBullet;
	public int bulletCount;

	public List<GameObject> BlueBullets;
	public List<GameObject> RedBullets;

	void Awake () {
		current = this;
	}

	void Start () {
		BlueBullets = new List<GameObject> ();
		for (int i = 0; i < bulletCount; i++) {
			GameObject obj = (GameObject) Instantiate (BlueBullet);
			obj.SetActive (false);
			BlueBullets.Add (obj);
		}
		RedBullets = new List<GameObject> ();
		for (int i = 0; i < bulletCount; i++) {
			GameObject obj = (GameObject) Instantiate (RedBullet);
			obj.SetActive (false);
			RedBullets.Add (obj);
		}
	}

	public GameObject getBullet (string team) {
		if (team == "Blue") {
			for (int i = 0; i < BlueBullets.Count; i++) {
				if (!BlueBullets[i].activeInHierarchy) {
					return BlueBullets[i];
				}
			}
			GameObject obj = (GameObject)Instantiate (BlueBullet);
			BlueBullets.Add (obj);
			return obj;
		} else if (team == "Red") {
			for (int i = 0; i < RedBullets.Count; i++) {
				if (!RedBullets[i].activeInHierarchy) {
					return RedBullets[i];
				}
			}
			GameObject obj = (GameObject)Instantiate (RedBullet);
			BlueBullets.Add (obj);
			return obj;
		} else {
			return null;
		}
	}
}
