using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mineral : FauxGravityBody {
	public int value;
	private float animCounter;
	public float animationDuration;
	public float animationSpeed;
	public BoxCollider playerCollider;

	void Start () {
		animCounter = 0f;
	}

	void Update () {
		animCounter += Time.deltaTime;
		if (animCounter <= animationDuration) {
			Vector3 velocity = (transform.forward + transform.up) * animationSpeed * Time.deltaTime;
			transform.Translate (velocity);
		}
	}

	new void FixedUpdate () {
		if (animCounter >= animationDuration) {
			base.FixedUpdate ();
		}
	}

}
