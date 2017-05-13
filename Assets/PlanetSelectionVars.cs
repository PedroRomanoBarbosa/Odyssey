using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlanetSelectionVars : MonoBehaviour {
	public int orbitRadius;
	public string planetName;
	public Object planetScene;
	[HideInInspector]
	public Vector3 planetPosition;

	void Start()
	{
		planetPosition = transform.position;
	}

}
