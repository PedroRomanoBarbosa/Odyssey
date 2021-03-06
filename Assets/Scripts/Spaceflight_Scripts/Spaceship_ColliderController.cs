﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship_ColliderController : MonoBehaviour {
	Spaceship_Movement playerScript;
	public AudioClip boostSound;
	public AudioClip engineStart;

	void Start()
	{
		playerScript = GetComponent<Spaceship_Movement>();
        if (engineStart != null)
        {
            if (AudioController.GetEffectsVolume() != 0)
            {
				GameObject spawn = GetComponent<Spaceship_SpawnLocation>().returnSpawnPoint();
				AudioSource.PlayClipAtPoint(engineStart, spawn.transform.position);
            }
        }
	}

	void OnTriggerEnter(Collider other)
	{
		if(!playerScript.isOutsideBounds() && !playerScript.isSelectingPlanet()){

			if(other.gameObject.CompareTag("BoostRing")){
				Debug.Log("BOOSTIO!");
                if (boostSound != null)
                {
                    if (AudioController.GetEffectsVolume() != 0)
                        AudioSource.PlayClipAtPoint(boostSound, transform.position);
                }
				if(playerScript != null)
					playerScript.initiateBoost();
			}

			if(other.gameObject.CompareTag("PlanetSelection")){
				//Remove all missiles
				Transform missileHolder = GameObject.Find("Missiles").transform;
				foreach (Transform child in missileHolder) {
					GameObject.Destroy(child.gameObject);
				}

				Debug.Log("Approached a Planet!");
				PlanetSelectionVars vars = other.GetComponent<PlanetSelectionVars>();
				if(playerScript != null){
					playerScript.setPlanetSelection(vars);
				}
			}
		}
		else if(playerScript.isOutsideBounds()){
			
			if(other.gameObject.CompareTag("GameBoundary")){
				Debug.Log("Back Inside!");
				if(playerScript != null){
					playerScript.unsetOutsideBounds();
				}
			}
			
		}

	}
	void OnTriggerExit(Collider other)
	{
		if(!playerScript.isOutsideBounds() && !playerScript.isSelectingPlanet()){

			if(other.gameObject.CompareTag("GameBoundary")){
				Debug.Log("Left Space");
				if(playerScript != null){
					playerScript.setOutsideBounds();
				}
			}

		}	
	}

}
