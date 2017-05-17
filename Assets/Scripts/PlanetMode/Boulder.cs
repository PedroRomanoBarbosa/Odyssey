using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : MonoBehaviour {
	private int collisionCounter;
	private Animator animator;

	public int life;
	public GameObject explosionPrefab;

	void Start () {
		animator = transform.GetChild (0).GetComponent<Animator> ();
	}

	void OnTriggerEnter (Collider collider) {
		if (collider.gameObject.CompareTag ("Pick")) {
			GameObject particles = collider.transform.parent.GetComponent<MiningPick> ().particles;
			Instantiate (particles, collider.gameObject.transform.position, collider.gameObject.gameObject.transform.rotation);
			animator.SetTrigger ("Vibrate");
			life--;
			if (life == 0) {
				Transform anchor = transform.FindChild ("ExplosionAnchor");
				GameObject explosion = Instantiate (explosionPrefab, anchor.position, anchor.rotation);
				explosion.transform.localScale = new Vector3 (5, 5, 5);
				Destroy (gameObject);
			}
		}
	}

}
