using UnityEngine;
using System.Collections;

public class GBWander : MonoBehaviour {

	private bool flag; // make it only call once

	public float collisionDistance = 10;
	public float speed = 1;

	private static bool DEBUG = false;

	public int LayerToMask = 5; /* other towel men on this layer, they avoid each other */
	private float obstacleRange = 0.3f; /* range of the angle to check for obstacles */
	private string state; /* "wander", "start-rotation", "rotating" */

	// Use this for initialization
	void Start () {
		flag = true;
		state = "start-rotation";
		StartCoroutine( doWander() );
	}

	// Wander behaviour
	IEnumerator doWander() {
		while (state=="wander") {
			if (DEBUG) Debug.Log (" doing the wander ");
			bool obstacles;
			Vector2 direction = transform.up;

			obstacles = checkForObstacles(direction);
			if (DEBUG) Debug.Log ("obstacles");
			if (DEBUG) Debug.Log (obstacles);
			if (obstacles == true) {
				if (DEBUG) Debug.Log ("rotating");
				state = "start-rotation";
				yield return null;
			}
			yield return null;
		}
	}



	// check for obstacles in the way
	bool checkForObstacles(Vector2 direction) {
		RaycastHit2D[] hits;
		RaycastHit2D[] hitsLeft;
		RaycastHit2D[] hitsRight;

		Vector2 directionLeft;
		Vector2 directionRight;

		bool hasObstacle = false; // bool to return

		//
		hits = Physics2D.RaycastAll(transform.position, direction, collisionDistance, 1 << LayerToMask);

		// left
		Vector2 left = new Vector2(obstacleRange, 0);
		Vector2 leftOrigin = new Vector2(transform.position.x, transform.position.y) + left;
		directionLeft = direction + left;
		hitsLeft = Physics2D.RaycastAll(leftOrigin, directionLeft, collisionDistance, 1 << LayerToMask);

		// right
		Vector2 right = new Vector2(obstacleRange, 0);
		Vector2 rightOrigin = new Vector2(transform.position.x, transform.position.y) + right;
		directionRight = direction + right;
		hitsRight= Physics2D.RaycastAll(rightOrigin, directionRight, collisionDistance, 1 << LayerToMask);

		foreach(RaycastHit2D hit in hits) {
			if (hit && hit.collider) {
				hasObstacle = true;
				return hasObstacle;
			}
		}

		foreach(RaycastHit2D hit in hitsLeft) {
			if (hit && hit.collider) {
				hasObstacle = true;
				return hasObstacle;
			}
		}

		foreach(RaycastHit2D hit in hitsRight) {
			if (hit && hit.collider) {
				hasObstacle = true;
				return hasObstacle;
			}
		}

		return hasObstacle;
	}

	//
	IEnumerator rotatePlayer() {
		while(state == "rotating") {
			transform.rotation = Random.rotation;
			/* set rotation to only be on the z-axis for 2D, otherwise object will look paper-thin */
			transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
			state = "wander";
			StartCoroutine( doWander() );
			yield return null;
		}
	}

	// Update is called once per frame
	void FixedUpdate () {

		if (enabled && flag) {
			if (DEBUG) Debug.Log ("Wander is enabled");
			flag = false;
		}

		if (enabled) {
			if (DEBUG) Debug.Log ("state: " + state);
			switch(state) {
			case "start-rotation":
				state = "rotating";
				StartCoroutine ( rotatePlayer () );
				break;
			case "wander":
				rigidbody2D.AddForce(gameObject.transform.up * speed);
				break;
			}
		}
	}

	// set up Event Listeners
	void onEnable() 
	{

	}
	
	// remove Event Listeners
	void onDisable() 
	{
		
	}
	
	// remove Event Listeners
	void onDestroy() 
	{
		
	}
}
