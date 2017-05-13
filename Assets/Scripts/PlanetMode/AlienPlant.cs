using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienPlant : MonoBehaviour {
	private float counter;
	private float heightCounter;
	private bool active;
	private float growingCounter;

	public float endHeight;
	public float speed;
	public bool growing;
	public float growingEnd;
	public float growingSpeed;


	void Start () {
		counter = 0;
		heightCounter = 0;
		active = false;
		growing = false;
	}

	void Update () {
		if (growing && !active) {
			growingCounter += Time.deltaTime;
			if (growingCounter < growingEnd) {
				growingCounter += growingSpeed * Time.deltaTime;
				transform.GetChild (0).GetChild (1).localScale += transform.localScale * growingSpeed * Time.deltaTime;
			} else {
				active = true;
				growing = false;
			}
		}
		if (active) {
			counter += Time.deltaTime;
			heightCounter += speed * Time.deltaTime;
			if (heightCounter < endHeight) {
				transform.Translate (0, speed * Time.deltaTime, 0);
			}
		}
		growing = false;
	}

}
