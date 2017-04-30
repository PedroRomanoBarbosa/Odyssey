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
	public bool planetGravity;
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

	// Weapon variables
	private Tool miningPick, missileLauncher;
	public List<Tool> equippedTools;
	private int toolIndex;

	void Start () {
		movementAxis = transform.GetChild (0);
		model = transform.GetChild (1);
		rigidBody = GetComponent<Rigidbody> ();
		planetGravity = true;
		isGrounded = false;
		miningPick = model.Find ("MiningPick").GetComponent<Tool> ();
		missileLauncher = model.Find ("MissileLauncher").GetComponent<Tool> ();
		equippedTools = new List<Tool> ();
		toolIndex = 0;
		if (equippedTools.Count > 0) {
			equippedTools [toolIndex].gameObject.SetActive (true);
		}
	}

	void Update () {
		if (!GameVariables.cinematicPaused) {
			CheckGrounded ();
			MovePlayer ();
			ChangeWeapon ();
		}
	}

	public void FixedUpdate () {
		rigidBody.velocity = Vector3.zero;
		if (!GameVariables.cinematicPaused) {
			if (planetGravity) {
				base.FixedUpdate ();
			} else {
				attractor.Attract (this, gravityVector);
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
		if (equippedTools.Count > 0) {
			if (Input.GetKeyUp("q")) {
				equippedTools [toolIndex].gameObject.SetActive (false);
				toolIndex++;
				if (toolIndex >= equippedTools.Count) {
					toolIndex = 0;
				}
				equippedTools [toolIndex].gameObject.SetActive (true);

			}
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

		Jump ();
	}

	void Jump() {
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

	void OnTriggerEnter(Collider collider) {
		GameObject colliderObject = collider.gameObject;
		if (colliderObject.CompareTag ("GravityZone")) {
			planetGravity = false;
			GravityZone script = colliderObject.GetComponent<GravityZone> ();
			gravityVector = script.transform.up;
			gravityRotationSpeed = script.gravityRotationSpeed;
		} else if (colliderObject.name == "MiningPickItem") {
			equippedTools.Add (miningPick);
			equippedTools [toolIndex].gameObject.SetActive (false);
			toolIndex = equippedTools.Count - 1;
			equippedTools [toolIndex].gameObject.SetActive (true);
			Destroy (colliderObject);
		}
	}

	void OnTriggerExit(Collider collider) {
		if (collider.gameObject.CompareTag ("GravityZone")) {
			planetGravity = true;
		}
	}

}
