using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FauxGravityAttractor : MonoBehaviour {
	public float gravity = -10f;
	public float radius;

	void Start() {
		radius = GetComponent<SphereCollider> ().radius;
	}

	public void Attract (Transform body, Vector3 force, float rotationSpeed) {
		Vector3 gravityUp = force.normalized;
		Vector3 bodyUp = body.up;
		body.GetComponent<Rigidbody> ().AddForce (gravityUp * gravity * radius * 100);
		Quaternion targetRotation = Quaternion.FromToRotation (bodyUp, gravityUp) * body.rotation;
		if (rotationSpeed != 0) {
			body.rotation = Quaternion.Slerp (body.rotation, targetRotation, rotationSpeed * Time.deltaTime);
		} else {
			body.rotation = targetRotation;
		}
	}

	public float getGravityVelocity(float distance) {
		return distance;
	}

}