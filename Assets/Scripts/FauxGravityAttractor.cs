using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FauxGravityAttractor : MonoBehaviour {
	public float gravity = -10f;
	public float radius;

	void OnStart() {
		radius = GetComponent<SphereCollider> ().radius;
	}

	public void Attract (Transform body) {
		Vector3 gravityUp = (body.position - transform.position).normalized;
		Vector3 bodyUp = body.up;
		body.GetComponent<Rigidbody> ().AddForce (gravityUp * gravity * getGravityVelocity(getGravityVelocity ((body.position - transform.position).magnitude - radius)));
		Quaternion targetRotation = Quaternion.FromToRotation (bodyUp, gravityUp) * body.rotation;
		body.rotation = Quaternion.Slerp (body.rotation, targetRotation, 50 * Time.deltaTime);
	}

	public float getGravityVelocity(float distance) {
		return distance;
	}

}