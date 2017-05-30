using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionInterface : MonoBehaviour {

	public Text planetName;
	public Button enterPlanetButton;
	public Button enterMoonButton;
	public Button leavePlanetButton;
	
	private Text enterPlanet;
	private Text enterMoon;
	private Text leavePlanet;

	[HideInInspector]
	public bool selectionEnabled;

	private float timer = 10f;


	// Use this for initialization
	void Start () {
		enterPlanet = enterPlanetButton.transform.GetChild(0).GetComponent<Text>();
		enterMoon = enterMoonButton.transform.GetChild(0).GetComponent<Text>();
		leavePlanet = leavePlanetButton.transform.GetChild(0).GetComponent<Text>();
		
		selectionEnabled = false;

		
		planetName.GetComponent<CanvasRenderer>().SetAlpha(0f);
		enterPlanetButton.image.GetComponent<CanvasRenderer>().SetAlpha(0f);
		enterMoonButton.image.GetComponent<CanvasRenderer>().SetAlpha(0f);
		leavePlanetButton.image.GetComponent<CanvasRenderer>().SetAlpha(0f);
		enterPlanet.GetComponent<CanvasRenderer>().SetAlpha(0f);
		enterMoon.GetComponent<CanvasRenderer>().SetAlpha(0f);
		leavePlanet.GetComponent<CanvasRenderer>().SetAlpha(0f);

		enterPlanetButton.interactable = false;
		enterMoonButton.interactable = false;
		leavePlanetButton.interactable = false;
		
	}
	
	// Update is called once per frame
	void Update () {
	}


	//Update Text based on planet variabless
	void updateText(PlanetSelectionVars vars){

	}

	private void fadeOut(){

	}
	private void fadeIn_text(int time){
    	planetName.CrossFadeAlpha(1f, time, false);
	}
	private void fadeIn_buttons(int time){
	}
}
