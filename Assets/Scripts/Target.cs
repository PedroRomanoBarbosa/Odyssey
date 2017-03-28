using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {
	private Renderer missileRenderer;
	private bool active;

	public GameObject action;
	public Material off;
	public Material on;

	void Start () {
		missileRenderer = GetComponent<Renderer> ();
		missileRenderer.material = off;
		active = false;
	}

	void Update () {
		
	}

	void OnTriggerEnter (Collider collider) {
		if (collider.gameObject.CompareTag ("Missile")) {
			active = !active;
			if (active) {
				missileRenderer.material = on;
			} else {
				missileRenderer.material = off;
			}
			if (action != null) {
				action.GetComponent<Action> ().OnAction ();
			}
		}
	}
}
