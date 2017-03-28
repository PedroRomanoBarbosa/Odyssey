using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FauxGravityBody : MonoBehaviour {
	public FauxGravityAttractor attractor;

	protected void FixedUpdate () {
		if (!GameVariables.cinematicPaused) {
			attractor.Attract (transform);
		}
	}

}
