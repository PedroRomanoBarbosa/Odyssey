using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FauxGravityAttractor : MonoBehaviour {
	public float gravity = -10f;

	public void Attract (FauxGravityBody body, Vector3 force, float rotationSpeed) {
		Vector3 gravityUp = force.normalized;
		Vector3 bodyUp = body.transform.up;
		body.GetComponent<Rigidbody> ().velocity += gravityUp * gravity;
		Quaternion targetRotation = Quaternion.FromToRotation (bodyUp, gravityUp) * body.transform.rotation;
		if (rotationSpeed != 0) {
			body.transform.rotation = Quaternion.Slerp (body.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
			if (body.transform.rotation == targetRotation) {
				body.rotationEnded = true;
			}
		} else {
			body.transform.rotation = targetRotation;
		}
	}

}