using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingAudio : MonoBehaviour {
	private AudioSource audioSource;

	public AudioClip[] heelsIce;
	public AudioClip[] feetIce;

	public AudioClip[] heelsNormal;
	public AudioClip[] feetNormal;

	public AudioClip[] heelsVulcanic;
	public AudioClip[] feetVulcanic;

	public AudioClip[] heelsForest;
	public AudioClip[] feetForest;

	public AudioClip[] heelsMetal;
	public AudioClip[] feetMetal;

	public void Start () {
		audioSource = GetComponent<AudioSource> ();
	}

	public void Heel() {
		AudioClip clip = null;
		switch (GameVariables.planet){
		case GameVariables.Planet.Ice:
			if (heelsIce.Length > 0) {
				clip = heelsIce [Random.Range (0, heelsIce.Length)];
			}
			break;
		case GameVariables.Planet.Normal:
			if (heelsNormal.Length > 0) {
				clip = heelsNormal [Random.Range (0, heelsNormal.Length)];
			}
			break;
		case GameVariables.Planet.Forest:
			if (heelsForest.Length > 0) {
				clip = heelsForest [Random.Range (0, heelsForest.Length)];
			}
			break;
		case GameVariables.Planet.Vulcanic:
			if (heelsVulcanic.Length > 0) {
				clip = heelsVulcanic [Random.Range (0, heelsVulcanic.Length)];
			}
			break;
		case GameVariables.Planet.Metal:
			if (heelsMetal.Length > 0) {
				clip = heelsMetal [Random.Range (0, heelsMetal.Length)];
			}
			break;
		}
		if (clip != null) {
			audioSource.PlayOneShot (clip, 0.5f);
		}
	}

	public void Foot() {
		AudioClip clip = null;
		switch (GameVariables.planet){
		case GameVariables.Planet.Ice:
			if (feetIce.Length > 0) {
				clip = feetIce [Random.Range (0, feetIce.Length)];
			}
			break;
		case GameVariables.Planet.Normal:
			if (feetNormal.Length > 0) {
				clip = feetNormal [Random.Range (0, feetNormal.Length)];
			}
			break;
		case GameVariables.Planet.Forest:
			if (feetForest.Length > 0) {
				clip = feetForest [Random.Range (0, feetForest.Length)];
			}
			break;
		case GameVariables.Planet.Vulcanic:
			if (feetVulcanic.Length > 0) {
				clip = feetVulcanic [Random.Range (0, feetVulcanic.Length)];
			}
			break;
		case GameVariables.Planet.Metal:
			if (feetMetal.Length > 0) {
				clip = feetMetal [Random.Range (0, feetMetal.Length)];
			}
			break;
		}
		if (clip != null) {
			audioSource.PlayOneShot (clip, 0.5f);
		}
	}

}
