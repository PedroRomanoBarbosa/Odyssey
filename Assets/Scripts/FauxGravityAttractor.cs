using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FauxGravityAttractor : MonoBehaviour {
	public float gravity = -10f;
	public float gravityRotationSpeed = 5f;

	public void Attract (FauxGravityBody body, Vector3 force) {
		Vector3 gravityUp = force.normalized;
		Vector3 bodyUp = body.transform.up;
		body.GetComponent<Rigidbody> ().velocity += gravityUp * gravity;
		Quaternion targetRotation = Quaternion.FromToRotation (bodyUp, gravityUp) * body.transform.rotation;
		body.transform.rotation = Quaternion.Slerp (body.transform.rotation, targetRotation, gravityRotationSpeed * Time.deltaTime);
	}

}