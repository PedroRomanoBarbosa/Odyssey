using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
	public Transform center;
	private Rigidbody rigidBody;
	private Vector3 move;
	private float jumpVel;
	private bool stopMoving;

	void Start () {
		rigidBody = GetComponent<Rigidbody> ();
		jumpVel = 0f;
		stopMoving = false;
	}

	void Update () {
		if (Input.GetKeyDown("space")) {
			jumpVel = 4;
		}
		move = new Vector3 (Input.GetAxisRaw("Horizontal"), jumpVel, Input.GetAxisRaw("Vertical")).normalized;
	}

	void FixedUpdate (){
		if (!stopMoving) {
			rigidBody.MovePosition (rigidBody.position + transform.TransformDirection(move) * 10 * Time.deltaTime);
		}
	}

	void OnCollisionEnter () {
		Debug.Log ("ENTER");
		jumpVel = 0;
	}

	void OnCollisionExit (){
		Debug.Log ("EXIT");
	}

}
