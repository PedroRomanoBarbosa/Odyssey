using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorArea : MonoBehaviour {
	private AI parent;

	void Start () {
		parent = transform.parent.GetComponent<AI> ();
	}

	void OnTriggerEnter (Collider collider) {
		if (collider.gameObject.CompareTag("Player")) {
			parent.EnterDetectionArea ();
		}
	}

	void OnTriggerExit (Collider collider) {
		if (collider.gameObject.CompareTag("Player")) {
			parent.LeaveDetetectionArea ();
		}
	}

}
