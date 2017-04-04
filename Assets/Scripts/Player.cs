using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : FauxGravityBody {
	private Rigidbody rigidBody;

	// Movement Variables
	private Vector3 move;
	public float speed;
	public float gravity;
	public float rotationSpeed;
	public float maxClimbAngle;

	// Children Variables
	private Transform movementAxis;
	private Transform model;
	private Transform playerCamera;

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
		playerCamera = movementAxis.GetChild (0);
		rigidBody = GetComponent<Rigidbody> ();
		missileCooldownCounter = missileCooldown;
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
				missileCooldownCounter = 0;
				GameObject missile = Instantiate (missilePrefab, transform.position, Quaternion.identity);
				missile.transform.rotation = model.rotation;
				missile.transform.Rotate (90, 0, 0);
				MissileMovement script = missile.GetComponent<MissileMovement> ();
				script.planet = attractor.gameObject;
				script.axis = model.right;
			}
		} else {
			missileCooldownCounter += Time.deltaTime;
		}
	}

	void MovePlayer() {
		movementAxis.Rotate (0, Input.GetAxis ("Mouse X") * rotationSpeed, 0);

		Vector3 velocity = Vector3.zero;
		velocity += Input.GetAxis ("Vertical") * movementAxis.forward;
		velocity += Input.GetAxis ("Horizontal") * movementAxis.right;
		move = Vector3.ClampMagnitude (velocity, 1f) * speed;

		if (move != Vector3.zero) {
			model.rotation = Quaternion.LookRotation (move, movementAxis.up);
		}

		if (!jumping) {
			if (Input.GetAxisRaw("Jump") == 1) {
				jumping = true;
			}
		}
		if (jumping) {
			jumpCounter += Time.deltaTime;
			if (jumpCounter <= initialJumpDuration) {
				move += movementAxis.up * initialJumpSpeed;
			} else if (Input.GetAxisRaw("Jump") == 1 && jumpCounter <= jumpDuration + initialJumpDuration) {
				move += movementAxis.up * jumpSpeed;
			}
			move += movementAxis.right * aerialSlowDown;
			move += movementAxis.forward * aerialSlowDown;
		}
	}

	void OnCollisionStay(Collision collision) {
		Vector3 normal = collision.contacts[0].normal;
		Vector3 vel = rigidBody.transform.up;
		if (Vector3.Angle(vel, -normal) > maxClimbAngle) {
			jumpCounter = jumpDuration;
			if (jumping) {
				jumping = false;
				jumpCounter = 0;
			} 
		}
	}

	void OnTriggerEnter(Collider collider) {
		planetGravity = false;
		gravityVector = Vector3.up;
		gravityRotationSpeed = 5;
	}

	void OnTriggerExit(Collider collider) {
		planetGravity = true;
		gravityRotationSpeed = 2;
	}

}
