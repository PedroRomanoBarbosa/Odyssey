using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : Tool {
	private Collider fireCollider;
	private AudioSource[] audioSources;
	private bool beginningFlag, burningFlag;
	private float burningCounter;

	public int damage;
	public Player player;

	void Start () {
		fireCollider = GetComponent<BoxCollider> ();
		audioSources = GetComponents<AudioSource> ();
	}

	public override void Use () {
		if (Input.GetAxisRaw ("Fire1") == 1) {
			burningCounter += Time.deltaTime;
			if (!beginningFlag) {
				beginningFlag = true;
				audioSources [0].Play ();
				player.SetShootAnimation ();
			}
			if (!burningFlag && burningCounter >= audioSources[0].clip.length) {
				burningFlag = true;
				audioSources [1].Play ();
			}
			transform.Find ("FlameHolder").gameObject.SetActive (true);
			fireCollider.enabled = true;
		} else {
			transform.Find ("FlameHolder").gameObject.SetActive (false);
			fireCollider.enabled = false;
			beginningFlag = false;
			burningFlag = false;
			audioSources [0].Stop ();
			audioSources [1].Stop ();
			player.StopShootAnimation ();
			burningCounter = 0f;
		}
	}

	public override void Stop () {
		
	}

}
