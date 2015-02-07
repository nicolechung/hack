using UnityEngine;
using System.Collections;

public class GBFlee : MonoBehaviour {

	private bool flag; // make it only call once
	// Use this for initialization
	void Start () {
		flag = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (enabled && flag) {
			Debug.Log ("Flee is enabled");
			flag = false;
		}
	}
}
