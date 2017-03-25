using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileMovement : MonoBehaviour {
	public Transform center;
	public Vector3 axis;
	public float speed;

	void Update () {
		transform.RotateAround (center.position, axis, speed * Time.deltaTime);
	}

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag ("Obstacle")) {
			Destroy (this.gameObject);
		}
	}
}
