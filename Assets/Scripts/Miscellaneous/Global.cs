using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Global : MonoBehaviour {
	public static Global stat;
	public GameObject boom;

	#region Stats
	public int BlueTotalMoney;
	public int BlueSuicideSpawned;
	public int BlueShooterSpawned;
	public int BlueBlockerSpawned;

	public int RedTotalMoney;
	public int RedSuicideSpawned;
	public int RedShooterSpawned;
	public int RedBlockerSpawned;
    #endregion

    public int MusicModifier;
    public int SoundModifier;

	public List<int> Stats = new List<int> () {0, 0, 0, 0, 0, 0, 0, 0};

	void Awake () {
		Object.DontDestroyOnLoad (gameObject);
		stat = this;
	}
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.Quit();
		}
        if (Input.GetKey (KeyCode.BackQuote)) {
            GameObject.Find ("BlueBase").GetComponent<BaseController> ().money += 100;
            GameObject.Find ("RedBase").GetComponent<BaseController> ().money += 100;
        }
		Stats [0] = BlueSuicideSpawned;
		Stats [1] = BlueShooterSpawned;
		Stats [2] = BlueBlockerSpawned;

		Stats [4] = RedSuicideSpawned;
		Stats [5] = RedShooterSpawned;
		Stats [6] = RedBlockerSpawned;
	}
}