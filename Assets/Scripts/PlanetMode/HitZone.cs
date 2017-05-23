using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitZone : MonoBehaviour {
	public Enemy enemy;

	void OnTriggerStay (Collider collider) {
		enemy.OnHitZoneStay (collider);
	}

}
