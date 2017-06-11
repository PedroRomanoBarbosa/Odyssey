using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringCan : Tool {
	private Animator animator;
	private bool animating;
	private Vector3 originalPosition;
	private Quaternion originalRotation;

	public GameObject particle;
	public Player player;

	void Start () {
		animator = GetComponent<Animator> ();
		originalPosition = transform.localPosition;
		originalRotation = transform.localRotation;
	}

	public override void Use () {
		if (Input.GetAxisRaw ("Fire1") == 1) {
			particle.SetActive (true);
			player.SetShootAnimation ();
		} else {
			particle.SetActive (false);
			player.StopShootAnimation ();
		}
	}

	public override void Stop () {
		AnimationEnd ();
		particle.SetActive (false);
	}

	// Used as an animation event
	public void StopPour () {
		particle.SetActive (false);
	}

	// Used as an animation event
	public void AnimationEnd () {
		animating = false;
		transform.localPosition = originalPosition;
		transform.localRotation = originalRotation;
	}

	// Used as an animation event
	public void Pour () {
		particle.SetActive (true);
	}

}
