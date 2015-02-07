using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public float spawnTime = 50f;
	public float spawnDelay = 50f;
	public int amount = 10;
	public TowelMan towelMan;

	// Use this for initialization
	void Start () {
		Debug.Log ("starting spawn");

	}
	
	// Update is called once per frame
	void Update () {
		while(amount > 0) {
			// instantiate a random enemy
			// TODO set up a spawn point to randomize the instantiation location

			Instantiate (towelMan, transform.position, transform.rotation);

			amount--;
		}

	}
}
