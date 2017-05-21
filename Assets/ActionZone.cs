using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionZone : MonoBehaviour {
	private bool playerAround;

	public Action action;

	void Update () {
		if (playerAround) {
			if (Input.GetAxisRaw ("Use") == 1) {
				action.OnAction ();
			}
		}
	}

	void OnTriggerEnter(Collider collider) {
		if (collider.gameObject.CompareTag("Player")) {
			playerAround = true;
		}
	}

	void OnTriggerExit(Collider collider) {
		if (collider.gameObject.CompareTag("Player")) {
			playerAround = false;
		}
	}

}
