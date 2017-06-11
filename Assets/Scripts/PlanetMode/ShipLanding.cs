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

	public Camera camera;
	public GameObject ship;
	public Transform end;
	public float duration;

	void Start () {
		if (GameVariables.shipFirstPlanet) {
			startPosition = transform.position;
			startRotation = transform.rotation;
			camera.enabled = true;
			GameVariables.cinematicPaused = true;
		} else {
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
					SceneManager.LoadScene ("spaceship_control_test_ui");
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

			// Action to leave planet
			if (playerInArea) {
				if (Input.GetAxisRaw ("Use") == 1) {
					reverse = true;
					camera.enabled = true;
					GameVariables.cinematicPaused = true;
					aniCounter = 0;
					GameObject.FindGameObjectWithTag("Player").GetComponent<Player> ().HideModel ();
					animating = true;
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
