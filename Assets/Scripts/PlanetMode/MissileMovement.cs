using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileMovement : MonoBehaviour {
	private float counter;
	private float angleSpeed;
	private int gravityZones;

	public GameObject planet;
	public Vector3 axis;
	public float speed;
	public float duration;

	void Start () {
		counter = 0f;
		angleSpeed = Mathf.Rad2Deg * (speed/planet.transform.localScale.x);
		gravityZones = 0;
	}

	void Update () {
		if (!GameVariables.cinematicPaused) {
			CalculatePosition ();
		}
	}

	void FixedUpdate () {
		
	}

	void CalculatePosition () {
		counter += Time.deltaTime;
		if (counter < duration) {
			if (gravityZones <= 0) {
				transform.rotation = Quaternion.LookRotation (transform.forward, transform.position - planet.transform.position);
				transform.RotateAround (planet.transform.position, axis, angleSpeed * Time.deltaTime);
			} else {
				transform.position += transform.forward * speed * Time.deltaTime;
			}
		} else {
			Destroy (this.gameObject);
		}
	}

	void OnTriggerEnter(Collider collider) {
		if (collider.gameObject.CompareTag ("Obstacle")) {
			Destroy (gameObject);
		} else if (collider.gameObject.CompareTag ("GravityZone")) {
			gravityZones++;
			transform.rotation = Quaternion.LookRotation (transform.forward, collider.transform.up);
		}
	}

	void OnTriggerExit (Collider collider) {
		if (collider.gameObject.CompareTag ("GravityZone")) {
			gravityZones--;
			transform.rotation = Quaternion.LookRotation (transform.forward, collider.transform.up);
		}
	}

}
