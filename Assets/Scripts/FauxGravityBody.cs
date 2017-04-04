using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FauxGravityBody : MonoBehaviour {
	public float gravityRotationSpeed;
	public FauxGravityAttractor attractor;

	protected void FixedUpdate () {
		if (!GameVariables.cinematicPaused) {
			attractor.Attract (transform, transform.position - attractor.transform.position, gravityRotationSpeed);
		}
	}

}
