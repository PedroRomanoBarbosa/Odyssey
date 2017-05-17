using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour {
	public Enemy parent;

	void OnTriggerEnter (Collider collider) {
		if (collider.gameObject.name == "DamageArea") {
			Player player = collider.transform.parent.GetComponent<Player> ();
			player.DecreaseLife (parent.damage);
		}
	}

}
