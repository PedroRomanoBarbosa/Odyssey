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

	public void Start () {
		audioFire = GetComponent<AudioSource> ();
	}

	public override void Use() {
		if (Input.GetAxisRaw ("Fire1") == 1) {
			if (missileCooldownCounter >= missileCooldown) {
				missileCooldownCounter = 0f;
				GameObject missile = Instantiate (missilePrefab, transform.Find ("MissileAnchor").position, Quaternion.identity);
				missile.transform.rotation = transform.rotation;
				MissileMovement script = missile.GetComponent<MissileMovement> ();
				script.planet = GameObject.Find ("Planet");
				script.axis = transform.right;
				script.speed = missileSpeed;
				script.duration = missileLifeDuration;
				script.damage = damage;
				audioFire.Play ();
			} else {
				missileCooldownCounter += Time.deltaTime;
			}
		}
	}

	public override void Stop () {
		
	}

}
