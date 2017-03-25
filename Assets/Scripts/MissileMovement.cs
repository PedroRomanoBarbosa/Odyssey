using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileMovement : MonoBehaviour {
	private float counter;
	private float angleSpeed;

	public GameObject planet;
	public Vector3 axis;
	public float speed;
	public float duration;

	void Start() {
		counter = 0f;
		angleSpeed = Mathf.Rad2Deg * (speed/planet.transform.localScale.x);
		Debug.Log ("ANGEL SPEED: " + angleSpeed);
	}

	void Update () {
		counter += Time.deltaTime;
		if (counter < duration) {
			transform.RotateAround (planet.transform.position, axis, angleSpeed * Time.deltaTime);
		} else {
			Destroy (this.gameObject);
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag ("Obstacle")) {
			Destroy (this.gameObject);
		}
	}
}
