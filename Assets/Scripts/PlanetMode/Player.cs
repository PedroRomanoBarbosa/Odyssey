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

	// Status
	public int lives;
	public int maxLives;
	public int energy;

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
	public int gravityZoneCounter;
	private Vector3 gravityVector;

	// Jump Variables
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
	private Tool miningPick, missileLauncher, flamethrower, wateringCan;
	public List<Tool> equippedTools;
	private int toolIndex;

    //Planet Interface
    public Text live;
    public int pts;
    public Text points; 

	// Sound Variables
	private AudioSource[] audioSources;

	void Start () {
		audioSources = GetComponents<AudioSource> ();
		movementAxis = transform.GetChild (0);
		model = transform.GetChild (1);
		rigidBody = GetComponent<Rigidbody> ();
		planetGravity = true;
		gravityZoneCounter = 0;
		isGrounded = false;
		miningPick = model.Find ("MiningPick").GetComponent<Tool> ();
		missileLauncher = model.Find ("MissileLauncher").GetComponent<Tool> ();
		flamethrower = model.Find ("Flamethrower").GetComponent<Tool> ();
		wateringCan = model.Find ("WateringCan").GetComponent<Tool> ();
		equippedTools = new List<Tool> ();
		toolIndex = 0;
		if (equippedTools.Count > 0) {
			equippedTools [toolIndex].gameObject.SetActive (true);
        }
        pts = 0;
	}

	void Update () {
		if (!GameVariables.cinematicPaused) {
			CheckGrounded ();
			MovePlayer ();
			ChangeWeapon ();
            UpdateUIText();
		}
		DamageLoop ();
	}

    void UpdateUIText()
    {
        live.text = "X " + lives;
        points.text = pts + " Points";
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
		if (equippedTools.Count > 1) {
			if (Input.GetKeyUp("q")) {
				Tool currentTool = equippedTools [toolIndex];
				currentTool.Stop ();
				currentTool.gameObject.SetActive (false);
				toolIndex++;
				if (toolIndex >= equippedTools.Count) {
					toolIndex = 0;
				}
                FindObjectOfType<ToolSwitch>().SetTool(equippedTools[toolIndex]);
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
		animator.SetFloat ("Speed", move.magnitude / speed);
		if (move != Vector3.zero) {
			model.rotation = Quaternion.LookRotation (move, movementAxis.up);
		}
		Jump ();
		if (!jumping && !isGrounded) {
			move += -movementAxis.up * 10f;
		}
	}

	void Jump() {
		if (!jumping && isGrounded) {
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
					move += transform.up * jumpFunction (jumpCounter, jumpDuration);
				}
			} else {
				move += -movementAxis.up * 10f;
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
		if (colliderObject.name == "MiningPickItem") {
			equippedTools.Add (miningPick);
            pts += 200;
            equippedTools [toolIndex].gameObject.SetActive (false);
			toolIndex = equippedTools.Count - 1;
            FindObjectOfType<ToolSwitch>().SetTool(equippedTools[toolIndex]);
			equippedTools [toolIndex].gameObject.SetActive (true);
			Destroy (colliderObject);
		} else if (colliderObject.name == "MissileLauncherItem") {
			equippedTools.Add (missileLauncher);
            pts += 200;
            equippedTools [toolIndex].gameObject.SetActive (false);
			toolIndex = equippedTools.Count - 1;
            FindObjectOfType<ToolSwitch>().SetTool(equippedTools[toolIndex]);
            equippedTools [toolIndex].gameObject.SetActive (true);
			Destroy (colliderObject);
		} else if (colliderObject.name == "FlamethrowerItem") {
			equippedTools.Add (flamethrower);
            pts += 200;
            equippedTools [toolIndex].gameObject.SetActive (false);
			toolIndex = equippedTools.Count - 1;
            FindObjectOfType<ToolSwitch>().SetTool(equippedTools[toolIndex]);
            equippedTools [toolIndex].gameObject.SetActive (true);
			Destroy (colliderObject);
		} else if (collider.gameObject.name == "WateringCanItem") {
			equippedTools.Add (wateringCan);
            pts += 200;
			equippedTools [toolIndex].gameObject.SetActive (false);
			toolIndex = equippedTools.Count - 1;
            FindObjectOfType<ToolSwitch>().SetTool(equippedTools[toolIndex]);
            equippedTools [toolIndex].gameObject.SetActive (true);
			Destroy (colliderObject);
		} else if (colliderObject.name == "MineralCollider") {
			audioSources [0].Play ();
            pts += 500;
			energy += colliderObject.transform.parent.gameObject.GetComponent<Mineral> ().value;
			Destroy (colliderObject.transform.parent.gameObject);
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
        pts += 100;
	}

	private void ChangeColor (bool active) {
		RawImage image = GameObject.Find ("Canvas").GetComponent<RawImage> ();
		if (active) {
			image.color = new Color (image.color.r, image.color.g, image.color.b, 0.50f);
		} else {
			image.color = new Color (image.color.r, image.color.g, image.color.b, 0f);
		}
	}

}
