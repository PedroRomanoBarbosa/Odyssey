using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FauxGravityBody : MonoBehaviour {
	public FauxGravityAttractor attractor;

	void Start () {
		
	}

	void FixedUpdate () {
		attractor.Attract (transform);
	}

}
