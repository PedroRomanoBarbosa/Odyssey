using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artifact : MonoBehaviour {
	private AudioSource audioSource;

	public GameVariables.Artifact artifact;

	void OnTriggerEnter (Collider collider) {
		if (collider.gameObject.CompareTag("Player")) {
			GameVariables.artifacts [(int)artifact] = true;
			Destroy (gameObject);
		}
	}
}
