using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorArea : MonoBehaviour {
	private Slime parent;

	void Start () {
		parent = transform.parent.GetComponent<Slime> ();
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
