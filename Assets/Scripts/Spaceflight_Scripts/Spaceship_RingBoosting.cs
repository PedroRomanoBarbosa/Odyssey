﻿using System.Collections;
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
		if(other.gameObject.CompareTag("BoostRing")){
			if(playerScript != null)
				playerScript.initiateBoost();
		}
	}
}
