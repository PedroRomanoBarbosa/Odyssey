using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {
	private Renderer missileRenderer;
	private bool active;

	public Action action;
	public Material off;
	public Material on;

	void Start () {
		missileRenderer = GetComponent<Renderer> ();
		missileRenderer.material = off;
		active = false;
		transform.GetChild (0).gameObject.GetComponent<ParticleSystem> ().Stop ();
	}

	void OnTriggerEnter (Collider collider) {
		if (collider.gameObject.CompareTag ("Missile")) {
			active = !active;
			ParticleSystem ps = transform.GetChild (0).gameObject.GetComponent<ParticleSystem> ();
			ps.Play ();
			ps.transform.GetChild (0).GetComponent<ParticleSystem> ().Play ();
			if (active) {
				missileRenderer.material = on;
			} else {
				missileRenderer.material = off;
			}
			if (action != null) {
				action.OnAction ();
			}
		}
	}
}
