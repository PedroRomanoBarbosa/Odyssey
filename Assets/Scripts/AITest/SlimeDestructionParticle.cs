using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeDestructionParticle : MonoBehaviour {
	private float duration;
	private float counter;
	private ParticleSystem ps;

	void Start () {
		ps = GetComponent<ParticleSystem> ();
		duration = ps.main.duration;
	}

	void Update () {
		if (counter >= duration) {
			Destroy (this.gameObject);
		} else {
			counter += Time.deltaTime;
		}
	}
}
