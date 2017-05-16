using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamable : MonoBehaviour {
	private bool burning;
	private float counter;
	private float flamePointTimeStep;
	private int flamepointCounter;
	private GameObject[] flames;

	public Transform[] flamePoints;
	public GameObject firePrefab;
	public float burningTime;

	void Start () {
		flames = new GameObject[flamePoints.Length];
		Vector3 planetPosition = GameObject.Find ("Planet").transform.position;
		if (flamePoints.Length > 0) {
			flamePointTimeStep = burningTime / flamePoints.Length;
			for (int i = 0; i < flamePoints.Length; i++) {
				Vector3 gravity = flamePoints [i].position - planetPosition;
				flamePoints [i].up = gravity;
			}
		}
	}

	void Update () {
		if (burning) {
			counter += Time.deltaTime;
			if (counter >= burningTime) {
				for (int i = 0; i < flames.Length; i++) {
					Destroy(flames [i]);
				}
				Destroy (gameObject);
			} else {
				if (counter >= flamepointCounter * flamePointTimeStep) {
					flames [flamepointCounter] = Instantiate (firePrefab, flamePoints [flamepointCounter].position, flamePoints [flamepointCounter].rotation);
					flames [flamepointCounter].transform.localScale = new Vector3 (3, 3, 3);
					flamepointCounter++;
				}
			}
		}
	}

	void OnTriggerStay (Collider collider) {
		if (collider.gameObject.name == "Flamethrower") {
			burning = true;
		}
	}

	void OnTriggerExit (Collider collider) {
		if (collider.gameObject.name == "Flamethrower") {
			burning = false;
		}
	}

}
