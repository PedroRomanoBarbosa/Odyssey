using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSettings : MonoBehaviour {
	public GameVariables.Planet planet;
	public Transform playerStartPosition;
	public Transform playerStartPosition2;
	public GameObject player;
	public FauxGravityAttractor attractor;

	void Start() {
		GameVariables.planet = planet;
		GameObject playerInstance = Instantiate (player);
		playerInstance.GetComponent<Player> ().attractor = attractor;
		if (GameVariables.shipFirstPlanet) {
			playerInstance.transform.position = playerStartPosition.position;
			playerInstance.transform.rotation = playerStartPosition.rotation;
			playerInstance.GetComponent<Player> ().HideModel ();
		} else {
			playerInstance.transform.position = playerStartPosition2.position;
			playerInstance.transform.rotation = playerStartPosition2.rotation;
		}
	}
}
