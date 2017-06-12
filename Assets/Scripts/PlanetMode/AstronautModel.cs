using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstronautModel : MonoBehaviour {
	public Player player;

	void OnMiningAnimationEnd() {
		player.OnPickingAnimatioEnd ();
	}

	public void StartPicking () {
		player.StartPicking ();
	}

	public void EndPicking () {
		player.EndPicking ();
	}

}
