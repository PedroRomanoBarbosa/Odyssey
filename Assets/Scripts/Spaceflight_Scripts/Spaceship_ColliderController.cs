using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship_ColliderController : MonoBehaviour {
	Spaceship_Movement playerScript;

	void Start()
	{
		playerScript = GetComponent<Spaceship_Movement>();
	}
	

	void OnTriggerEnter(Collider other)
	{
		if(!playerScript.isOutsideBounds() && !playerScript.isSelectingPlanet()){

			if(other.gameObject.CompareTag("BoostRing")){
				Debug.Log("BOOSTIO!");
				if(playerScript != null)
					playerScript.initiateBoost();
			}

			if(other.gameObject.CompareTag("PlanetSelection")){
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
