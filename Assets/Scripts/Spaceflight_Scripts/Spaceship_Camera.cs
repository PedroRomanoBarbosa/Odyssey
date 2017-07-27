using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityStandardAssets.ImageEffects;
using UnityEngine.SceneManagement;

public class Spaceship_Camera : MonoBehaviour {
	//Some access to the ship script will be required.
	public GameObject playerShip;
	Transform playerTransform;
	Spaceship_Movement playerScript;

	//Direct Mouse control variables
	private Vector3 mouseCenter;
	float noTurn = 0.1f; //If the mouse is within the middle 10% of the screen, it shouldn't turn.
	int planarFactor = 150; //Controls how fast the camera shifts its angle horizontally and vertically
	int rotationFactor = 10; //Controls how fast you can rotate the view with Q/E
	//Camera Delay and Boost variables
	public float cameraDelay = 0.3f; //The smaller this value, the further behind the camera will be while chasing the player. Should be between 0 and 1
	private float initialCameraDelay;
	bool boostCheck = false;
	float boostDelayFactor = 1.0f;

	//Boundary variables
	//Note: The space boundaries should be a sphere, centered at the origin (0,0,0)
	public int SpaceSize = 1000;
	ScreenOverlay overlayScript;
	public AudioSource warningSignal;

	//Planet Selection Interface
	public GameObject SelectionUI;
	public Image whiteFadeScreen;
	SelectionInterface selectionUIScript;
	float sequenceTimer = 0f;
	bool isScreenFading = false;
	bool isSceneLoading = false;
	Object sceneToLoad;

	//Camera Movement Guide
	GameObject guide;

	//Swapping modes
	bool isLoadingPlanet = false;
	bool requireReturnToChase = false;

    //Canvas HUD
    public GameObject pause;

	void Start(){
		playerTransform = playerShip.transform; 
		playerScript = playerShip.GetComponent<Spaceship_Movement>();
		overlayScript = GetComponent<ScreenOverlay>();
		selectionUIScript = SelectionUI.GetComponent<SelectionInterface>();
		initialCameraDelay = cameraDelay;

		//Find the point in the center of the screen
		mouseCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);

		//Set fade screen to transparent
		whiteFadeScreen.gameObject.SetActive(false);

