using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FauxGravityBody : MonoBehaviour {
	public FauxGravityAttractor attractor;

	void Start () {
		
	}

	protected void FixedUpdate () {
		attractor.Attract (transform);
	}

}
