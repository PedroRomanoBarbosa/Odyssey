using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship_SpaceBoundary : MonoBehaviour {
	Spaceship_Movement playerScript;
	void Start()
	{
		playerScript = GetComponent<Spaceship_Movement>();
	}

	void OnTriggerExit(Collider other)
	{
		if(other.gameObject.CompareTag("GameBoundary")){
			Debug.Log("Left Space");
			if(playerScript != null)
				playerScript.setOutsideBounds();
		}
	}
}
