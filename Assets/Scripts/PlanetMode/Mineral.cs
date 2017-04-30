using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mineral : FauxGravityBody {
	public int value;

	void OnCollisionEnter (Collision collision) {
		if (collision.gameObject.CompareTag("Player")) {
			collision.gameObject.GetComponent<Player> ().energy += value;
			Destroy (gameObject);
		}
	}

}
