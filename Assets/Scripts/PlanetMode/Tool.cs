using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tool : MonoBehaviour {
	
	public abstract void Use ();

	public abstract void Stop ();

	void Update () {
		if (Input.GetAxisRaw ("Fire1") == 1) {
			Use ();
		}
	}

}
