using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : Tool {
	private Collider fireCollider;
	private AudioSource audioSource;

	public int damage;
	public AudioClip beginningClip;
	public AudioClip clip;

	void Start () {
		fireCollider = GetComponent<BoxCollider> ();
		audioSource = GetComponent<AudioSource> ();
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
