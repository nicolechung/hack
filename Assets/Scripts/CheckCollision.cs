using UnityEngine;
using System.Collections;

public class CheckCollision : MonoBehaviour {

	private bool checkCollision = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void checkForCollision() {
		Debug.Log("check for collision");
		checkCollision = true;
	}

	public void turnOffCollisionCheck() {
		Debug.Log ("turning off collision check");
		checkCollision = false;
	}

	void OnTriggerEnter2D() {
		if (checkCollision) {
			Debug.Log ("something is in the Trigger zone");
		}
	}
}
