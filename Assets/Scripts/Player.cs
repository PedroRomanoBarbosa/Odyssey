using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : FauxGravityBody {
	private Rigidbody rigidBody;

	// Movement Variables
	private Vector3 move;
	public float speed;
	private float planetSpeed;
	private float planetGravityRotationSpeed;
	public float gravity;
	public float rotationSpeed;
	public float maxClimbAngle;

	// Children Variables
	private Transform movementAxis;
	private Transform model;

	// Gravity Variables
	private bool planetGravity;
	private Vector3 gravityVector;

	// Jump Variables
	private bool jumping;
	private Vector3 jumpingVelocity;
	private bool jumpPressed;
	private float jumpCounter;
	public float initialJumpSpeed;
	public float initialJumpDuration;
	public float jumpSpeed;
	public float jumpDuration;
	public float aerialSlowDown;

	// Missile Variables
	private float missileCooldownCounter;
	public GameObject missilePrefab;
	public float missileCooldown;

	void Start () {
		movementAxis = transform.GetChild (0);
		model = transform.GetChild (1);
		rigidBody = GetComponent<Rigidbody> ();
		missileCooldownCounter = missileCooldown;
		planetSpeed = speed;
		planetGravityRotationSpeed = gravityRotationSpeed;
		planetGravity = true;
	}

	void Update () {
		if (!GameVariables.cinematicPaused) {
			MovePlayer ();
			FireMissile ();
		}
	}

	new void FixedUpdate() {
		if (!GameVariables.cinematicPaused) {
			if (planetGravity) {
				base.FixedUpdate ();
			} else {
				attractor.Attract (transform, gravityVector, gravityRotationSpeed);
			}
			rigidBody.velocity = move * gravity;
		} else {
			rigidBody.velocity = Vector3.zero; 
		}
	}

	void FireMissile() {
		if (missileCooldownCounter >= missileCooldown) {
			if (Input.GetAxisRaw("Fire1") == 1) {
				missileCooldownCounter = 0f;
				GameObject missile = Instantiate (missilePrefab, transform.position, Quaternion.identity);
				missile.transform.rotation = model.rotation;
				missile.transform.Rotate (90f, 0f, 0f);
				MissileMovement script = missile.GetComponent<MissileMovement> ();
				script.planet = attractor.gameObject;
				script.axis = model.right;
			}
		} else {
			missileCooldownCounter += Time.deltaTime;
		}
	}

	void MovePlayer() {
		movementAxis.Rotate (0f, Input.GetAxis ("Mouse X") * rotationSpeed, 0f);

		Vector3 velocity = Vector3.zero;
		velocity += Input.GetAxis ("Vertical") * movementAxis.forward;
		velocity += Input.GetAxis ("Horizontal") * movementAxis.right;
		move = Vector3.ClampMagnitude (velocity, 1f) * speed;

		if (move != Vector3.zero) {
			model.rotation = Quaternion.LookRotation (move, movementAxis.up);
		}

		if (!jumping) {
			if (Input.GetAxisRaw("Jump") == 1f) {
				jumping = true;
				jumpingVelocity = Vector3.ClampMagnitude(move, 1f);
			}
		}
		if (jumping) {
			move = jumpingVelocity;
			jumpCounter += Time.deltaTime;
			if (jumpCounter <= initialJumpDuration) {
				move += movementAxis.up * initialJumpSpeed;
			} else if (Input.GetAxisRaw ("Jump") == 1 && jumpCounter <= jumpDuration + initialJumpDuration) {
				move += movementAxis.up * jumpSpeed;
			} else if (jumpCounter > 2f * (jumpDuration + initialJumpDuration)) {
				jumping = false;
			}
		}
	}

	void OnCollisionStay(Collision collision) {
		Vector3 normal = collision.contacts[0].normal;
		Vector3 vel = rigidBody.transform.up;
		if (Vector3.Angle(vel, -normal) > maxClimbAngle) {
			jumpCounter = jumpDuration;
			if (jumping) {
				jumping = false;
				jumpCounter = 0f;
			} 
		}
	}

	void OnTriggerEnter(Collider collider) {
		if (collider.gameObject.CompareTag ("GravityZone")) {
			planetGravity = false;
			GravityZone script = collider.gameObject.GetComponent<GravityZone> ();
			gravityVector = script.transform.up;
			gravityRotationSpeed = script.gravityRotationSpeed;
			speed = script.speed;
		}
	}

	void OnTriggerExit(Collider collider) {
		if (collider.gameObject.CompareTag ("GravityZone")) {
			gravityRotationSpeed = planetGravityRotationSpeed;
			speed = planetSpeed;
			planetGravity = true;
		}
	}

}
