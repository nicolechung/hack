using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public float spawnTime = 5f;
	public float spawnDelay = 3f;
	public GameObject[] towelMen; /* guys on the prowl... */
	// Use this for initialization
	void Start () {
		Debug.Log ("starting spawn");
		// start calling the spawn function repeatedly after delay
		InvokeRepeating ("Spawn", spawnDelay, spawnTime);
	}
	
	// Update is called once per frame
	void Update () {
		// instantiate a random enemy
		int towelMenIndex = Random.Range (0, towelMen.Length);
		Instantiate (towelMen[towelMenIndex], transform.position, transform.rotation);
	}
}
