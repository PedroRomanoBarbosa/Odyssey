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
		GameObject spawnPoint = returnSpawnPoint();
		transform.position = spawnPoint.transform.position;
		transform.rotation = spawnPoint.transform.rotation;

		//Send camera behind the player right away
		shipCamera.transform.position = transform.position - transform.forward * 5 + transform.up * 1;
		shipCamera.transform.rotation = transform.rotation;
	}


	public GameObject returnSpawnPoint(){
		switch(GameVariables.planet){
			case GameVariables.Planet.Ice:
			case GameVariables.Planet.Vulcanic:
				return spawnVulcanic;
			case GameVariables.Planet.Forest:
				return spawnForest;
			case GameVariables.Planet.Metal:
				return spawnMetal;
			default:
			case GameVariables.Planet.Normal:
				return spawnNormal;
		}
	}

}
