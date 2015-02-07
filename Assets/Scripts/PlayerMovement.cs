using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public float speed;

	// using Physics, so use Fixed Update
	void FixedUpdate() 
	{
		var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		// figure out your quaternion rotation
		Quaternion rot = Quaternion.LookRotation(transform.position - mousePosition, Vector3.forward);
		transform.rotation = rot;
		transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
		rigidbody2D.angularVelocity = 0;

		// if the player is pushing the up/down arrow
		// move it "forward" (in top-down...y axis)
		float input = System.Convert.ToBoolean(Input.GetAxis("Vertical")) ? Input.GetAxis ("Vertical") : 1;
		rigidbody2D.AddForce(gameObject.transform.up * speed * input);

	}
}
