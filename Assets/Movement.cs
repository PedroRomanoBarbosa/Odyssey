using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
	private Rigidbody rigidBody;
	private Vector3 move;

	public float jumpSpeed;
	public float rotationSpeed;
	public float gravity;

	void Start () {
		rigidBody = GetComponent<Rigidbody> ();
	}

	void Update () {
		move = new Vector3 (Input.GetAxisRaw("Horizontal"), Input.GetAxis("Jump"), Input.GetAxisRaw("Vertical")).normalized;
		transform.Rotate (0, Input.GetAxis("Mouse X") * rotationSpeed, 0);
	}

	void FixedUpdate () {
		rigidBody.MovePosition (rigidBody.position + transform.TransformDirection(move) * gravity * Time.fixedDeltaTime);
	}

}
