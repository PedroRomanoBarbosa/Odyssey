using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienPlant : MonoBehaviour {
	private float counter;
	private float heightCounter;

	public float endHeight;
	public float speed;

	void Start () {
		counter = 0;
		heightCounter = 0;
	}

	void Update () {
		counter += Time.deltaTime;
		heightCounter += speed * Time.deltaTime;
		if (heightCounter < endHeight) {
			transform.Translate (0, speed * Time.deltaTime, 0);
		}
	}
}
