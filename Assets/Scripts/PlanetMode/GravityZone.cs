using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityZone : MonoBehaviour {
	public float gravityRotationSpeed = 1f;

	void OnTriggerEnter (Collider collider) {
		if (collider.gameObject.CompareTag ("Player")) {
			Player player = collider.gameObject.GetComponent<Player> ();
			player.EnterGravityZone (transform.up);
		}
	}

	void OnTriggerExit (Collider collider) {
		if (collider.gameObject.CompareTag ("Player")) {
			Player player = collider.gameObject.GetComponent<Player> ();
			player.ExitGravityZone ();
		}
	}
}
