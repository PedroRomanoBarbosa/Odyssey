using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : FauxGravityBody {
	private Rigidbody rigidBody;
	private Vector3 move;
	private float missileCooldownCounter;

	public GameObject missilePrefab;
	public float jumpSpeed;
	public float rotationSpeed;
	public float gravity;
	public float missileCooldown;

	void Start () {
		rigidBody = GetComponent<Rigidbody> ();
		missileCooldownCounter = missileCooldown;
	}

	void Update () {
		MovePlayer ();
		if (missileCooldownCounter >= missileCooldown) {
			if (Input.GetMouseButtonDown (0)) {
				missileCooldownCounter = 0;
				FireMissile ();
			}
		} else {
			missileCooldownCounter += Time.deltaTime;
		}
	}

	new void FixedUpdate() {
		base.FixedUpdate ();
		rigidBody.MovePosition (rigidBody.position + transform.TransformDirection(move) * gravity * Time.fixedDeltaTime);
	}

	void FireMissile() {
		GameObject missile = Instantiate (missilePrefab, transform.position, Quaternion.identity);
		missile.transform.rotation = transform.rotation;
		missile.transform.Rotate (90, 0, 0);
		MissileMovement script = missile.GetComponent<MissileMovement> ();
		script.planet = attractor.gameObject;
		script.axis = transform.right;
	}

	void MovePlayer() {
		move = new Vector3 (Input.GetAxisRaw("Horizontal"), Input.GetAxis("Jump"), Input.GetAxisRaw("Vertical")).normalized;
		transform.Rotate (0, Input.GetAxis ("Mouse X") * rotationSpeed, 0);
	}
}
