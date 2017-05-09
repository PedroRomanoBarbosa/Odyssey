using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringCan : Tool {
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
				animator.SetTrigger ("Use");
			}
		} else if (Input.GetAxisRaw ("Fire1") == 0) {
			if (animating) {
				animator.SetTrigger ("Stop");
			}
		}
	}

	public override void Stop () {
		AnimationEnd ();
	}

	// Used as an animation event
	public void AnimationEnd () {
		animating = false;
		transform.localPosition = originalPosition;
		transform.localRotation = originalRotation;
	}

}
