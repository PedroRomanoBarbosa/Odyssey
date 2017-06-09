using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSettings : MonoBehaviour {
	public GameVariables.Planet planet;
	public Transform playerStartPosition;

	void Start() {
		GameVariables.planet = planet;
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		player.transform.position = playerStartPosition.position;
		player.transform.rotation = playerStartPosition.rotation;
		if (GameVariables.shipFirstPlanet) {
			player.GetComponent<Player> ().HideModel ();
		}
	}
}
