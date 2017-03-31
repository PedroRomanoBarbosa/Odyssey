using System.Collections;
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
	public float initialJumpSpeed;
	public float initialJumpDuration;
	public float jumpSpeed;
	public float jumpDuration;
	public float aerialSlowDown;
	public float rotationSpeed;
	public float gravity;
	public float missileCooldown;

	void Start () {
		rigidBody = GetComponent<Rigidbody> ();
		missileCooldownCounter = missileCooldown;
		canJump = true;
	}

	void Update () {
		if (!GameVariables.cinematicPaused) {
			MovePlayer ();
			FireMissile ();
		}
	}

	new void FixedUpdate() {
		if (!GameVariables.cinematicPaused) {
			base.FixedUpdate ();
			rigidBody.velocity = move * gravity;
		} else {
			rigidBody.velocity = Vector3.zero; 
		}
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
		Vector3 velocity = Vector3.zero;
		velocity += Input.GetAxis ("Vertical") * transform.forward;
		velocity += Input.GetAxis ("Horizontal") * transform.right;
		move = Vector3.ClampMagnitude (velocity, 1f) * speed;

		if (move != Vector3.zero) {
			transform.GetChild (0).rotation = Quaternion.LookRotation (move);
		}

		if (!jumping) {
			if (Input.GetKey ("space")) {
				jumping = true;
			}
		}
		if (jumping) {
			jumpCounter += Time.deltaTime;
			if (jumpCounter <= initialJumpDuration) {
				move += transform.up * initialJumpSpeed;
			} else if (Input.GetKey ("space") && jumpCounter <= jumpDuration + initialJumpDuration) {
				move += transform.up * jumpSpeed;
			}
			move += transform.right * aerialSlowDown;
			move += transform.forward * aerialSlowDown;
		}

		transform.GetChild (1).RotateAround (transform.position, transform.up, Input.GetAxis ("Mouse X") * rotationSpeed);
		if (Input.GetMouseButton (1)) {
			
		}
	}

	void OnCollisionStay(Collision collision) {
		Vector3 normal = collision.contacts[0].normal;
		Vector3 vel = rigidBody.transform.up;
		if (Vector3.Angle(vel, -normal) > maxClimbAngle) {
			canJump = true;
			jumpCounter = jumpDuration;
			if (jumping) {
				jumping = false;
				jumpCounter = 0;
			} 
		} else {
			canJump = false;
		}
	}

}
