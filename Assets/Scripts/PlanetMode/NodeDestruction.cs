﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeDestruction : MonoBehaviour {
	private int state;
	private GameObject sparkle, explosion;
	private AudioSource audioSource;

	public GameObject mineralPrefab;
	public AudioClip explosionClip;
	public Renderer boulderRenderer;

	void Start () {
		audioSource = GetComponent<AudioSource> ();
		state = 2;
		sparkle = transform.Find ("Sparkle").gameObject;
		explosion = transform.Find ("Explosion").gameObject;
		sparkle.GetComponent<ParticleSystem> ().Stop(true);
		explosion.GetComponent<ParticleSystem> ().Stop(true);
		explosion.SetActive (false);
		sparkle.SetActive (false);
	}

	void OnTriggerEnter(Collider collider) {
		if (collider.gameObject.CompareTag ("Pick")) {
			transform.GetChild (state).GetComponent<Renderer> ().enabled = false;
			state -= 1;
			sparkle.SetActive (true);
			sparkle.GetComponent<ParticleSystem> ().Play(true);
			if (state == -1) {
				GetComponent<BoxCollider> ().enabled = false;
				state = 2;
				if (explosionClip != null) {
					audioSource.PlayOneShot (explosionClip, 1f);
				}
				explosion.SetActive (true);
				explosion.GetComponent<ParticleSystem> ().Play(true);
				int rand = Random.Range (1,5);
				float rotAngle = 360f / rand;
				GameObject mineral;
				for (int i = 0; i < rand; i++) {
					mineral = Instantiate (mineralPrefab, transform.Find("MineralAnchor").position, Quaternion.identity, transform);
					mineral.GetComponent<Mineral> ().attractor = GameObject.Find ("Planet").GetComponent<FauxGravityAttractor> ();
					mineral.transform.Rotate (0, rotAngle * i, 0);
					mineral.transform.Translate (transform.forward);
				}
			} else {
				transform.GetChild (state).GetComponent<Renderer> ().enabled = true;
			}
		}
	}

}
