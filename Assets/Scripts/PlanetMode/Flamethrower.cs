using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : Tool {
	private Collider fireCollider;

	public int damage;

	void Start () {
		fireCollider = GetComponent<BoxCollider> ();
	}

	public override void Use () {
		if (Input.GetAxisRaw ("Fire1") == 1) {
			transform.Find ("FlameHolder").gameObject.SetActive (true);
			fireCollider.enabled = true;
		} else {
			transform.Find ("FlameHolder").gameObject.SetActive (false);
			fireCollider.enabled = false;
		}
	}

	public override void Stop () {
		
	}

}
