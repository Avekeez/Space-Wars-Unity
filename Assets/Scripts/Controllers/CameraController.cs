using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject statusRed;
	public GameObject statusBlue;

	public GameObject blue;
	public GameObject red;

	void Start () {
		statusBlue.SetActive (false);
		statusRed.SetActive (false);
	}
	void allowControl () {
		statusBlue.SetActive (true);
		statusRed.SetActive (true);
	}
}