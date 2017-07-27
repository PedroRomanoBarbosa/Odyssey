using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WarpInput : MonoBehaviour {

	void Update () {
		if (Input.GetKeyDown ("1")) {
			SceneManager.LoadSceneAsync ("FirstPlanet");
		} else if (Input.GetKeyDown ("2")) {
			SceneManager.LoadSceneAsync ("SmallPlanet");
		} else if (Input.GetKeyDown ("3")) {
			SceneManager.LoadSceneAsync ("VulcanicPlanet");
		} else if (Input.GetKeyDown ("4")) {
			SceneManager.LoadSceneAsync ("JunglePlanet");
		} else if (Input.GetKeyDown ("5")) {
			SceneManager.LoadSceneAsync ("FinalPlanet");
		}
	}
}
