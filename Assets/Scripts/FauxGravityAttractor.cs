using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FauxGravityAttractor : MonoBehaviour {
	public float gravity = -10f;
	public float radius;

	void Start() {
		radius = GetComponent<SphereCollider> ().radius;
	}

	public void Attract (Transform body, Vector3 force, float speed) {
		Vector3 gravityUp = force.normalized;
		Vector3 bodyUp = body.up;
		body.GetComponent<Rigidbody> ().AddForce (gravityUp * gravity * radius * 100);
		Quaternion targetRotation = Quaternion.FromToRotation (bodyUp, gravityUp) * body.rotation;
		body.rotation = Quaternion.Slerp (body.rotation, targetRotation, speed * Time.deltaTime);
	}

	public float getGravityVelocity(float distance) {
		return distance;
	}

}