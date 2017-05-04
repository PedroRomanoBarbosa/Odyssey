using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLauncher : Tool {
	public GameObject missilePrefab;
	private float missileCooldownCounter;
	public float missileCooldown;
	public float missileSpeed;
	public float missileLifeDuration;

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
			} else {
				missileCooldownCounter += Time.deltaTime;
			}
		}
	}

	public override void Stop () {
		
	}

}
