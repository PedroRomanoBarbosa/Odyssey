using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLauncher : Tool {
	private float missileCooldownCounter;
	private AudioSource audioFire;

	public GameObject missilePrefab;
	public float missileCooldown;
	public float missileSpeed;
	public float missileLifeDuration;
	public int damage;
	public Transform missileAnchor;
	public Player player;
	public int energyPerShot;
	public AudioClip shotClip, blanksClip;

	public void Start () {
		audioFire = GetComponent<AudioSource> ();
	}

	public override void Use() {
		if (Input.GetButton ("Fire1")) {
			player.SetShootAnimation ();
		} else {
			player.StopShootAnimation ();
		}
	}

	public override void Stop () {
		
	}

	public void Shoot () {
		if (player.energy >= energyPerShot) {
			player.energy -= energyPerShot;
			GameObject missile = Instantiate (missilePrefab, missileAnchor.position, Quaternion.identity);
			missile.transform.rotation = transform.rotation;
			MissileMovement script = missile.GetComponent<MissileMovement> ();
			script.planet = GameObject.Find ("Planet");
			script.axis = transform.right;
			script.speed = missileSpeed;
			script.duration = missileLifeDuration;
			script.damage = damage;
			audioFire.PlayOneShot (shotClip);
			player.SetShootAnimation ();
		} else {
			audioFire.PlayOneShot (blanksClip);
		}
	}

}
