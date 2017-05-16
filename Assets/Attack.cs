﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : StateMachineBehaviour {
	public AI slime;
	
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		slime.AttackEnd ();
	}

}
