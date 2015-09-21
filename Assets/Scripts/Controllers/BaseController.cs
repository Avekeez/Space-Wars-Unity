using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BaseController : MonoBehaviour {
    #region Declaration
    public string Team;

    public GameObject Opponent;

    public GameObject Suicide;
    public GameObject Shooter;
    public GameObject Blocker;

    public List<bool> selectedLanes = new List<bool> () { false, false, false };

    public float SuicideCooldown = 0.5f;
    public float ShooterCooldown = 1;
    public float BlockerCooldown = 2;

    private bool canBuildSuicide = true;
    private bool canBuildShooter = true;
    private bool canBuildBlocker = true;
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

    public List<GameObject> AllShips;

    public GameObject boom;
    public GameObject cannon;
    private bool inDeathSequence;

    public bool hasWon;

    public List<KeyCode> keys;
    #endregion

    void Awake () {
        hasWon = false;
        inDeathSequence = false;
        money = moneyMax / 10;
        health = maxHealth;
        canControl = false;
        AllSuicide = new List<GameObject> ();
        for (int i = 0; i < maxSuicide; i++) {
            GameObject obj = (GameObject) Instantiate (Suicide);
            obj.SetActive (false);
            AllSuicide.Add (obj);
        }
        AllShooter = new List<GameObject> ();
        for (int i = 0; i < maxShooter; i++) {
            GameObject obj = (GameObject) Instantiate (Shooter);
            obj.SetActive (false);
            AllShooter.Add (obj);
        }
        AllBlocker = new List<GameObject> ();
        for (int i = 0; i < maxBlocker; i++) {
            GameObject obj = (GameObject) Instantiate (Blocker);
            obj.SetActive (false);
            AllBlocker.Add (obj);
        }

        if (GetComponent<BlueController> () != null) {
            keys = GetComponent<BlueController> ().keys;
        } else if (GetComponent<RedController> () != null) {
            keys = GetComponent<RedController> ().keys;
        }
        Debug.Log (transform.position);
    }

    void Update () {
        if (canControl) {
            updateLane ();
            updateMoney ();
            if (money >= 10) {
                if (Input.GetKeyDown (keys[0]) && canBuildSuicide && activeSuicide < maxSuicide) {
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
                    playCreateSound ();
                    StartCoroutine (SuiWait (totalShips));
                }
                if (Input.GetKeyDown (keys[1]) && canBuildShooter && activeShooter < maxShooter) {
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
                    playCreateSound ();
                    StartCoroutine (ShootWait (totalShips));
                }
                if (Input.GetKeyDown (keys[2]) && canBuildBlocker && activeBlocker < maxBlocker) {
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
                    playCreateSound ();
                    StartCoroutine (BlockWait (totalShips));
                }
                if (Input.GetKey (keys[6])) {
                    if (Input.GetKeyDown (keys[3])) {
                        cannon.GetComponent<LaserLogic> ().lane = 1;
                        Instantiate (cannon, transform.position, Quaternion.identity);
                    }
                    if (Input.GetKeyDown (keys[4])) {
                        cannon.GetComponent<LaserLogic> ().lane = 2;
                        Instantiate (cannon, transform.position, Quaternion.identity);
                    }
                    if (Input.GetKeyDown (keys[5])) {
                        cannon.GetComponent<LaserLogic> ().lane = 3;
                        Instantiate (cannon, transform.position, Quaternion.identity);
                    }
                }
                if (Input.GetKeyDown (keys[7])) {
                    if (GetComponent<MissileManager> ().GetMissile () != null) {
                    GetComponent<MissileManager> ().Launch ();
                    }
                }
            }
        }
        if (health <= 0 && !inDeathSequence) {
            inDeathSequence = true;
            if (!hasWon)
                Opponent.GetComponent<BaseController> ().hasWon = true;
            StartCoroutine (die ());
        }
        GetComponent<AudioSource> ().volume = 0.1f * Global.stat.SoundModifier;
    }

    void updateLane () {
        if (Input.GetKeyDown (keys[3])) {
            selectedLanes[0] = !selectedLanes[0];
        }
        if (Input.GetKeyDown (keys[4])) {
            selectedLanes[1] = !selectedLanes[1];
        }
        if (Input.GetKeyDown (keys[5])) {
            selectedLanes[2] = !selectedLanes[2];
        }
    }

    void updateMoney () {
        if (canGiveMoney && money < moneyMax) {
            canGiveMoney = false;
            money++;
            StartCoroutine (giveMoney ());
        }
    }
    
    public IEnumerator giveMoney () {
        yield return new WaitForSeconds (giveMoneyCooldown);
        canGiveMoney = true;
    }
    public void allowControl (bool state) {
        canControl = state;
    }
    public int getMaxHealth () {
        return maxHealth;
    }

    private void playCreateSound () {
        gameObject.GetComponent<AudioSource> ().Play ();
    }

    #region waitFunctions
    public IEnumerator SuiWait (int shipsBuilt) {
        canBuildSuicide = false;
        yield return new WaitForSeconds (SuicideCooldown * shipsBuilt);
        canBuildSuicide = true;
    }
    public IEnumerator ShootWait (int shipsBuilt) {
        canBuildShooter = false;
        yield return new WaitForSeconds (ShooterCooldown * shipsBuilt);
        canBuildShooter = true;
    }
    public IEnumerator BlockWait (int shipsBuilt) {
        canBuildBlocker = false;
        yield return new WaitForSeconds (BlockerCooldown * shipsBuilt);
        canBuildBlocker = true;
    }
    #endregion

    #region buildFunctions
    private void buildSuicide (int lane) {
        for (int i = 0; i < AllSuicide.Count; i++) {
            if (!AllSuicide[i].activeInHierarchy) {
                AllSuicide[i].GetComponent<BaseShip> ().lane = lane;
                AllSuicide[i].GetComponent<BaseShip> ().team = Team;
                AllSuicide[i].SetActive (true);
                activeSuicide++;
                break;
            }
        }
    }
    private void buildShooter (int lane) {
        for (int i = 0; i < AllShooter.Count; i++) {
            if (!AllShooter[i].activeInHierarchy) {
                AllShooter[i].GetComponent<BaseShip> ().lane = lane;
                AllShooter[i].GetComponent<BaseShip> ().team = Team;
                AllShooter[i].SetActive (true);
                activeShooter++;
                break;
            }
        }
    }
    private void buildBlocker (int lane) {
        for (int i = 0; i < AllBlocker.Count; i++) {
            if (!AllBlocker[i].activeInHierarchy) {
                AllBlocker[i].GetComponent<BaseShip> ().lane = lane;
                AllBlocker[i].GetComponent<BaseShip> ().team = Team;
                AllBlocker[i].SetActive (true);
                activeBlocker++;
                break;
            }
        }
    }
    #endregion

    IEnumerator buildSuicideSquadron () {
        buildSuicide (2);
        yield return new WaitForSeconds (0.1f * Random.value);
        buildSuicide (1);
        yield return new WaitForSeconds (0.1f * Random.value);
        buildSuicide (3);
    }
    IEnumerator buildShooterSquadron () {
        buildShooter (2);
        yield return new WaitForSeconds (0.1f * Random.value);
        buildShooter (1);
        yield return new WaitForSeconds (0.1f * Random.value);
        buildShooter (3);
    }
    IEnumerator buildBlockerSquadron () {
        buildBlocker (2);
        yield return new WaitForSeconds (0.1f * Random.value);
        buildBlocker (1);
        yield return new WaitForSeconds (0.1f * Random.value);
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
            GameObject.Find (Team + "StatusCam").SetActive (false);
            yield return new WaitForSeconds (1);
            GameObject.Find (Opponent.GetComponent<BaseController> ().Team + "StatusCam").SetActive (false);
            yield return new WaitForSeconds (1);
            GameObject.Find ("WinScreen").SendMessage ("setWinScreen", Opponent.GetComponent<BaseController> ().Team);
        }
        gameObject.SetActive (false);
    }
}
