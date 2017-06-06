using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour {
	public Enemy parent;

	void OnTriggerStay (Collider collider) {
		parent.OnAttackAreaStay (collider);
	}

	void OnTriggerExit (Collider collider) {
		parent.OnAttackAreaExit (collider);
	}

}
