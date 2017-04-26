using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeDestruction : MonoBehaviour {
	private int state;

	void Start () {
		state = 2;
	}

	void OnCollisionEnter(Collision collision) {
		transform.GetChild (state).GetComponent<Renderer> ().enabled = false;
		state -= 1;
		if (state == -1) {
			Destroy (this.gameObject);
		} else {
			transform.GetChild (state).GetComponent<Renderer> ().enabled = true;
		}
	}
}
