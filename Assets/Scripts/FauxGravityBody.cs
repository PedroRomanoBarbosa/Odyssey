﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FauxGravityBody : MonoBehaviour {
	protected float gravityRotationSpeed;
	public FauxGravityAttractor attractor;
	protected bool rotationEnded;
	public float planetGravityRotationSpeed;

	void Start() {
		gravityRotationSpeed = planetGravityRotationSpeed;
	}

	protected void FixedUpdate () {
		if (!GameVariables.cinematicPaused) {
			attractor.Attract (this, transform.position - attractor.transform.position, gravityRotationSpeed);
		}
	}

}
