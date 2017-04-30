using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningPick : Tool {
	private Animator animator;

	void Start() {
		animator = transform.GetChild(0).GetComponent<Animator> ();
	}

	public override void Use() {
		animator.SetTrigger ("Swing");
	} 

}
