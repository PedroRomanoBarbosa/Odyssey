using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : FauxGravityBody {
	private Rigidbody rigidBody;

	// Other
	public Animator animator;
	private float damageCounter;
	private bool damaged;
	public float damageDuration;
	public Transform detectionAnchor;
	private bool inputActive;

	// Status
	public int lives;
	public int maxLives;
	public int energy;

	// Movement Variables
	private float baseSpeed;
	private Vector3 move;
	private Vector3 velocity;
	private float speedCounter;
	private bool speeding;
	public float speed;
	public float speedingCooldown;
	public float rotationSpeed;
	public float maxClimbAngle;
	public GameObject speedTrail;

	// Children Variables
	private Transform movementAxis;
	private Transform model;

	// Gravity Variables
	public bool planetGravity;
	public int gravityZoneCounter;
	private Vector3 gravityVector;

	// Jump Variables
	private bool isGroundedLastFrame;
	public bool isGrounded;
	public bool jumping;
	private Vector3 jumpingVelocity;
	public float jumpMomentum = 4f;
	private bool jumpPressed;
	private float jumpCounter;
	public float jumpSpeed;
	public float jumpDuration;
	public float aerialSlowDown;

	// Weapon variables
	public Tool[] tools;
	public List<Tool> equippedTools;
	private int toolIndex;

    //Planet Interface
    public Text live;

	// Sound Variables
	private AudioSource[] audioSources;

	void Start () {
		baseSpeed = speed;
		audioSources = GetComponents<AudioSource> ();
		movementAxis = transform.GetChild (0);
		model = transform.GetChild (1);
		rigidBody = GetComponent<Rigidbody> ();
		planetGravity = true;
		gravityZoneCounter = 0;
		isGrounded = false;
		equippedTools = new List<Tool> ();
		for (int i = 0; i < GameVariables.tools.Length; i++) {
			if (GameVariables.tools [i]) {
				equippedTools.Add (tools[i]);
			}
		}
		toolIndex = 0;
		if (equippedTools.Count > 0) {
			equippedTools [toolIndex].gameObject.SetActive (true);
		}
		inputActive = true;
	}

	void Update () {
		if (!GameVariables.cinematicPaused) {
			CheckGrounded ();
			MovePlayer ();
			ChangeWeapon ();
            UpdateUIText();
			SpeedingLoop ();
		}
		DamageLoop ();
	}

    void UpdateUIText()
    {
        //live.text = "X " + lives;
    }

	public new void FixedUpdate () {
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

	void DamageLoop () {
		if (damaged) {
			if (damageCounter >= damageDuration) {
				ChangeColor (false);
				damaged = false;
			} else {
				damageCounter += Time.deltaTime;
			}
		}
	}

	void SpeedingLoop() {
		if (speeding) {
			if (speedCounter >= speedingCooldown) {
				speeding = false;
				speedTrail.SetActive (false);
				speed = baseSpeed;
			} else {
				speedCounter += Time.deltaTime;
			}
		}
	}

	void CheckGrounded () {
		Debug.DrawRay (transform.position, -transform.up * 1.2f);
		if (Physics.Raycast (transform.position, -transform.up, 1.5f)) {
			isGrounded = true;
			if (isGroundedLastFrame != isGrounded) {
				audioSources [2].Play ();
			}
			isGroundedLastFrame = isGrounded;
			jumpCounter = 0f;
			jumping = false;
			animator.SetBool ("Jump", false);
		} else {
			isGrounded = false;
			isGroundedLastFrame = isGrounded;
		}
	}

	void ChangeWeapon () {
		if (equippedTools.Count > 1) {
			if (Input.GetKeyUp("q") && inputActive) {
				Tool currentTool = equippedTools [toolIndex];
				currentTool.Stop ();
				currentTool.gameObject.SetActive (false);
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
		if (inputActive) {
			velocity += Input.GetAxis ("Vertical") * movementAxis.forward;
			velocity += Input.GetAxis ("Horizontal") * movementAxis.right;
		}
		if (!isGrounded) {
			velocity *= aerialSlowDown;
		}
		move = Vector3.ClampMagnitude (velocity, 1f) * speed;
		animator.SetFloat ("Speed", move.magnitude / speed);
		if (move != Vector3.zero) {
			model.rotation = Quaternion.LookRotation (move, movementAxis.up);
		}
		Jump ();
		if (!jumping && !isGrounded) {
			animator.SetBool ("Jump", true);
			move += -movementAxis.up * 10f;
		}
	}

	void Jump() {
		if (!jumping && isGrounded) {
			if (Input.GetAxisRaw("Jump") == 1f && inputActive) {
				animator.SetBool ("Jump", true);
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
				if (Input.GetAxisRaw ("Jump") == 1f  && inputActive) {
					move += transform.up * jumpFunction (jumpCounter, jumpDuration);
				}
			} else {
				move += -movementAxis.up * 10f;
			}
			if (inputActive) {
				move += Input.GetAxis ("Vertical") * movementAxis.forward * speed * aerialSlowDown;
				move += Input.GetAxis ("Horizontal") * movementAxis.right * speed * aerialSlowDown;
			}
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
		if (colliderObject.name == "MiningPickItem") {
			equippedTools.Add (tools [(int)GameVariables.Tools.MiningPick]);
			equippedTools [toolIndex].gameObject.SetActive (false);
			toolIndex = equippedTools.Count - 1;
			equippedTools [toolIndex].gameObject.SetActive (true);
			Destroy (colliderObject);
			audioSources [4].Play ();
		} else if (colliderObject.name == "MissileLauncherItem") {
			equippedTools.Add (tools [(int)GameVariables.Tools.MissileLauncher]);
			equippedTools [toolIndex].gameObject.SetActive (false);
			toolIndex = equippedTools.Count - 1;
			equippedTools [toolIndex].gameObject.SetActive (true);
			Destroy (colliderObject);
			audioSources [4].Play ();
		} else if (colliderObject.name == "FlamethrowerItem") {
			equippedTools.Add (tools [(int)GameVariables.Tools.Flamethrower]);
			equippedTools [toolIndex].gameObject.SetActive (false);
			toolIndex = equippedTools.Count - 1;
			equippedTools [toolIndex].gameObject.SetActive (true);
			Destroy (colliderObject);
			audioSources [4].Play ();
		} else if (collider.gameObject.name == "WateringCanItem") {
			equippedTools.Add (tools [(int)GameVariables.Tools.WateringCan]);
			equippedTools [toolIndex].gameObject.SetActive (false);
			toolIndex = equippedTools.Count - 1;
			equippedTools [toolIndex].gameObject.SetActive (true);
			Destroy (colliderObject);
			audioSources [4].Play ();
		} else if (colliderObject.name == "MineralCollider") {
			audioSources [0].Play ();
			energy += colliderObject.transform.parent.gameObject.GetComponent<Mineral> ().value;
			Destroy (colliderObject.transform.parent.gameObject);
		} else if (colliderObject.CompareTag ("LifeBall")) {
			audioSources [1].Play ();
			lives++;
			if (lives > maxLives) {
				lives = maxLives;
			}
			Destroy (colliderObject);
		} else if (collider.CompareTag ("SpeedBall")) {
			speeding = true;
			speedCounter = 0f;
			speed = baseSpeed + collider.GetComponent<SpeedBall> ().speed;
			collider.GetComponent<SpeedBall> ().Cath ();
			speedTrail.SetActive (true);
			audioSources [3].Play ();
		} else if (collider.CompareTag ("Artifact")) {
			audioSources [5].Play ();
		}
	}

	public void EnterGravityZone (Vector3 vector) {
		planetGravity = false;
		gravityZoneCounter++;
		gravityVector = vector;
	}

	public void ExitGravityZone () {
		gravityZoneCounter--;
		if (gravityZoneCounter <= 0) {
			planetGravity = true;
		}
	}

	public void IncreaseMaxLife (int num) {
		maxLives += num;
		lives = maxLives;
	}

	public void DecreaseLife (int num) {
		lives -= num;
		damaged = true;
		damageCounter = 0f;
		ChangeColor (true);
		if (lives <= 0) {
			SceneManager.LoadSceneAsync (SceneManager.GetActiveScene ().name);
		}
	}

	public void IncreaseLife (int num) {
		lives += num;
	}

	private void ChangeColor (bool active) {
		RawImage image = GameObject.Find ("Canvas").GetComponent<RawImage> ();
		if (active) {
			image.color = new Color (image.color.r, image.color.g, image.color.b, 0.50f);
		} else {
			image.color = new Color (image.color.r, image.color.g, image.color.b, 0.00f);
		}
	}

	public void HideModel () {
		transform.Find ("Model").gameObject.SetActive (false);
	}

	public void ShowModel () {
		transform.Find ("Model").gameObject.SetActive (true);
	}

	public void SetShootAnimation () {
		animator.SetBool ("Shoot", true);
	}

	public void StopShootAnimation () {
		animator.SetBool ("Shoot", false);
	}

	public void TriggerPickAnimation() {
		animator.SetBool("MiningPick", true);
		inputActive = false;
	}

	public void OnPickingAnimatioEnd() {
		animator.SetBool("MiningPick", false);
		inputActive = true;
		((MiningPick)tools [0]).AnimationEnd ();
	}

}
