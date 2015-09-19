using UnityEngine;
using System.Collections;

public class SetTextLayer : MonoBehaviour {
	
	void Start () {
		gameObject.GetComponent<MeshRenderer> ().sortingLayerName = "Gui";
		gameObject.GetComponent<MeshRenderer> ().sortingOrder = 11;
	}
}
