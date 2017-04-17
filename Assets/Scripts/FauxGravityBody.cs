using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FauxGravityBody : MonoBehaviour {
	public float gravityRotationSpeed;
	public FauxGravityAttractor attractor;
	protected bool rotationEnded;
	protected float planetGravityRotationSpeed;

	protected void FixedUpdate () {
		if (!GameVariables.cinematicPaused) {
			attractor.Attract (this, transform.position - attractor.transform.position, gravityRotationSpeed);
		}
	}

}
