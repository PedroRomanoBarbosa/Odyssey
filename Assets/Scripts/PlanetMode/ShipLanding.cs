using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShipLanding : MonoBehaviour {
	private float aniCounter;
	private Vector3 startPosition;
	private Quaternion startRotation;
	private bool playerInArea;
	private bool reverse;
	private bool animating;
	private AudioSource[] audioSources;

	public Camera camera;
	public GameObject ship;
	public Transform end;
	public float duration;

	void Start () {
		audioSources = GetComponents<AudioSource> ();
		if (GameVariables.shipFirstPlanet) {
			startPosition = transform.position;
			startRotation = transform.rotation;
			camera.enabled = true;
			GameVariables.cinematicPaused = true;
			audioSources [0].Play ();
		} else {
			startPosition = transform.position;
			startRotation = transform.rotation;
			transform.position = end.position;
			transform.rotation = end.rotation;
			camera.enabled = false;
		}
		animating = true;
	}

	void Update () {
		// If player has already the ship
		if (GameVariables.shipFirstPlanet && animating) {
			if (aniCounter > duration) {
				if (reverse) {
					transform.position = startPosition;
					transform.rotation = startRotation;
					if (!GameVariables.shipFirstPlanet) {
						GameVariables.shipFirstPlanet = true;
					}
					SceneManager.LoadScene ("Space");
				} else {
					transform.position = end.position;
					transform.rotation = end.rotation;
					ShipLanded ();
					animating = false;
				}
			} else {
				aniCounter += Time.deltaTime;
				float t;
				if (reverse) {
					t = 1 - aniCounter / duration;
				} else {
					t = aniCounter / duration;
				}
				transform.position = Vector3.Slerp (startPosition, end.position, t);
				transform.rotation = Quaternion.Slerp (startRotation, end.rotation, t);
			}
		}

		// Action to leave planet
		if (playerInArea) {
			if (Input.GetAxisRaw ("Use") == 1) {
				reverse = true;
				audioSources [1].Play ();
				camera.enabled = true;
				GameVariables.cinematicPaused = true;
				aniCounter = 0;
				GameObject.FindGameObjectWithTag("Player").GetComponent<Player> ().HideModel ();
				animating = true;
				if (!GameVariables.shipFirstPlanet) {
					GameVariables.shipFirstPlanet = true;
				}
			}
		}
	}

	void ShipLanded () {
		GameVariables.cinematicPaused = false;
		camera.enabled = false;
		GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ().ShowModel ();
	}

	void OnTriggerEnter (Collider collider) {
		if (collider.CompareTag ("Player")) {
			playerInArea = true;
		}
	}

	void OnTriggerExit (Collider collider) {
		if (collider.CompareTag ("Player")) {
			playerInArea = false;
		}
	}

}
