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
	}
	public static bool cinematicPaused = false;
	public static Planet planet = Planet.Normal;
}
