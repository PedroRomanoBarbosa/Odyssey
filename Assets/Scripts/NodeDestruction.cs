using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeDestruction : MonoBehaviour {
	private int state;

	void Start () {
		state = 2;
	}

	void OnCollisionEnter(Collision collision) {
		if (state == 0) {
			Destroy (this.gameObject);
		} else {
			transform.GetChild (state).GetComponent<Renderer> ().enabled = false;
			state -= 1;
			transform.GetChild (state).GetComponent<Renderer> ().enabled = true;
		}
	}
}
