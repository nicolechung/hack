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
		InvokeRepeating ("SpawnTowelMan", spawnDelay, spawnTime);
	}
	
	// Update is called once per frame
	void Update () {
		if (amount == 0) {
			CancelInvoke ();
		}
	}

	void SpawnTowelMan() {
		var x1 = transform.position.x - renderer.bounds.size.x/2;
		var x2 = transform.position.x + renderer.bounds.size.x/2;
		
		var y1 = transform.position.y - renderer.bounds.size.y/2;
		var y2 = transform.position.y + renderer.bounds.size.y/2;
		
		var spawnpoint = new Vector2(Random.Range (x1, x2), Random.Range (y1, y2));
		
		Instantiate (towelMan, spawnpoint, transform.rotation);
		
		amount--;
	}
}
