using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSettings : MonoBehaviour {
	public GameVariables.Planet planet;

	void Start() {
		GameVariables.planet = planet;
	}
}
