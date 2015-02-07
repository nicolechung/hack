using UnityEngine;
using System.Collections;

public class GBWander : MonoBehaviour {

	private bool flag; // make it only call once

	public float collisionDistance = 2;
	public float speed = 50;

	private static bool DEBUG = true;
	private static bool DEBUG_DRAW = false;

	public int LayerToMask = 5; /* other towel men on this layer, they avoid each other */
	public int straightAhead = 10; // how much to go in a given direction before turning
	private int count;
	private float obstacleRange = 0.3f; /* range of the angle to check for obstacles */
	private string state; /* "wander", "start-rotation", "rotating" */

	// Use this for initialization
	void Start () {
		flag = true;
		state = "wander";
		count = straightAhead;
		StartCoroutine( doWander() );
	}

	// Wander behaviour
	IEnumerator doWander() {
		while (state=="wander" && count > 0) {
			if (DEBUG) Debug.Log (" doing the wander ");
			bool obstacles;
			Vector2 direction = transform.up;

			obstacles = checkForObstacles(direction);
			if (DEBUG) Debug.Log ("obstacles");
			if (DEBUG) Debug.Log (obstacles);
			if (!obstacles) {
				if (DEBUG) Debug.Log ("moving forward");
				transform.Translate(Vector2.up * speed * Time.smoothDeltaTime);
			} else {
				if (DEBUG) Debug.Log ("rotating");
				state = "start-rotation";
				yield return null;
			}

			count--;
			if (count == 0) state = "start-rotation";
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
	void Update () {

		if (enabled && flag) {
			Debug.Log ("Wander is enabled");
			flag = false;
		}

		if (enabled) {
			if (DEBUG) Debug.Log ("state: " + state);
			switch(state) {
			case "start-rotation":
				state = "rotate";
				count = straightAhead;
				StartCoroutine ( rotatePlayer () );
				break;
			}
		}
	}
}
