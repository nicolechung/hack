using UnityEngine;
using System.Collections;

public class TowelMan : MonoBehaviour {

	public float speed;

	// states
	private bool isChasing;
	private bool isLoving;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Love() {
		// go to shower zone
		Debug.Log ("loving behaviour");
	}

	void Chase() {
		// seek 
		Debug.Log ("seek behaviour");
	}
}
