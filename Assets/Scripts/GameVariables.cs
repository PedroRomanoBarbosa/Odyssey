using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameVariables {
	public enum Planet {
		Normal,
		Ice,
		Vulcanic,
		Forest,
		Metal
	};
	public enum Artifact {
		Spiral,
		Star,
		Penthagon,
		Cuboid
	};
	public enum Tools {
		MiningPick,
		MissileLauncher,
		Flamethrower,
		WateringCan
	}
	public int diamonds;
	public static bool cinematicPaused = false;
	public static bool shipFirstPlanet = true;
	public static Planet planet = Planet.Normal;
	public static bool[] artifacts = { false, false, false, false };
	public static bool[] tools = {true, true, true, true};
}
