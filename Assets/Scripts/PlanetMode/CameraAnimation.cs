using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnimation : MonoBehaviour {
	private bool animate;
	private float aniCounter;
	private Vector3 startPosition;
	private Quaternion startRotation;
	private bool stopAction;

	public float aniDuration;
	public Camera camera;
	public Action action;
	public Transform anchor;

	void Start () {
		animate = false;
	}

	void Update () {
		if (animate) {
			aniCounter += Time.deltaTime;
			float t = aniCounter / aniDuration;
			if (t <= 0.5f) {
				camera.transform.rotation = Quaternion.Slerp (startRotation, anchor.localRotation, t * 2f);
				camera.transform.position = Vector3.Slerp (startPosition, anchor.localPosition, t * 2f);
			} else if (t <= 1f) {
				if (!stopAction) {
					action.OnAction ();
					stopAction = true;
				}
				camera.transform.rotation = Quaternion.Slerp (anchor.localRotation, startRotation, t * 2f - 1f);
				camera.transform.position = Vector3.Slerp (anchor.localPosition, startPosition, t * 2f - 1f);
			} else {
				GameVariables.cinematicPaused = false;
				animate = true;
				camera.transform.rotation = startRotation;
				camera.transform.position = startPosition;
				animate = false;
			}
		}
	}

	public void Animate () {
		GameVariables.cinematicPaused = true;
		animate = true;
		aniCounter = 0;
		startPosition = camera.transform.position;
		startRotation = camera.transform.rotation;
	}
}
