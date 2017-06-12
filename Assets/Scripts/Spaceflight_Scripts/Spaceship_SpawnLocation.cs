using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship_SpawnLocation : MonoBehaviour {

	public GameObject spawnNormal;
	public GameObject spawnIce;
	public GameObject spawnVulcanic;
	public GameObject spawnForest;
	public GameObject spawnMetal;
	public GameObject shipCamera;

	void Start () {
        //Change spawn point based on last planet
		switch(GameVariables.planet){
			case GameVariables.Planet.Ice:
				break;
			case GameVariables.Planet.Vulcanic:
				break;
			case GameVariables.Planet.Forest:
				break;
			case GameVariables.Planet.Metal:
				break;
			
			default:
			case GameVariables.Planet.Normal:
				transform.position = spawnNormal.transform.position;
				transform.rotation = spawnNormal.transform.rotation;
				break;
		}
	
		//Send camera behind the player right away
		shipCamera.transform.position = transform.position - transform.forward * 5 + transform.up * 1;
	}
	
}
