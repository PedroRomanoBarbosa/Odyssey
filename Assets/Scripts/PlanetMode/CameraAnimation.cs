using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnimation : MonoBehaviour {
	private bool animate;
	private float aniCounter;
	private Vector3 startPosition, endPosition;
	private Quaternion startRotation, endRotation;

	public float aniDuration;

	void Start () {
		animate = false;
	}

	void Update () {
		if (animate) {
			aniCounter += Time.deltaTime;
			float t = aniCounter / aniDuration;
			if (aniCounter <= aniDuration) {
				transform.rotation = Quaternion.Slerp (startRotation, endRotation, t);
				transform.position = Vector3.Lerp (startPosition, endPosition, t);
			} else if (aniCounter <= aniDuration * 2) {
				t = t - aniDuration;
				transform.rotation = Quaternion.Slerp (endRotation, startRotation, t);
				transform.position = Vector3.Lerp (endPosition, startPosition, t);
			} else {
				transform.rotation = startRotation;
				transform.position = startPosition;
				animate = false;
				aniCounter = 0;
				GameVariables.cinematicPaused = false;
			}
		}
	}

	public void Animate (Transform anchor, float duration) {
		animate = true;
		aniCounter = 0;
		aniDuration = duration;
		startPosition = transform.position;
		startRotation = transform.rotation;
		endPosition = anchor.position;
		endRotation = anchor.rotation;
	}
}
