using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBall : MonoBehaviour {
	private float counter;
	private bool active;

	public float speed;
	public float cooldown;
	public GameObject model;
	public GameObject particle;

	void Update () {
		if (!active) {
			if (counter >= cooldown) {
				active = true;
				Activate ();
			} else {
				counter += Time.deltaTime;
			}
		}
	}

	public void Cath () {
		Deactivate ();
	}

	public void Activate () {
		GetComponent<Collider> ().enabled = true;
		active = true;
		model.SetActive (true);
		particle.SetActive (true);
	}

	public void Deactivate () {
		GetComponent<Collider> ().enabled = false;
		counter = 0;
		active = false;
		model.SetActive (false);
		particle.SetActive (false);
	}

}
