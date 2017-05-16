using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPositioning : MonoBehaviour {
	private float defaultDistance;

	public Transform player;

	void Start () {
		defaultDistance = (transform.position - player.position).magnitude;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 direction = transform.position - player.position;
		RaycastHit hit;
		if (Physics.Raycast(player.position, direction, out hit)) {
			transform.position = player.position + (direction.normalized * hit.distance);
		}
	}
}
