using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : FauxGravityBody {
	private Rigidbody rigidBody;
	private Vector3 move;

	public GameObject missilePrefab;
	public float jumpSpeed;
	public float rotationSpeed;
	public float gravity;

	void Start () {
		rigidBody = GetComponent<Rigidbody> ();
	}

	void Update () {
		move = new Vector3 (Input.GetAxisRaw("Horizontal"), Input.GetAxis("Jump"), Input.GetAxisRaw("Vertical")).normalized;
		transform.Rotate (0, Input.GetAxis ("Mouse X") * rotationSpeed, 0);
		if (Input.GetMouseButtonDown(0)) {
			GameObject missile = Instantiate (missilePrefab, transform.position, Quaternion.identity);
			missile.transform.Rotate (90, 0, 0);
			MissileMovement script = missile.GetComponent<MissileMovement> ();
			script.center = attractor.transform;
			script.axis = transform.right;
		}
	}

	new void FixedUpdate() {
		base.FixedUpdate ();
		rigidBody.MovePosition (rigidBody.position + transform.TransformDirection(move) * gravity * Time.fixedDeltaTime);
	}
}
