using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

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

	//Planet Selection Interface
	bool disableUI = true;
	float waitForUI = 0f;

	//Camera Movement Guide
	GameObject guide;

	//Swapping modes
	bool requireReturnToChase = false;


	void Start(){
		playerTransform = playerShip.transform; 
		playerScript = playerShip.GetComponent<Spaceship_Movement>();
		overlayScript = GetComponent<ScreenOverlay>();
		initialCameraDelay = cameraDelay;

		//Find the point in the center of the screen
		mouseCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
	}
	void LateUpdate () {
		//If the player is getting closer to the space boundaries, the camera should have a warning effect
		if(overlayScript != null)
			warningOverlayIntensity();
		
		if(playerScript.isOutsideBounds()) 
			cameraBehaviour_OutOfBounds();
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
		}
		else if (Input.GetKey(KeyCode.Q))
		{
			transform.Rotate(0,0,Time.smoothDeltaTime * rotationFactor);
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
			waitForUI = 3f;
			requireReturnToChase = true;
			disableUI = true;
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


		//Interface timer
		if(waitForUI > 0 && disableUI) 
			waitForUI -= Time.deltaTime; 
		else
			disableUI = false;

		//Interface Control
		if(!disableUI) {
			if(Input.GetKey(KeyCode.Escape) ){
				playerScript.setLeavingPlanet();
				disableUI = true;
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
		} 
		//Otherwise, intensity is a percentage of how far he's gone
		else {
			overlayScript.intensity =  -diff*3/outterLength;
		}

	}

}
