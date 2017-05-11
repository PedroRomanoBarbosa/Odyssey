using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour {

	void OnTriggerStay (Collider collider) {
		if (collider.gameObject.CompareTag ("AlienPlant")) {
			collider.gameObject.GetComponent<AlienPlant> ().growing = true;
		}
	}
}
