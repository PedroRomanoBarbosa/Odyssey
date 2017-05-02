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
				int rand = Random.Range (2,5);
				float rotAngle = 360f / rand;
				GameObject mineral;
				for (int i = 0; i < rand; i++) {
					mineral = Instantiate (mineralPrefab, transform.Find("MineralAnchor").position, Quaternion.identity);
					mineral.GetComponent<Mineral> ().attractor = GameObject.Find ("Planet").GetComponent<FauxGravityAttractor> ();
					mineral.transform.Rotate (0, rotAngle * i, 0);
					mineral.transform.Translate (transform.forward);
				}
			} else {
				transform.GetChild (state).GetComponent<Renderer> ().enabled = true;
			}
		}
	}
}
