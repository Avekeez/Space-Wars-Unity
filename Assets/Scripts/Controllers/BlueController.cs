using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlueController : MonoBehaviour {

	public GameObject Opponent;

	public GameObject BlueBlocker;
	public GameObject BlueShooter;
	public GameObject BlueSuicide;

	public List<bool> selectedLanes = new List<bool> () {false, false, false};
	
	public float SuicideCooldown = 0.5f;
	public float ShooterCooldown = 1;
	public float BlockerCooldown = 2;
	
	private bool canBuildBlueSuicide = true;
	private bool canBuildBlueShooter = true;
	private bool canBuildBlueBlocker = true;
	private bool canGiveMoney = true;

	public int money = 30;
	public const int moneyMax = 1000;
	public const float giveMoneyCooldown = 0.2f;

	public int health;
	public const int maxHealth = 500;

	public bool canControl;

	public int activeSuicide;
	public int activeShooter;
	public int activeBlocker;
	private int maxSuicide = 15;
	private int maxShooter = 15;
	private int maxBlocker = 15;
	public List<GameObject> AllSuicide;
	public List<GameObject> AllShooter;
	public List<GameObject> AllBlocker;

	public GameObject boom;
	public GameObject cannon;
	private bool inDeathSequence;

	public bool hasWon;

	void Awake() {
		hasWon = false;
		inDeathSequence = false;
		money = moneyMax/10;
		health = maxHealth;
		canControl = false;
		AllSuicide = new List<GameObject>();
		for (int i = 0; i < maxSuicide; i++) {
			GameObject obj = (GameObject)Instantiate(BlueSuicide);
			obj.SetActive(false);
			AllSuicide.Add(obj);
		}
		AllShooter = new List<GameObject>();
		for (int i = 0; i < maxShooter; i++) {
			GameObject obj = (GameObject)Instantiate(BlueShooter);
			obj.SetActive(false);
			AllShooter.Add(obj);
		}
		AllBlocker = new List<GameObject>();
		for (int i = 0; i < maxBlocker; i++) {
			GameObject obj = (GameObject)Instantiate(BlueBlocker);
			obj.SetActive(false);
			AllBlocker.Add(obj);
		}
        //GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, (Random.value - 0.5f) * 2).normalized * 100, ForceMode2D.Impulse);
	}

	void FixedUpdate () {
		if (canControl) {
			updateLane ();
			updateMoney ();
			if (money >= 10) {
				if (Input.GetKeyDown (KeyCode.Q) && canBuildBlueSuicide && activeSuicide < maxSuicide) {
					int totalShips = 0;
					if (selectedLanes[0] && selectedLanes[1] && selectedLanes[2] && money >= 30) {
						StartCoroutine (buildSuicideSquadron ());
						totalShips = 3;
						money -= 30;
					} else {
						if (selectedLanes[1] && money >= 10) {
							buildSuicide (2);
							totalShips++;
							money -= 10;
						}
						if (selectedLanes[0] && money >= 10) {
							buildSuicide (1);
							totalShips++;
							money -= 10;
						}
						if (selectedLanes[2] && money >= 10) {
							buildSuicide (3);
							totalShips++;
							money -= 10;
						}
					}
					Global.stat.BlueSuicideSpawned += totalShips;
					playCreateSound();
					StartCoroutine(BluSuiWait(totalShips));
				}
				if (Input.GetKeyDown (KeyCode.W) && canBuildBlueShooter && activeShooter < maxShooter) {
					int totalShips = 0;
					if (selectedLanes[0] && selectedLanes[1] && selectedLanes[2] && money >= 30) {
						StartCoroutine (buildShooterSquadron ());
						totalShips = 3;
						money -= 30;
					} else {
						if (selectedLanes[1] && money >= 10) {
							buildShooter (2);
							totalShips++;
							money -= 10;
						}
						if (selectedLanes[0] && money >= 10) {
							buildShooter (1);
							totalShips++;
							money -= 10;
						}
						if (selectedLanes[2] && money >= 10) {
							buildShooter (3);
							totalShips++;
							money -= 10;
						}
					}
					Global.stat.BlueShooterSpawned += totalShips;
					playCreateSound();
					StartCoroutine(BluShootWait(totalShips));
				}
				if (Input.GetKeyDown (KeyCode.E) && canBuildBlueBlocker && activeBlocker < maxBlocker) {
					int totalShips = 0;
					if (selectedLanes[0] && selectedLanes[1] && selectedLanes[2] && money >= 30) {
						StartCoroutine (buildBlockerSquadron ());
						totalShips = 3;
						money -= 30;
					} else {
						if (selectedLanes[1] && money >= 10) {
							buildBlocker (2);
							totalShips++;
							money -= 10;
						}
						if (selectedLanes[0] && money >= 10) {
							buildBlocker (1);
							totalShips++;
							money -= 10;
						}
						if (selectedLanes[2] && money >= 10) {
							buildBlocker (3);
							totalShips++;
							money -= 10;
						}
					}
					Global.stat.BlueBlockerSpawned += totalShips;
					playCreateSound();
					StartCoroutine(BluBlockWait(totalShips));
				}
				if (Input.GetKey (KeyCode.A)) {
                    if (Input.GetKeyDown (KeyCode.Alpha1)) {
                        cannon.GetComponent<LaserLogic> ().lane = 1;
                        Instantiate (cannon, transform.position, Quaternion.identity);
                    }
                    if (Input.GetKeyDown (KeyCode.Alpha2)) {
                        cannon.GetComponent<LaserLogic> ().lane = 2;
                        Instantiate (cannon, transform.position, Quaternion.identity);
                    }
                    if (Input.GetKeyDown (KeyCode.Alpha3)) {
                        cannon.GetComponent<LaserLogic> ().lane = 3;
                        Instantiate (cannon, transform.position, Quaternion.identity);
                    }
                    
				}
			}
		}
		if (health <= 0 && !inDeathSequence) {
			inDeathSequence = true;
			if (!hasWon) Opponent.GetComponent<RedController> ().hasWon = true;
			StartCoroutine (die ());
		}
        GetComponent<AudioSource> ().volume = 0.1f * Global.stat.SoundModifier;
        //Hover ();
    }

	void updateLane() {
		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			selectedLanes[0] = !selectedLanes[0];
		}
		if (Input.GetKeyDown (KeyCode.Alpha2)) {
			selectedLanes[1] = !selectedLanes[1];
		}
		if (Input.GetKeyDown (KeyCode.Alpha3)) {
			selectedLanes[2] = !selectedLanes[2];
		}
	}
	void updateMoney() {
		if (canGiveMoney && money < moneyMax) {
			canGiveMoney = false;
			money ++;
			StartCoroutine (giveMoney ());
		}
	}
	private void playCreateSound() {
		gameObject.GetComponent<AudioSource> ().Play ();
	}
	#region waitFunctions
	public IEnumerator BluSuiWait(int shipsBuilt) {
		canBuildBlueSuicide = false;
		yield return new WaitForSeconds (SuicideCooldown * shipsBuilt);
		canBuildBlueSuicide = true;
	}
	public IEnumerator BluShootWait(int shipsBuilt) {
		canBuildBlueShooter = false;
		yield return new WaitForSeconds (ShooterCooldown * shipsBuilt);
		canBuildBlueShooter = true;
	}
	public IEnumerator BluBlockWait(int shipsBuilt) {
		canBuildBlueBlocker = false;
		yield return new WaitForSeconds (BlockerCooldown * shipsBuilt);
		canBuildBlueBlocker = true;
	}
	#endregion
	public IEnumerator giveMoney() {
		yield return new WaitForSeconds (giveMoneyCooldown);
		canGiveMoney = true;
	}
	public void allowControl (bool state) {
		canControl = state;
	}
	public int getMaxHealth () {
		return maxHealth;
	}
	#region buildFunctions
	private void buildSuicide (int lane) {
		for (int i = 0; i < AllSuicide.Count; i++) {
			if (!AllSuicide[i].activeInHierarchy) {
				AllSuicide[i].GetComponent<BaseShip>().lane = lane;
				AllSuicide[i].GetComponent<BaseShip>().team = "Blue";
				AllSuicide[i].SetActive (true);
				activeSuicide++;
				break;
			}
		}
	}
	private void buildShooter (int lane) {
		for (int i = 0; i < AllShooter.Count; i++) {
			if (!AllShooter[i].activeInHierarchy) {
				AllShooter[i].GetComponent<BaseShip>().lane = lane;
				AllShooter[i].GetComponent<BaseShip>().team = "Blue";
				AllShooter[i].SetActive (true);
				activeShooter++;
				break;
			}
		}
	}
	private void buildBlocker (int lane) {
		for (int i = 0; i < AllBlocker.Count; i++) {
			if (!AllBlocker[i].activeInHierarchy) {
				AllBlocker[i].GetComponent<BaseShip>().lane = lane;
				AllBlocker[i].GetComponent<BaseShip>().team = "Blue";
				AllBlocker[i].SetActive (true);
				activeBlocker++;
				break;
			}
		}
	}
	#endregion

	IEnumerator buildSuicideSquadron () {
		buildSuicide (2);
		yield return new WaitForSeconds (0.1f*Random.value);
		buildSuicide (1);
		yield return new WaitForSeconds (0.1f*Random.value);
		buildSuicide (3);
	}
	IEnumerator buildShooterSquadron () {
		buildShooter (2);
		yield return new WaitForSeconds (0.1f*Random.value);
		buildShooter (1);
		yield return new WaitForSeconds (0.1f*Random.value);
		buildShooter (3);
	}
	IEnumerator buildBlockerSquadron () {
		buildBlocker (2);
		yield return new WaitForSeconds (0.1f*Random.value);
		buildBlocker (1);
		yield return new WaitForSeconds (0.1f*Random.value);
		buildBlocker (3);
	}

	IEnumerator die () {
		boom.transform.localScale = new Vector3 (1, 1, 0);
		yield return new WaitForEndOfFrame ();
		boom.transform.position = transform.position;
		boom.transform.localScale = new Vector3 (10, 10, 0);
		Instantiate (boom);
		transform.position -= new Vector3 (0, 1000, 0);
		if (!hasWon) {
			Application.LoadLevelAdditive (2);
			yield return new WaitForSeconds (1);
			GameObject.Find ("BlueStatusCam").SetActive (false);
			yield return new WaitForSeconds (1);
			GameObject.Find ("RedStatusCam").SetActive (false);
			yield return new WaitForSeconds (1);
			GameObject.Find ("WinScreen").SendMessage ("setWinScreen", "Red");
		}
		gameObject.SetActive (false);
	}

    void Hover () {
        if (Vector3.Distance (transform.position, new Vector3 (-16, 0)) > 0.001f) {
            Vector3 heading = new Vector3 (-16, 0) - transform.position;
            GetComponent<Rigidbody2D> ().AddForce (heading * 100);
        } else {
            GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, Random.value-0.5f).normalized*100);
        }
    }
}
