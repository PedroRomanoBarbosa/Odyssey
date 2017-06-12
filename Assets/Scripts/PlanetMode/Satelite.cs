using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Satelite : MonoBehaviour {
	private float radius;
	private Vector3 centerVector;

	public Transform center;
	public float speed;

	void Start () {
		centerVector = transform.position - center.position;
		radius = centerVector.magnitude;
		transform.up = centerVector.normalized;
	}

	void Update () {
		if (!GameVariables.cinematicPaused) {
			transform.RotateAround (center.position, center.up, speed * Time.deltaTime);
		}
	}
}
