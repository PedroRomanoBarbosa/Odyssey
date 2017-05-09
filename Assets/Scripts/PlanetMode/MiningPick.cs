using System.Collections;
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
		if (Input.GetAxisRaw ("Fire1") == 1) {
			if (animating == false) {
				animating = true;
				transform.GetChild (1).GetComponent<BoxCollider> ().enabled = true;
				animator.SetTrigger ("Swing");
			}
		}
	}

	public override void Stop () {
		AnimationEnd ();
	}

	// Used as an animation event
	public void AnimationEnd () {
		transform.GetChild (1).GetComponent<BoxCollider> ().enabled = false;
		animating = false;
		transform.localPosition = originalPosition;
		transform.localRotation = originalRotation;
	}

	// Used as an animation event
	public void EndDownSwing () {
		transform.GetChild (1).GetComponent<BoxCollider> ().enabled = false;
	}

}
