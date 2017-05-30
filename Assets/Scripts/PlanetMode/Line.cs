using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour {
	public Transform[] points;

	void Start() {
		LineRenderer lineRenderer = GetComponent<LineRenderer> ();
		Vector3[] positions = new Vector3[points.Length];
		for (int i = 0; i < points.Length; i++) {
			positions [i] = points [i].position;
		}
		lineRenderer.SetPositions (positions);
	}

}
