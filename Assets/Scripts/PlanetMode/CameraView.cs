using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraView : Action {
	private Camera mainCamera;
	private float counter;
	private bool animating;
	private Vector3 oldPosition;
	private Quaternion oldRotation;

	public Camera cameraView;
	public float duration;
	public Action action;

	void Start () {
		mainCamera = GameObject.Find ("Main Camera").GetComponent<Camera> ();
	}

	void Update () {
		if (animating) {
			counter += Time.deltaTime;
			if (counter >= duration / 2f) {
				action.OnAction ();
			} else if (counter >= duration) {
				counter = 0;
				cameraView.enabled = false;
				mainCamera.enabled = true;
			}
		}
	}

	public override void OnAction () {
		mainCamera.enabled = false;
		cameraView.enabled = true;
	}
}
