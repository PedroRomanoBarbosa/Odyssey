using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : Tool {
	private Collider fireCollider;
	private AudioSource[] audioSources;
	private bool beginningFlag, burningFlag;
	private float burningCounter;
	private float burningTimeCounter;

	public int damage;
	public Player player;
	public int energyPerBurningTime;
	public float burningTime;

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
			if (!burningFlag && burningCounter >= audioSources [0].clip.length && player.energy >= energyPerBurningTime) {
				burningFlag = true;
				audioSources [1].Play ();
			}
			if (player.energy >= energyPerBurningTime) {
				if (burningTimeCounter >= burningTime) {
					burningTimeCounter = 0f;
					player.energy -= energyPerBurningTime;
				} else {
					burningTimeCounter += Time.deltaTime;
				}
				transform.Find ("FlameHolder").gameObject.SetActive (true);
				fireCollider.enabled = true;
			} else {
				audioSources [1].Stop ();
				transform.Find ("FlameHolder").gameObject.SetActive (false);
				fireCollider.enabled = false;
			}
		} else {
			transform.Find ("FlameHolder").gameObject.SetActive (false);
			fireCollider.enabled = false;
			beginningFlag = false;
			burningFlag = false;
			audioSources [0].Stop ();
			audioSources [1].Stop ();
			player.StopShootAnimation ();
			burningCounter = 0f;
			burningTimeCounter = 0f;
		}
	}

	public override void Stop () {
		
	}

}
