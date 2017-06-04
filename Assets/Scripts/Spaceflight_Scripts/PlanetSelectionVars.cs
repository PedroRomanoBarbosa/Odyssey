using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlanetSelectionVars : MonoBehaviour {
	public string planetName;
	[MultilineAttribute]
	public string description;
	public Object planetScene;
	public Object moonScene;

	public int orbitRadius;
	public int planetSize;
	public bool barrier = false;

	[HideInInspector]
	public Vector3 planetPosition;

	void Start()
	{
		planetPosition = transform.position;
	}

}
