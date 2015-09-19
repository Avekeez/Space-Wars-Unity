using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject statusRed;
	public GameObject statusBlue;

	public GameObject blue;
	public GameObject red;

	void Awake () {
		statusBlue.SetActive (false);
		statusRed.SetActive (false);
	}
	void allowControl () {
		statusBlue.SetActive (true);
		statusRed.SetActive (true);
	}
    void Update () {
        GetComponent<AudioSource> ().volume = 0.1f * Global.stat.MusicModifier;
    }
}