using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipLanding : MonoBehaviour {
	private float aniCounter;
	private Vector3 startPosition;
	private Quaternion startRotation;

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
		}
	}

	void Update () {
		if (GameVariables.shipFirstPlanet) {
			if (aniCounter > duration) {
				transform.position = Vector3.Slerp (startPosition, end.position, 1f);
				transform.rotation = Quaternion.Slerp (startRotation, end.rotation, 1f);
				ShipLanded ();
			} else {
				aniCounter += Time.deltaTime;
				float t = aniCounter / duration;
				transform.position = Vector3.Slerp (startPosition, end.position, t);
				transform.rotation = Quaternion.Slerp (startRotation, end.rotation, t);
			}
		}
	}

	void ShipLanded () {
		GameVariables.cinematicPaused = false;
		camera.enabled = false;
		GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ().ShowModel ();
	}

}
