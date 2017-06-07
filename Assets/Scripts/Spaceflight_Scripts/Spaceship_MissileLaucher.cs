using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship_MissileLaucher : MonoBehaviour {
	public GameObject missilePrefab;
	private Spaceship_Movement shipScript;
	public AudioClip[] fireSounds;
	private float missileCooldownCounter = 0;
	public float missileCooldown = 5;
	public float missileSpeed  = 85;
	public float missileLifeDuration = 5;
	public bool canFire = false;

	void Start()
	{
		shipScript = GetComponent<Spaceship_Movement>();
	}

	void Update(){
		//Can Fire && Ship is being controlled
		if(canFire && shipScript.isUnderPlayerControl())
		{

			if (missilePrefab != null && missileCooldownCounter > 0) 
				missileCooldownCounter -= Time.deltaTime;
			else{
				if (Input.GetKey(KeyCode.Space)) {
					Debug.Log("FIRE!");
					if (fireSounds.Length > 0) { 
						AudioSource.PlayClipAtPoint(fireSounds[Random.Range(0,fireSounds.Length)], transform.position);
					} 
					missileCooldownCounter = missileCooldown;
					GameObject missile = Instantiate (missilePrefab, transform.position, Quaternion.identity);
					missile.transform.parent = GameObject.Find("Missiles").transform;
					missile.transform.rotation = transform.rotation;
					Space_MissileLogic script = missile.GetComponent<Space_MissileLogic>();
					script.speed = missileSpeed;
					script.duration = missileLifeDuration;
				}
			}

		}
			
	}

}
