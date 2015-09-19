using UnityEngine;
using System.Collections;

public class ExplosionManager : MonoBehaviour {

	public static ExplosionManager current;
	public GameObject boom;


	void Awake () {
		current = this;
	}

	public void createExplosion (Vector3 pos1, Vector3 pos2) {
		if (!existsAtPosition (new Vector3 ((pos1.x + pos2.x) / 2, (pos1.y + pos2.y) / 2, 0))) {
			boom.transform.localScale = new Vector3 (1, 1, 1);
			Instantiate (boom, new Vector3 ((pos1.x + pos2.x) / 2, (pos1.y + pos2.y) / 2, 0), Quaternion.identity);
		}
	}
	public void createSingleExplosion (Vector3 pos) {
		boom.transform.localScale = new Vector3 (1, 1, 1);
		Instantiate (boom, pos, Quaternion.identity);
	}
	bool existsAtPosition (Vector3 pos) {
		foreach (GameObject i in GameObject.FindGameObjectsWithTag ("Explosion")) {
			if (i.transform.position == pos) {
				return true;
			}
		}
		return false;
	}
}
