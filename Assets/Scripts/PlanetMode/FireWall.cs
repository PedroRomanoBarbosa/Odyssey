using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWall : MonoBehaviour {
	private float extinguishCounter;

	public bool extinguish;
	public float duration;

	void Update () {
		if (extinguish) {
			if (extinguishCounter >= duration) {
				gameObject.SetActive (false);
			} else {
				extinguishCounter += Time.deltaTime;
				transform.position -= transform.up * Time.deltaTime;
			}
		}
		extinguish = false;
	}
}
