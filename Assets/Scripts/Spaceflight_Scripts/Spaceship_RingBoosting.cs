using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship_RingBoosting : MonoBehaviour {
	Spaceship_Movement playerScript;

	void Start()
	{
		playerScript = GetComponent<Spaceship_Movement>();
	}
	
	void OnTriggerEnter(Collider other)
	{
		Debug.Log("BOOSTIO!");
		if(other.gameObject.CompareTag("BoostRing")){
			playerScript.initiateBoost();
		}
	}
	
}
