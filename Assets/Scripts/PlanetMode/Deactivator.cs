using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deactivator : Action {
	private static int counter = 2;
	private bool used;

	public GameObject[] objects;

	public override void OnAction () {
		if (!used) {
			used = true;
			foreach (GameObject obj in objects) {
				obj.SetActive (false);
				counter--;
				if (counter <= 0) {
					
				}
			}
		}
	}
}
