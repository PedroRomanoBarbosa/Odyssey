using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {
	private bool active;

	public Renderer targetRenderer;
	public CameraAnimation cameraAnimation;
	public Material off;
	public Material on;

	void Start () {
		targetRenderer.material = off;
		active = false;
		transform.GetChild (0).gameObject.SetActive (false);
		transform.GetChild (0).gameObject.GetComponent<ParticleSystem> ().Stop ();
	}

	void OnTriggerEnter (Collider collider) {
		if (collider.gameObject.CompareTag ("Missile")) {
			if (!active) {
				active = !active;
				ParticleSystem ps = transform.GetChild (0).gameObject.GetComponent<ParticleSystem> ();
				ps.gameObject.SetActive (true);
				ps.Play (true);
				targetRenderer.material = on;
				if (cameraAnimation != null) {
					cameraAnimation.Animate ();
				}
			}
		}
	}
}
