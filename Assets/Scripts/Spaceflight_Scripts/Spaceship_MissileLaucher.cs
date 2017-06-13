using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship_MissileLaucher : MonoBehaviour {
	public GameObject missilePrefab;
	public GameObject missilePowerfullPrefab;
	private Spaceship_Movement shipScript;
	public AudioClip[] fireSounds;
	private float missileCooldownCounter = 0;
	public float missileCooldown = 5;
	public float missileSpeed  = 85;
	public float missileLifeDuration = 5;

	void Start()
	{
		shipScript = GetComponent<Spaceship_Movement>();
	}

	void LateUpdate(){
		//Can Fire && Ship is being controlled
		if(shipScript.isUnderPlayerControl())
		{
			if (missilePowerfullPrefab != null && missilePrefab != null && missileCooldownCounter >= 0) 
				missileCooldownCounter -= Time.deltaTime;
			else{
				if (Input.GetKey(KeyCode.Space)) 
				{
					missileCooldownCounter = missileCooldown;
					if(GameVariables.artifacts[0] && GameVariables.artifacts[1] 
						&& GameVariables.artifacts[2] && GameVariables.artifacts[3])
					{
						//Strong Missile
						fireMissle(missilePowerfullPrefab);
					}
					else {
						if(GameVariables.artifacts[0])
						{
							//Missile
							fireMissle(missilePrefab);						
						} else {
							//Nothing
							Debug.Log("Not enough Crystals to shoot");
						}
					}
				}


			}
		}		
	}

	void fireMissle(GameObject prefab){
		Debug.Log("FIRE!");
		if (fireSounds.Length > 0) { 
			AudioSource.PlayClipAtPoint(fireSounds[Random.Range(0,fireSounds.Length)], transform.position);
		} 
		GameObject missile = Instantiate (prefab, transform.position, Quaternion.identity);
		missile.transform.parent = GameObject.Find("Missiles").transform;
		missile.transform.rotation = transform.rotation;
		Space_MissileLogic script = missile.GetComponent<Space_MissileLogic>();
		script.speed = missileSpeed;
		script.duration = missileLifeDuration;
	}
}
