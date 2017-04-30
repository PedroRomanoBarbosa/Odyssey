using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningPick : Tool {
	private Animator animator;

	void Start() {
		animator = GetComponent<Animator> ();
	}

	public override void Use() {
		transform.GetChild (1).GetComponent<BoxCollider> ().enabled = true;
		animator.SetTrigger ("Swing");
	}

	public void animatioEnd() {
		transform.GetChild (1).GetComponent<BoxCollider> ().enabled = false;
	}

}
