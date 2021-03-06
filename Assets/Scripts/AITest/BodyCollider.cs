﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyCollider : MonoBehaviour {
	private Enemy parent;

	void Start () {
		parent = transform.parent.GetComponent<Enemy> ();
	}

	void OnTriggerEnter (Collider collider) {
		parent.EnterBodyCollider (collider);
	}

	void OnTriggerExit (Collider collider) {
		parent.ExitBodyCollider (collider);
	}
}
