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
	public Camera mainCamera;
	public Action action;
	public Action endAction;
	public Transform anchor;

	void Start () {
		animate = false;
		mainCamera = GameObject.Find ("Main Camera").GetComponent<Camera> ();
	}

	void Update () {
		if (animate) {
			aniCounter += Time.deltaTime;
			float t = aniCounter / aniDuration;
			if (t <= 0.5f) {
				mainCamera.transform.rotation = Quaternion.Slerp (startRotation, anchor.localRotation, t * 2f);
				mainCamera.transform.position = Vector3.Slerp (startPosition, anchor.localPosition, t * 2f);
			} else if (t <= 1f) {
				if (!stopAction) {
					action.OnAction ();
					stopAction = true;
				}
				mainCamera.transform.rotation = Quaternion.Slerp (anchor.localRotation, startRotation, t * 2f - 1f);
				mainCamera.transform.position = Vector3.Slerp (anchor.localPosition, startPosition, t * 2f - 1f);
			} else {
				mainCamera.transform.rotation = startRotation;
				mainCamera.transform.position = startPosition;
				animate = false;
				if (endAction != null) {
					endAction.OnAction ();
				}
				GameVariables.cinematicPaused = false;
			}
		}
	}

	public void Animate () {
		GameVariables.cinematicPaused = true;
		animate = true;
		aniCounter = 0;
		startPosition = mainCamera.transform.position;
		startRotation = mainCamera.transform.rotation;
	}
}
