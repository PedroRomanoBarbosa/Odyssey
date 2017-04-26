using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : FauxGravityBody {
	private Rigidbody rigidBody;

	// Movement Variables
	private Vector3 move;
	private Vector3 velocity;
	public float speed;
	public float rotationSpeed;
	public float maxClimbAngle;

	// Children Variables
	private Transform movementAxis;
	private Transform model;

	// Gravity Variables
	private bool planetGravity;
	private Vector3 gravityVector;

	// Jump Variables
	public bool isGrounded;
	private bool jumping;
	private Vector3 jumpingVelocity;
	public float jumpMomentum = 4f;
	private bool jumpPressed;
	private float jumpCounter;
	public float jumpSpeed;
	public float jumpDuration;
	public float aerialSlowDown;

	// Missile Variables
	private float missileCooldownCounter;
	public GameObject missilePrefab;
	public float missileCooldown;

	// Weapon variables
	public GameObject missileLauncher;
	public GameObject miningPick;
	private enum Weapon {
		MissileLauncher,
		MiningPick
	}
	private Weapon activeWeapon;
	private List<Weapon> equippedWeapons;
	private int weaponIndex;

	void Start () {
		movementAxis = transform.GetChild (0);
		model = transform.GetChild (1);
		rigidBody = GetComponent<Rigidbody> ();
		missileCooldownCounter = missileCooldown;
		planetGravity = true;
		gravityRotationSpeed = planetGravityRotationSpeed;
		isGrounded = false;
		activeWeapon = Weapon.MissileLauncher;
		equippedWeapons = new List<Weapon> { Weapon.MissileLauncher };
		weaponIndex = 0;
	}

	void Update () {
		if (!GameVariables.cinematicPaused) {
			CheckGrounded ();

			MovePlayer ();

			ChangeWeapon ();

			UseWeapon ();
		}
	}

	public void FixedUpdate () {
		rigidBody.velocity = Vector3.zero;
		if (!GameVariables.cinematicPaused) {
			if (planetGravity) {
				base.FixedUpdate ();
			} else {
				attractor.Attract (this, gravityVector, gravityRotationSpeed);
			}
			rigidBody.velocity += move;
		}
	}

	void CheckGrounded () {
		Debug.DrawRay (transform.position, -transform.up * 1.2f);
		if (Physics.Raycast (transform.position, -transform.up, 1.2f)) {
			isGrounded = true;
			jumpCounter = 0f;
			jumping = false;
		} else {
			isGrounded = false;
		}
	}

	void ChangeWeapon () {
		if (Input.GetKey("q")) {
			weaponIndex++;
			if (weaponIndex >= equippedWeapons.Count) {
				weaponIndex = 0;
			}
			activeWeapon = equippedWeapons[weaponIndex];
		}
	}

	void PositionWeapon (GameObject weapon) {
		switch (activeWeapon) {
		case Weapon.MissileLauncher:
			break;
		case Weapon.MiningPick:
			weapon.GetComponent<Rotation> ().enabled = false;
			weapon.transform.localScale = new Vector3 (1.5f, 1.5f, 1.5f);
			weapon.transform.parent = transform.GetChild (1);
			weapon.transform.up = transform.GetChild (1).up;
			weapon.transform.right = transform.GetChild (1).right;
			weapon.transform.Rotate (0f, 90f, 0f);
			weapon.transform.position = transform.GetChild (1).GetChild (2).position;
			break;
		}
	}

	void UseWeapon () {
		switch (activeWeapon) {
		case Weapon.MissileLauncher:
			FireMissile ();
			break;
		case Weapon.MiningPick:
			break;
		}
	}

	void FireMissile () {
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

	void MovePlayer () {
		movementAxis.Rotate (0f, Input.GetAxis ("Mouse X") * rotationSpeed, 0f);

		velocity = Vector3.zero;
		velocity += Input.GetAxis ("Vertical") * movementAxis.forward;
		velocity += Input.GetAxis ("Horizontal") * movementAxis.right;
		if (!isGrounded) {
			velocity *= aerialSlowDown;
		}
		move = Vector3.ClampMagnitude (velocity, 1f) * speed;

		if (move != Vector3.zero) {
			model.rotation = Quaternion.LookRotation (move, movementAxis.up);
		}

		if (!jumping) {
			if (Input.GetAxisRaw("Jump") == 1f) {
				jumping = true;
				jumpingVelocity = Vector3.ClampMagnitude(move, 1f);
				jumpCounter = 0f;
			}
		}
		if (jumping) {
			move = Vector3.zero;
			jumpCounter += Time.deltaTime;
			if (jumpCounter <= jumpDuration / 3f) {
				move += transform.up * jumpSpeed;
			} else if (jumpCounter <= jumpDuration) {
				if (Input.GetAxisRaw ("Jump") == 1f) {
					move += transform.up * jumpFunction(jumpCounter, jumpDuration);
				}
			}
			move += Input.GetAxis ("Vertical") * movementAxis.forward * speed * aerialSlowDown;
			move += Input.GetAxis ("Horizontal") * movementAxis.right * speed * aerialSlowDown;
			move += Vector3.ClampMagnitude((jumpingVelocity * jumpMomentum), (jumpingVelocity * jumpMomentum).magnitude - jumpCounter);
		}
	}

	float jumpFunction(float time, float dur) {
		float t = time / dur;
		float gravity = -4 * (t - 0.5f) * (t - 0.5f) + 1;
		return gravity * -attractor.gravity + -attractor.gravity;
	}

	void OnCollisionEnter (Collision collision) {
		if (collision.gameObject.name == "Planet") {
			if (rotationEnded) {
				planetGravity = true;
				gravityRotationSpeed = planetGravityRotationSpeed;
			}
		}
	}

	void OnCollisionStay(Collision collision) {
		/*
		Vector3 normal = collision.contacts[0].normal;
		Vector3 vel = rigidBody.transform.up;
		if (Vector3.Angle(vel, -normal) > maxClimbAngle) {
			if (jumping) {
				jumping = false;
				jumpCounter = 0f;
			} 
		}
		*/
	}

	void OnTriggerEnter(Collider collider) {
		if (collider.gameObject.CompareTag ("GravityZone")) {
			planetGravity = false;
			GravityZone script = collider.gameObject.GetComponent<GravityZone> ();
			gravityVector = script.transform.up;
			gravityRotationSpeed = script.gravityRotationSpeed;
		} else if (collider.gameObject.name == "MiningPick") {
			equippedWeapons.Add (Weapon.MiningPick);
			activeWeapon = Weapon.MiningPick;
			PositionWeapon (collider.gameObject);
		}
	}

	void OnTriggerExit(Collider collider) {
		if (collider.gameObject.CompareTag ("GravityZone")) {
			planetGravity = true;
		}
	}

}
