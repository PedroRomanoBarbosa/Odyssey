﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : FauxGravityBody {
	private Rigidbody rigidBody;
	private Vector3 move;
	private bool canJump;
	private bool jumping;
	private bool jumpPressed;
	private float jumpCounter;
	private float missileCooldownCounter;

	public GameObject missilePrefab;
	public float speed;
	public float maxClimbAngle;
	public float jumpSpeed;
	public float jumpDuration;
	public float rotationSpeed;
	public float gravity;
	public float missileCooldown;

	void Start () {
		rigidBody = GetComponent<Rigidbody> ();
		missileCooldownCounter = missileCooldown;
		canJump = true;
	}

	void Update () {
		MovePlayer ();
		FireMissile ();
	}

	new void FixedUpdate() {
		base.FixedUpdate ();
		rigidBody.MovePosition (rigidBody.position + transform.TransformDirection(move) * gravity * Time.fixedDeltaTime);
	}

	void FireMissile() {
		if (missileCooldownCounter >= missileCooldown) {
			if (Input.GetMouseButtonDown (0)) {
				missileCooldownCounter = 0;
				GameObject missile = Instantiate (missilePrefab, transform.position, Quaternion.identity);
				missile.transform.rotation = transform.rotation;
				missile.transform.Rotate (90, 0, 0);
				MissileMovement script = missile.GetComponent<MissileMovement> ();
				script.planet = attractor.gameObject;
				script.axis = transform.right;
			}
		} else {
			missileCooldownCounter += Time.deltaTime;
		}
	}

	void MovePlayer() {
		float jumpValue = 0;
		if (canJump) {
			jumpValue = Input.GetAxis ("Jump") * jumpSpeed;
		}
		move += new Vector3 (Input.GetAxis ("Horizontal") * speed, jumpValue, Input.GetAxis ("Vertical") * speed);
		move *= Time.deltaTime;
		transform.Rotate (0, Input.GetAxis ("Mouse X") * rotationSpeed, 0);
	}

	void OnTriggerEnter(Collider collider) {
		
	}

	void OnCollisionStay(Collision collision) {
		Vector3 normal = collision.contacts[0].normal;
		Vector3 vel = rigidBody.transform.up;
		if (Vector3.Angle(vel, -normal) > maxClimbAngle){
			canJump = true;
		} else {
			canJump = false;
		}
	}

}
