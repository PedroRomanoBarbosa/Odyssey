﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningPick : Tool {
	private Animator animator;
	private bool animating;
	private Vector3 originalPosition;
	private Quaternion originalRotation;

	void Start () {
		animator = GetComponent<Animator> ();
		originalPosition = transform.localPosition;
		originalRotation = transform.localRotation;
	}

	public override void Use () {
		if (animating == false) {
			animating = true;
			transform.GetChild (1).GetComponent<BoxCollider> ().enabled = true;
			animator.SetTrigger ("Swing");
		}
	}

	public override void Stop () {
		animatioEnd ();
	}

	// Used as an animation event
	public void animatioEnd () {
		transform.GetChild (1).GetComponent<BoxCollider> ().enabled = false;
		animating = false;
		transform.localPosition = originalPosition;
		transform.localRotation = originalRotation;
	}

}