        //Don´t destroy when loading another scene
		if (GameObject.Find ("Pause") != null) {
             pause = GameObject.Find ("Pause");
         } else {
             pause = Instantiate(pause);
			 pause.transform.name = "Pause";
         }
		DontDestroyOnLoad(pause);

    }
	void LateUpdate () {
		//If the player is getting closer to the space boundaries, the camera should have a warning effect
		if(overlayScript != null)
			warningOverlayIntensity();
		
		if(playerScript.isOutsideBounds()) 
			cameraBehaviour_OutOfBounds();
		else if(isLoadingPlanet)
			cameraBehaviour_LoadingPlanet();
		else if(playerScript.isSelectingPlanet())
			cameraBehaviour_PlanetSelection();
		else if(requireReturnToChase)
			cameraBehaviour_ReturningToChase();
		else 
			cameraBehaviour_ChaseSpaceship();
 	}


	void cameraBehaviour_ChaseSpaceship(){
		//How fast the ship can roll should depend on the ship's speed. But with a minimum value of rotation
		planarFactor = playerScript.shipForwardSpeed<10 ? 30 : (int)playerScript.shipForwardSpeed*3;
		rotationFactor = playerScript.shipForwardSpeed<10 ? 10 : (int)playerScript.shipForwardSpeed; 

		//If the ship is boosting, the camera should have a take longer to trail the ship
		//Makes it look FAAAAAST!!!
		cameraDelay = determineCameraDelay();


		//Figure out how far from the center of the screen the mouse is
		//and rotate camera smoothly to that spot 
		var delta = (Input.mousePosition - mouseCenter) / Screen.height;
		if (delta.y > noTurn) 
			transform.Rotate(-(delta.y - noTurn) * Time.smoothDeltaTime * planarFactor, 0, 0);
		if (delta.y < -noTurn)
			transform.Rotate(-(delta.y + noTurn) * Time.smoothDeltaTime * planarFactor, 0, 0);
		if (delta.x > noTurn)
			transform.Rotate(0, (delta.x - noTurn) * Time.smoothDeltaTime * planarFactor, 0);
		if (delta.x < -noTurn)
			transform.Rotate(0, (delta.x + noTurn) * Time.smoothDeltaTime * planarFactor, 0); 

		//Rolling the Camera
		if (Input.GetKey(KeyCode.E))
		{
			transform.Rotate(0,0,Time.smoothDeltaTime * -rotationFactor);
			playerScript.maneuverSound.volume = playerScript.shipForwardSpeed;
		}
		else if (Input.GetKey(KeyCode.Q))
		{
			transform.Rotate(0,0,Time.smoothDeltaTime * rotationFactor);
			playerScript.maneuverSound.volume = playerScript.shipForwardSpeed;
		} else {
			playerScript.maneuverSound.volume = 0;
		}

		//Find a point behind and above the player ship and go there smoothly
		Vector3 targetPosition = playerTransform.position - playerTransform.forward * 5 + playerTransform.up * 1;
		transform.position = Vector3.Lerp(transform.position, targetPosition, cameraDelay);

		//Garbage collection
		if(guide != null)
			Destroy(guide);

	}
	void cameraBehaviour_OutOfBounds(){
		//While the ship is performing out of bounds actions, the camera should follow its location without moving
		Quaternion targetRotation = Quaternion.LookRotation(playerTransform.position - transform.position);
   		transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * 90f);

		//Flag that the ship has been outside
		if (!requireReturnToChase) requireReturnToChase = true;
 
	}
	void cameraBehaviour_ReturningToChase(){
		if(playerScript.actionTimer > 0){
			//Find a point behind and above the player ship and go there quickly!
			Vector3 targetPosition = playerTransform.position - playerTransform.forward * 5 + playerTransform.up * 1;
			transform.position = Vector3.Lerp(transform.position, targetPosition, playerScript.actionTimer);

			//Rotate the camera to the ship's facing
			transform.rotation = Quaternion.Slerp(transform.rotation, playerTransform.transform.rotation, playerScript.actionTimer);
		} else {
			requireReturnToChase = false;
		}
	}
	void cameraBehaviour_PlanetSelection(){
		//Flag that the ship has been in selection mode
		if (!requireReturnToChase){
			requireReturnToChase = true;
			selectionUIScript.selectionEnabled = false;

			//Fade Out the Ship Driving UI elements
			playerScript.speedImage.CrossFadeAlpha(0f, 2, false);
			playerScript.speedImage.transform.parent.GetComponent<Image>().CrossFadeAlpha(0f, 2, false);
			playerScript.fuelImage.CrossFadeAlpha(0f, 2, false);
			playerScript.fuelImage.transform.parent.GetComponent<Image>().CrossFadeAlpha(0f, 2, false);

			//Tell the UI to enable itself and appear
			selectionUIScript.updateVars(playerScript.getPlanetVars());
			selectionUIScript.setToAppear();			
		}

		PlanetSelectionVars vars = playerScript.getPlanetVars();
		if(guide == null)
			guide = new GameObject();

		//Get a vector between the planet and the origin
		Vector3 direction = Vector3.zero - vars.planetPosition;
		//Uniformize that vector to the planet's size
		direction = direction.normalized;
		//Find a spot for the camera to go to
		Vector3 targetPosition = vars.planetPosition + direction*2*vars.planetSize;
		//Send Guide there and make it look at the planet
		guide.transform.position = targetPosition;
		guide.transform.LookAt(vars.planetPosition, Vector3.up);
		//Find a spot to the side of the planet to look at
		Vector3 targetLookAt =  vars.planetPosition + guide.transform.right*1.2f*vars.planetSize;
		
		//Move camera and change rotation
		transform.position = Vector3.Lerp(transform.position, guide.transform.position, 0.05f);
		transform.LookAt(targetLookAt, Vector3.up);

		//Disable Interface through Escape Key
		if(selectionUIScript.selectionEnabled) {
			if(Input.GetKey(KeyCode.Escape) && !playerScript.isLeavingPlanet())
				leavePlanet();
		}

	}
	void cameraBehaviour_LoadingPlanet(){
		sequenceTimer -= Time.deltaTime;
		PlanetSelectionVars vars = playerScript.getPlanetVars();

		//First make the camera face the planet (animation runs from sequenceTimer [10, 6])
		if(sequenceTimer > 5){
			//Discover intended direction vector for the camera t look at
			Vector3 direction = vars.planetPosition - transform.position;
			direction = direction.normalized;
			//Make sure the camere is aligned upwards
			//Make target rotation object
			Quaternion targetRotation = Quaternion.LookRotation(direction);

			//Discover angle between current and final rotations
        	float angle = Vector3.Angle(direction, transform.forward);
			//Discover time left
			float timeRemaining = sequenceTimer - 6;
			if (timeRemaining<0) timeRemaining = 0.5f;
			//Angular speed
			float speed = angle/timeRemaining;

			//Rotate there slowly
			transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);
		}
		//Then make it approach it and fade the screen white (animation runs from sequenceTimer [5, 0])
		else if(sequenceTimer > -1)
		{
			//Tell white screen to fade in
			if(!isScreenFading){
				isScreenFading = true;
				whiteFadeScreen.gameObject.SetActive(true);
				whiteFadeScreen.canvasRenderer.SetAlpha(0);
				whiteFadeScreen.CrossFadeAlpha(1,5,false);
			}

			//Make sure we are facing the planet
			transform.LookAt(vars.planetPosition);

			//Get a vector between planet and camera
			Vector3 direction = transform.position - vars.planetPosition;
			direction = direction.normalized;
			//Create a Guide object
			if(guide == null){
				guide = new GameObject();
				guide.name = "CameraGuide";
			}
			//Send the guide to a spot between the camera and the planet's surface
			guide.transform.position = vars.planetPosition + direction*vars.planetSize;

			//Figure out where the camera started
			Vector3 movementStart = vars.planetPosition + direction*2f*vars.planetSize;
			//Movement's Distance and Movement Speed
			float distance = Vector3.Distance(movementStart, guide.transform.position);
			float speed =  distance/5;
			//Fraction of travel covered
			float fraction = ((5 - sequenceTimer) * speed)/distance;

			//Move the camera towards the guide
			transform.position = Vector3.Lerp(movementStart, guide.transform.position, fraction);
		}
		else{
			//Load the thing already!
			if(!isSceneLoading){
				isSceneLoading = true;
				Debug.Log(sceneToLoad.name);				
				SceneManager.LoadScene (sceneToLoad.name);
			}
		}

	}

	float determineCameraDelay(){
		//Reset Behaviour if player is boosting.
		if(playerScript.isBoosting()) {
			boostCheck = true;
			boostDelayFactor = 3.0f;
		} //Smoothly decrease how far the camera trails behind the player once the boost is over
		else if (boostCheck){
			boostDelayFactor -= Time.smoothDeltaTime;
			if(boostDelayFactor < 1.0f)
			{
				boostDelayFactor = 1.0f;
				boostCheck = false;
			}			
		
		}
		//Once the player stops boosting, the camera should drag back to the player slowly.
		return initialCameraDelay/boostDelayFactor;
	}

	void warningOverlayIntensity(){
		//Check how far the player is from the origin
		float dist = Vector3.Distance(playerShip.transform.position, Vector3.zero);
		//If he's gone, do max intensity
		if(dist > SpaceSize){
			overlayScript.intensity = 3;
			return;
		}

		//Check how far away ahead of the outtermost 10% he got
		float outterLength = SpaceSize/10;
		float diff = (SpaceSize - outterLength) - dist;

		//if he isn't outside the outtermost 10%, no need to warn him
		if(diff > 0){
			overlayScript.intensity = 0;
			warningSignal.volume = 0;
		} 
		//Otherwise, intensity is a percentage of how far he's gone
		else {
			overlayScript.intensity =  -diff*3/outterLength;
			warningSignal.volume = -diff/outterLength;
		}

	
	}

	public void leavePlanet(){
		//Trigger planet leaving movement
		playerScript.setLeavingPlanet();

		//Tell the selection UI to go away and disable itself
		selectionUIScript.setToDisappear();

		//Fade In the Ship Driving UI elements
		playerScript.speedImage.CrossFadeAlpha(1f, 2, false);
		playerScript.speedImage.transform.parent.GetComponent<Image>().CrossFadeAlpha(1f, 2, false);
		playerScript.fuelImage.CrossFadeAlpha(1f, 2, false);
		playerScript.fuelImage.transform.parent.GetComponent<Image>().CrossFadeAlpha(1f, 2, false);
	}
	public void loadPlanetScene(Object scene){
		Debug.Log("Camera Told to load planet, no going back!");

		//Tell the selection UI to go away and disable itself
		selectionUIScript.setToDisappear();

		//Setup an animation timer
		sequenceTimer = 10;

		//Switch to planet approach mode
		isLoadingPlanet = true;

		//Store which scene to load later
		sceneToLoad = scene;
	}

}
