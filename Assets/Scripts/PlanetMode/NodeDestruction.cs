using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeDestruction : MonoBehaviour {
	private int state;
	public GameObject mineralPrefab;

	void Start () {
		state = 2;
	}

	void OnTriggerEnter(Collider collider) {
		if (collider.gameObject.CompareTag ("Pick")) {
			transform.GetChild (state).GetComponent<Renderer> ().enabled = false;
			state -= 1;
			if (state == -1) {
				Destroy (this.gameObject);
				int rand = Random.Range (1,4);
				GameObject mineral;
				for (int i = 1; i < rand; i++) {
					mineral = Instantiate (mineralPrefab, transform.Find("MineralAnchor").position, Quaternion.identity);
					mineral.GetComponent<Mineral> ().attractor = GameObject.Find ("Planet").GetComponent<FauxGravityAttractor> ();
					float randRotation = Random.value;
					mineral.transform.Rotate (0, randRotation, 0);
				}
			} else {
				transform.GetChild (state).GetComponent<Renderer> ().enabled = true;
			}
		}
	}
}
