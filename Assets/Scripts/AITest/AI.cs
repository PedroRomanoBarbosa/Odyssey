using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {
	private Rigidbody rigidBody;
	private SphereCollider bodyCollider;
	private bool patrolling;
	private GameObject player;
	private bool detectionArea;
	private bool following;
	private Dying dyingBehaviour;
	private Vector3 moveVector;
	private Animator animator;
	private Renderer thisRenderer;
	private Material defaultMaterial;
	private int collisionCounter;
	private bool dying;

	public GameObject modelObject;
	public Material damageMaterial;
	public Collider damageCollider;
	public GameObject deathExplosion;
	public int life;
	public float speed;
	public float sensorMaxAngle;
	public float maxAttackRange;

	void Start () {
		thisRenderer = modelObject.GetComponent<SkinnedMeshRenderer> ();
		defaultMaterial = thisRenderer.material;
		rigidBody = GetComponent<Rigidbody> ();
		bodyCollider = GetComponent<SphereCollider> ();
		player = GameObject.FindGameObjectWithTag ("Player");
		animator = GetComponent<Animator> ();
		dyingBehaviour = animator.GetBehaviour<Dying> ();
		dyingBehaviour.slime = this;
	}

	void Update () {
		if (dying) {
			transform.localScale -= new Vector3(0, (transform.localScale.y / 1.001f) * Time.deltaTime, 0);
			bodyCollider.radius -= bodyCollider.radius / 2f * Time.deltaTime;
		}
		if (detectionArea) {
			if (!following) {
				SearchPlayer ();
			} else {
				Move ();
				if ((transform.position - player.transform.position).magnitude <= maxAttackRange) {
					Attack ();
				}
			}
		}
	}

	void FixedUpdate () {
		if (detectionArea && following) {
			rigidBody.velocity = transform.forward * speed;
		}
	}

	void SearchPlayer () {
		Vector3 direction = player.transform.position - transform.position;
		Debug.DrawRay (transform.position, direction, Color.red);
		RaycastHit raycasthit;
		if (Physics.Raycast (transform.position, direction, out raycasthit)) {
			if (raycasthit.collider.gameObject.CompareTag ("Player")) {
				float angle = Vector3.Angle (transform.forward, direction);
				if (angle <= sensorMaxAngle) {
					following = true;
					animator.SetTrigger ("Move");
				} else {
					following = false;
				}
			} else {
				following = false;
			}
		} else {
			following = false;
		}
	}

	void Move () {
		moveVector = Vector3.zero;
		Vector3 feet = player.transform.FindChild ("Feet").position;
		transform.rotation = Quaternion.LookRotation (feet - transform.position, transform.up);
	}

	void Attack () {
		animator.SetTrigger ("Attack");
	}

	void OnTriggerEnter (Collider collider) {
		if (collider.gameObject.CompareTag("Missile")) {
			if (collisionCounter == 0) {
				MissileMovement missile = collider.gameObject.transform.parent.GetComponent<MissileMovement> ();
				life -= missile.damage;
				if (!dying) {
					thisRenderer.material = damageMaterial;
					if (life <= 0) {
						animator.SetTrigger ("Die");
						dying = true;
						following = false;
						detectionArea = false;
						transform.FindChild ("SensorArea").gameObject.SetActive (false);
					}
				}
				collisionCounter++;
			}
		}
	}

	void OnTriggerExit (Collider collider) {
		if (collider.gameObject.CompareTag("Missile")) {
			if (collisionCounter == 1) {
				thisRenderer.material = defaultMaterial;
				collisionCounter = 0;
			}
		}
	}

	public void EnterDetectionArea () {
		detectionArea = true;
	}

	public void LeaveDetetectionArea () {
		detectionArea = false;
		following = false;
		animator.SetTrigger ("Iddle");
	}

	public void Die () {
		Destroy (this.gameObject);
		Instantiate (deathExplosion, transform.position, transform.rotation);
	}

}
