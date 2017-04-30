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
		if (missileCooldownCounter >= missileCooldown) {
			missileCooldownCounter = 0f;
			GameObject missile = Instantiate (missilePrefab, transform.Find("MissileAnchor").position, Quaternion.identity);
			missile.transform.rotation = transform.rotation;
			missile.transform.Rotate (90f, 0f, 0f);
			MissileMovement script = missile.GetComponent<MissileMovement> ();
			script.axis = transform.right;
			script.speed = missileSpeed;
			script.duration = missileLifeDuration;
		} else {
			missileCooldownCounter += Time.deltaTime;
		}
	}

}
