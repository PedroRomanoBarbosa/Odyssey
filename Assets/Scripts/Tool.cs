using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tool : MonoBehaviour {
	
	public abstract void Use ();

	void Update() {
		if (Input.GetAxisRaw ("Fire1") == 1) {
			Use ();
		}
	}

}
