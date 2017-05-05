using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship_Camera : MonoBehaviour {

	public GameObject playerShip;
	Transform playerTransform;

	private Vector3 mouseCenter;
	float noTurn = 0.1f;
	int factor = 150;

	void Start(){
		playerTransform = playerShip.transform; 

		//Find the point in the center of the screen
		mouseCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
	}
	void Update () {

		//Figure out how far from the center of the screen the mouse is
		//and rotate camera smoothly to that spot 
		var delta = (Input.mousePosition - mouseCenter) / Screen.height;
		if (delta.y > noTurn) 
			transform.Rotate(-(delta.y - noTurn) * Time.deltaTime * factor, 0, 0);
		if (delta.y < -noTurn)
			transform.Rotate(-(delta.y + noTurn) * Time.deltaTime * factor, 0, 0);
		if (delta.x > noTurn)
			transform.Rotate(0, (delta.x - noTurn) * Time.deltaTime * factor, 0);
		if (delta.x < -noTurn)
			transform.Rotate(0, (delta.x + noTurn) * Time.deltaTime * factor, 0); 

		//Find a point behind and above the player ship and go there smoothly
		Vector3 targetPosition = playerTransform.position - playerTransform.forward * 5 + playerTransform.up * 1;
		transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime*50);
 	}
  	

}
