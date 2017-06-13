using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetSettings : MonoBehaviour {
	public GameVariables.Planet planet;
	public Transform playerStartPosition;
	public Transform playerStartPosition2;
	public GameObject player;
	public FauxGravityAttractor attractor;

	public RawImage[] artifactsGUI;
	public RawImage[] heartsGUI;

	void Awake () {
		GameVariables.planet = planet;
		GameObject playerInstance = Instantiate (player);
		Player playerScript = playerInstance.GetComponent<Player> ();
		playerScript.attractor = attractor;
		if (GameVariables.shipFirstPlanet) {
			playerInstance.transform.position = playerStartPosition.position;
			playerInstance.transform.rotation = playerStartPosition.rotation;
			playerInstance.GetComponent<Player> ().HideModel ();
		} else {
			playerInstance.transform.position = playerStartPosition2.position;
			playerInstance.transform.rotation = playerStartPosition2.rotation;
		}
		GameObject.Find ("Main Camera").GetComponent<Camera> ().cullingMask = 1 << 0 | 1 << 1 | 1 << 2 | 1 << 4 | 1 << 8;
		GUI ();
	}

	void Start () {
		GUI ();
	}

	void Update() {
		GUI ();
	}

	void GUI () {
		for (int i = 0; i < GameVariables.artifacts.Length; i++) {
			if (GameVariables.artifacts [i]) {
				artifactsGUI [i].gameObject.SetActive (true);
			}
		}
		for (int i = 0; i < GameVariables.maxLives; i++) {
			if (i < GameVariables.lives) {
				heartsGUI [i].gameObject.SetActive (true);
			} else {
				heartsGUI [i].gameObject.SetActive (false);
			}
		}
	}
}
