using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlanetSelectionVars : MonoBehaviour {
	public string planetName;
	public Object planetScene;
	public int orbitRadius;
	public int planetSize;
	[HideInInspector]
	public Vector3 planetPosition;

	void Start()
	{
		planetPosition = transform.position;
	}

}
