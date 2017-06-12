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

	public void Shoot () {
		if (player.tools[1].gameObject.activeSelf) {
			((MissileLauncher)player.tools [1]).Shoot ();
		}
	}

}
