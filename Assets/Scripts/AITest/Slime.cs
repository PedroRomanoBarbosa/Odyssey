using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy {
	private Rigidbody rigidBody;
	private SphereCollider bodyCollider;
	private bool patrolling;
	private GameObject player;
	private bool detectionArea;
	private bool following;
	private bool wandering;
	private Dying dyingBehaviour;
	private Attack attackBehaviour;
	private Vector3 moveVector;
	private Animator animator;
	private bool dying;
	private int pointIterator;

	public GameObject modelObject;
	public GameObject deathExplosion;
	public float speed;
	public float sensorMaxAngle;
	public float maxAttackRange;
	public Transform[] points;
	public Collider attackCollider;

	void Start () {
		rigidBody = GetComponent<Rigidbody> ();
		bodyCollider = GetComponent<SphereCollider> ();
		player = GameObject.FindGameObjectWithTag ("Player");
		animator = GetComponent<Animator> ();
		dyingBehaviour = animator.GetBehaviour<Dying> ();
		dyingBehaviour.slime = this;
		attackBehaviour = animator.GetBehaviour<Attack> ();
		attackBehaviour.slime = this;
	}

	public override void Update () {
		base.Update ();
		if (dying) {
			transform.localScale -= new Vector3 (0, (transform.localScale.y / 1.001f) * Time.deltaTime, 0);
			bodyCollider.radius -= bodyCollider.radius / 2f * Time.deltaTime;
		} else {
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
			if (!following) {
				Wander ();
			}
		}
	}

	void FixedUpdate () {
		if ( (detectionArea && following) || !following) {
			if (!dying) {
				if (!stop) {
					rigidBody.velocity = transform.forward * speed;
				}
			}
		}
	}

	void Wander () {
		if (points.Length > 0) {
			stop = false;
			float distance = (points [pointIterator].position - transform.position).magnitude;
			if (distance > 1f) {
				moveVector = Vector3.zero;
				transform.rotation = Quaternion.LookRotation (points [pointIterator].position - transform.position, transform.up);
			} else {
				pointIterator++;
				if (pointIterator >= points.Length) {
					pointIterator = 0;
				}
			}
		} else {
			stop = true;
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
					stop = false;
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
		Vector3 feet = player.transform.Find ("Feet").position;
		transform.rotation = Quaternion.LookRotation (feet - transform.position, transform.up);
	}

	void Attack () {
		attackCollider.enabled = true;
		animator.SetTrigger ("Attack");
	}

	public override void DecreaseLife (int damage) {
		base.DecreaseLife (damage);
		if (!dying) {
			thisRenderer.material = damageMaterial;
			if (life <= 0) {
				animator.SetTrigger ("Die");
				dying = true;
				following = false;
				detectionArea = false;
				transform.Find ("SensorArea").gameObject.SetActive (false);
			}
		}
	}

	public void EnterDetectionArea () {
		detectionArea = true;
	}

	public void LeaveDetetectionArea () {
		detectionArea = false;
		following = false;
		stop = true;
		animator.SetTrigger ("Iddle");
	}

	public override void Die () {
		base.Die ();
		Instantiate (deathExplosion, transform.position, transform.rotation);
	}

	public void AttackEnd () {
		attackCollider.enabled = false;
	}

}
