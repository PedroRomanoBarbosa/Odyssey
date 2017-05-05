using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship_Camera : MonoBehaviour {

	//Some access to the ship script will be required.
	public GameObject playerShip;
	Transform playerTransform;
	Spaceship_Movement playerScript;

	private Vector3 mouseCenter;
	float noTurn = 0.1f; //If the mouse is within the middle 10% of the screen, it shouldn't turn.
	int planarFactor = 150; //Controls how fast the camera shifts its angle horizontally and vertically
	int rotationFactor = 10; //Controls how fast you can rotate the view with Q/E

	//The smaller this value, the further behind the camera will be while chasing the player.
	//Should be between 0 and 1
	public float cameraDelay = 0.3f; 
	private float initialCameraDelay;

	void Start(){
		playerTransform = playerShip.transform; 
		playerScript = playerShip.GetComponent<Spaceship_Movement>();
		initialCameraDelay = cameraDelay;

		//Find the point in the center of the screen
		mouseCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
	}
	void Update () {

		//How fast the ship can roll should depend on the ship's speed. But with a minimum value of rotation
		planarFactor = playerScript.shipForwardSpeed<10 ? 30 : (int)playerScript.shipForwardSpeed*3;
		rotationFactor = playerScript.shipForwardSpeed<10 ? 10 : (int)playerScript.shipForwardSpeed; 

		//If the ship is boosting, the camera should have a take longer to trail the ship
		//Makes it look FAAAAAST!!!
		cameraDelay = playerScript.isBoosting() ? initialCameraDelay/3 : initialCameraDelay;

		//Figure out how far from the center of the screen the mouse is
		//and rotate camera smoothly to that spot 
		var delta = (Input.mousePosition - mouseCenter) / Screen.height;
		if (delta.y > noTurn) 
			transform.Rotate(-(delta.y - noTurn) * Time.deltaTime * planarFactor, 0, 0);
		if (delta.y < -noTurn)
			transform.Rotate(-(delta.y + noTurn) * Time.deltaTime * planarFactor, 0, 0);
		if (delta.x > noTurn)
			transform.Rotate(0, (delta.x - noTurn) * Time.deltaTime * planarFactor, 0);
		if (delta.x < -noTurn)
			transform.Rotate(0, (delta.x + noTurn) * Time.deltaTime * planarFactor, 0); 

		//Rolling the Camera
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(0,0,Time.deltaTime * -rotationFactor);
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(0,0,Time.deltaTime * rotationFactor);
        }

		//Find a point behind and above the player ship and go there smoothly
		Vector3 targetPosition = playerTransform.position - playerTransform.forward * 5 + playerTransform.up * 1;
		transform.position = Vector3.Lerp(transform.position, targetPosition, cameraDelay);
 	}
  	

}
