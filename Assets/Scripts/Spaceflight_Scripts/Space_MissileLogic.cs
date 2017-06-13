using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Space_MissileLogic : MonoBehaviour {
	public float speed;
	public float duration;
	public AudioClip barrierSound;
	private float timer = 0;

	void Update () {
		//Basic Forward Momentum
		transform.position += transform.forward * speed * Time.deltaTime;
		//Check if it's dead
		timer += Time.deltaTime;
		if(timer > duration)
			Destroy(this.gameObject);
		
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.CompareTag("PlanetBarrier")){
			Debug.Log("Shot a barrier!");

			//If it's a regular barrier, open the planet
			if(other.gameObject.name == "Barrier"){
				//Destroy the barrier and the missile
				Destroy(this.gameObject);
				PlanetSelectionVars vars = other.transform.parent.transform.GetComponent<PlanetSelectionVars>();
				AudioSource.PlayClipAtPoint(barrierSound, other.transform.position);
				vars.barrier = false;
				//Very shitty way to do this...
				other.gameObject.SetActive(false);
			}

			//If it's a stronger barrier, check for crystals before opening, but don't unlock planet yet...
			if(other.gameObject.name == "Barrier Powerful"){

				Destroy(this.gameObject);
				AudioSource.PlayClipAtPoint(barrierSound, other.transform.position);

				if(GameVariables.artifacts[0] && GameVariables.artifacts[1] 
						&& GameVariables.artifacts[2] && GameVariables.artifacts[3])
				{
					other.gameObject.SetActive(false);
				}
			}

			
		}
	}
}
