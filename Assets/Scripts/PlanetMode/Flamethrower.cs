using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : Tool {

	public override void Use () {
		if (Input.GetAxisRaw ("Fire1") == 1) {
			transform.Find ("FlameHolder").gameObject.SetActive (true);
		} else {
			transform.Find ("FlameHolder").gameObject.SetActive (false);
		}
	}

	public override void Stop () {
		
	}

}
