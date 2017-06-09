using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipLanding : MonoBehaviour {
	public Camera camera;
	public GameObject ship;

	void Start () {
		if (GameVariables.shipFirstPlanet) {
			camera.enabled = true;
			GameVariables.cinematicPaused = true;
		}
	}

	void ShipLanded () {
		GameVariables.cinematicPaused = false;
		camera.enabled = false;
		GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ().ShowModel ();
	}

}
