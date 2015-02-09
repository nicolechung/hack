using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public float speed;
	public string state = "go";
	private string lockState = "not-locked";
	protected Animator animator;

	private CheckCollision _lockColliderObj;

	void Start() 
	{
		animator = GetComponent<Animator>();
	}

	// using Physics, so use Fixed Update
	void FixedUpdate() 
	{
		if (state == "go") {
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

	// keyboard

	void Update() {

		if ( (Input.GetKeyDown (KeyCode.LeftShift) || Input.GetKeyDown (KeyCode.RightShift) ) ) {
			if (state=="go") {
				rigidbody2D.velocity = Vector2.zero;
				rigidbody2D.angularVelocity = 0;
				rigidbody2D.Sleep();
				state = "stop";
			} else {
				state="go";
			}
		} 

		if ( Input.GetKeyDown (KeyCode.Space) ) {
			if (lockState == "not-locked") {
				animator.SetBool ("lock", true);
				BroadcastMessage("checkForCollision");
				lockState = "locked";
			} else {
				animator.SetBool ("lock", false);
				BroadcastMessage("turnOffCollisionCheck");
				lockState = "not-locked";
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

	void HaltActivity()
	{
		StartCoroutine( DoTheDance() );
	}

	private IEnumerator DoTheDance()
	{
		rigidbody2D.velocity = Vector2.zero;
		rigidbody2D.angularVelocity = 0;
		rigidbody2D.Sleep();
		state = "stop";
		yield return new WaitForSeconds(10);
		state = "go";
	}




	void StopMovement() {
		rigidbody2D.velocity = Vector2.zero;
		rigidbody2D.angularVelocity = 0;
		rigidbody2D.Sleep();
	}
}
