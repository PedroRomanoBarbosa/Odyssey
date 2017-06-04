using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Space_MissileLogic : MonoBehaviour {
	public float speed;
	public float duration;
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
			//Destroy the barrier and the missile
			PlanetSelectionVars vars = other.transform.parent.transform.GetComponent<PlanetSelectionVars>();
			vars.barrier = false;
			Destroy(this.gameObject);

			//Very shitty way to do this...
			other.gameObject.SetActive(false);
		}
	}
}
