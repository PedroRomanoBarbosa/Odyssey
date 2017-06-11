using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionInterface : MonoBehaviour {

    public GameObject shipCamera;
    private Spaceship_Camera cameraScript;
	public AudioClip planetApproachStinger;

	public Text planetName;
	public Text planetDescription;
	public Button enterPlanetButton;
	public Button enterMoonButton;
	public Button leavePlanetButton;

	private Text enterPlanet;
	private Text enterMoon;
	private Text leavePlanet;

	private PlanetSelectionVars selectedPlanetVars;

	[HideInInspector]
	public bool selectionEnabled;

	private bool counting = false;
	private float timer = -10f;

	private bool showMoon = false;
	private bool alreadyFadedText = false;
	private bool alreadyFadedButtons = false;


	// Use this for initialization
	void Start () {
		cameraScript = shipCamera.GetComponent<Spaceship_Camera>();

		enterPlanet = enterPlanetButton.transform.GetChild(0).GetComponent<Text>();
		enterMoon = enterMoonButton.transform.GetChild(0).GetComponent<Text>();
		leavePlanet = leavePlanetButton.transform.GetChild(0).GetComponent<Text>();
		
		planetName.GetComponent<CanvasRenderer>().SetAlpha(0f);
		planetDescription.GetComponent<CanvasRenderer>().SetAlpha(0f);
		enterPlanetButton.image.GetComponent<CanvasRenderer>().SetAlpha(0f);
		enterMoonButton.image.GetComponent<CanvasRenderer>().SetAlpha(0f);
		leavePlanetButton.image.GetComponent<CanvasRenderer>().SetAlpha(0f);
		enterPlanet.GetComponent<CanvasRenderer>().SetAlpha(0f);
		enterMoon.GetComponent<CanvasRenderer>().SetAlpha(0f);
		leavePlanet.GetComponent<CanvasRenderer>().SetAlpha(0f);

		leavePlanetButton.GetComponent<Button>().onClick.AddListener(leavePlanetPressed);
		enterPlanetButton.GetComponent<Button>().onClick.AddListener(enterPlanetPressed);
		enterMoonButton.GetComponent<Button>().onClick.AddListener(enterMoonPressed);

		enterPlanetButton.interactable = false;
		enterMoonButton.interactable = false;
		leavePlanetButton.interactable = false;

		selectionEnabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(counting){
			if(timer > 0)
				timer -= Time.deltaTime;
			else
				activateUI();

			if(timer < 5 && !alreadyFadedText){
				fadeIn_text(5);
				//Stinger
				if(planetApproachStinger != null)
					AudioSource.PlayClipAtPoint(planetApproachStinger, shipCamera.transform.position);
			}
			if(timer < 2 && !alreadyFadedButtons)
				fadeIn_buttons(2);
		}
	}

	//Public Functions for the Camera to Invoke
	public void updateVars(PlanetSelectionVars vars){
		//Store vars
		selectedPlanetVars = vars;

		//Update Text
		planetName.text = vars.planetName;
		planetDescription.text = vars.description;

		//Change moon flag
		if(vars.moonScene != null)
			showMoon = true;
		else
			showMoon = false;
	}
	public void setToAppear(){
		Debug.Log("Selection interface timer started");

		timer = 8f;
		counting = true;
		alreadyFadedText = false;
		alreadyFadedButtons = false;
	}
	public void setToDisappear(){
		selectionEnabled = false;
		fadeOut(3);
		showMoon = false;
	}


	//Private Helper Functions
	private void fadeOut(int time){
		Debug.Log("Interface set to fadeout");
		planetName.CrossFadeAlpha(0, time, false);
		planetDescription.CrossFadeAlpha(0, time, false);
		enterPlanetButton.image.CrossFadeAlpha(0, time, false);
		enterMoonButton.image.CrossFadeAlpha(0, time, false);
		leavePlanetButton.image.CrossFadeAlpha(0, time, false);
		enterPlanet.CrossFadeAlpha(0, time, false);
		enterMoon.CrossFadeAlpha(0, time, false);
		leavePlanet.CrossFadeAlpha(0, time, false);
		enterPlanetButton.interactable = false;
		if(showMoon)
			enterMoonButton.interactable = false;
		leavePlanetButton.interactable = false;
	}
	private void fadeIn_text(int time){
		Debug.Log("Planet Name fading in");
		alreadyFadedText = true;
    	planetName.CrossFadeAlpha(1f, time, false);
    	planetDescription.CrossFadeAlpha(1f, time, false);
	}
	private void fadeIn_buttons(int time){
		Debug.Log("Buttons fading in");
		alreadyFadedButtons = true;
		if(!selectedPlanetVars.barrier){
			enterPlanet.CrossFadeAlpha(1f, time, false);
			if(showMoon)
				enterMoon.CrossFadeAlpha(1f, time, false);
		}
		leavePlanet.CrossFadeAlpha(1f, time, false);

	}
	private void activateUI(){
		Debug.Log("Interface made interactible");

		selectionEnabled = true;
		counting = false;
		if(!selectedPlanetVars.barrier){
			enterPlanetButton.interactable = true;
			if(showMoon)
				enterMoonButton.interactable = true;
		}
		leavePlanetButton.interactable = true;
	}


	//Button functions
	private void leavePlanetPressed(){
		Debug.Log("You have clicked to Leave!");
		cameraScript.leavePlanet();
	}
	private void enterPlanetPressed(){
		Debug.Log("You have clicked to enter Planet!");
		cameraScript.loadPlanetScene(selectedPlanetVars.planetScene);
	}
	private void enterMoonPressed(){
		Debug.Log("You have clicked to enter Moon!");
		cameraScript.loadPlanetScene(selectedPlanetVars.moonScene);
	}
}
