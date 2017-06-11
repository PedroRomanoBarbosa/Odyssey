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

	public void Start () {
		audioFire = GetComponent<AudioSource> ();
	}

	public override void Use() {
		if (Input.GetAxisRaw ("Fire1") == 1) {
			if (missileCooldownCounter >= missileCooldown) {
				missileCooldownCounter = 0f;
				GameObject missile = Instantiate (missilePrefab, missileAnchor.position, Quaternion.identity);
				missile.transform.rotation = transform.rotation;
				MissileMovement script = missile.GetComponent<MissileMovement> ();
				script.planet = GameObject.Find ("Planet");
				script.axis = transform.right;
				script.speed = missileSpeed;
				script.duration = missileLifeDuration;
				script.damage = damage;
				audioFire.Play ();
				player.SetShootAnimation ();
			} else {
				player.StopShootAnimation ();
				missileCooldownCounter += Time.deltaTime;
			}
		}
	}

	public override void Stop () {
		
	}

}
