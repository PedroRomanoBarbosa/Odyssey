using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour {
	public int life;

	void OnTriggerEnter (Collider collider) {
		if (collider.gameObject.CompareTag ("Player")) {
			Player player = collider.gameObject.GetComponent<Player> ();
			player.maxLives += life;
			Destroy (gameObject);
		}
	}

}
