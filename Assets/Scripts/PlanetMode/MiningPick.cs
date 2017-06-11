using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningPick : Tool {
	private Vector3 originalPosition;
	private Quaternion originalRotation;
	private Collider pickCollider;
	private AudioSource audioSource;

	public Player player;
	public GameObject particles;
	public int damage;
	public AudioClip[] pickAudioClips;

	void Start () {
		pickCollider = GetComponent<Collider> ();
		audioSource = GetComponent<AudioSource> ();
		originalPosition = transform.localPosition;
		originalRotation = transform.localRotation;
	}

	public override void Use () {
		if (Input.GetAxisRaw ("Fire1") == 1) {
				pickCollider.enabled = true;
				player.TriggerPickAnimation ();
		}
	}

	public override void Stop () {
		AnimationEnd ();
	}

	// Used as an animation event
	public void AnimationEnd () {
		pickCollider.enabled = false;
	}

	// Used as an animation event
	public void EndDownSwing () {
		pickCollider.enabled = false;
	}

	void OnTriggerEnter (Collider collider) {
		if (collider.gameObject.CompareTag ("Node") || collider.gameObject.CompareTag ("Boulder")) {
			Debug.Log (collider.name);
			if (pickAudioClips.Length > 0) {
				audioSource.PlayOneShot (pickAudioClips[Random.Range(0, pickAudioClips.Length)], 1f);
			}
		}
	}

}