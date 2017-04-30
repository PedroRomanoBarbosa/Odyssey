using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : Action {
	public enum State {
		Closed,
		Opened,
		Opening,
		Closing
	}
	private State state;
	private Vector3 openPosition;
	private Vector3 closedPosition;
	private float counter;

	public float duration;
	public float distance;

	void Start () {
		state = State.Closed;
		openPosition = transform.position;
		closedPosition = transform.position + transform.right * distance;
	}

	void Update () {
		if (state == State.Opening || state == State.Closing) {
			counter += Time.deltaTime;
			float t;
			if (counter >= duration) {
				t = 1;
			} else {
				t = counter / duration;
			}
			if (t <= 1) {
				switch (state) {
				case State.Opening:
					transform.position = Vector3.Lerp (openPosition, closedPosition, t);
					break;
				case State.Closing:
					transform.position = Vector3.Lerp (closedPosition, openPosition, t);
					break;
				}
				if (t == 1) {
					switch (state) {
					case State.Opening:
						state = State.Opened;
						break;
					case State.Closing:
						state = State.Closed;
						break;
					}
					counter = 0;
				}
			}
		}
	}

	public override void OnAction () {
		GameVariables.cinematicPaused = true;
		GameObject.Find ("Main Camera").GetComponent<CameraAnimation> ().Animate (GameObject.Find("CameraAnchor").transform, 1);
		counter = 0;
		switch(state) {
		case State.Opened:
			state = State.Closing;
			break;
		case State.Closed:
			state = State.Opening;
			break;
		}
	}

}
