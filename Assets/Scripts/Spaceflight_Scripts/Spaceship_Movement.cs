using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Spaceship_Movement : MonoBehaviour
{
    public GameObject shipCamera;

    //Space and Planet Bounds controller bools - changes behaviour.
    bool OutsideBounds = false;
    bool SelectingPlanet = false;

    //Variables related to forward momentum
	[HideInInspector]
    public float shipForwardSpeed;
    public float minSpeed = 0f;
    public float maxSpeed = 50f;
    private float acceleration, deceleration;

    //Text Display Object
    public Text displaySpeed;

    //SpeedBoost
    private bool boosting = false;
    private float boostTime = 0f;

    //Action Timer for Cinematic effects
    public float actionTimer = 0f;

    //Planet Selection variables
    bool planetLeaving = false;
    PlanetSelectionVars planetVars;
    Vector3 previousPosition;
    float startTime;

    void Awake()
    {
        Application.targetFrameRate = 60;
    }

    void Start()
    {
        shipForwardSpeed = minSpeed;
    }

    void Update()
    {
        //Check if boosting is active and deactivate once over
        if(boosting){
            boostTime -= Time.smoothDeltaTime;
            if(boostTime < 0)
                boosting = false;
        }

        //Count Down Action Timer
        if(actionTimer >= 0)
            actionTimer -= Time.smoothDeltaTime;

        //Behaviour based on player state
        if(OutsideBounds) 
			spaceshipBehaviour_OutOfBounds();
		else if(SelectingPlanet)
			spaceshipBehaviour_PlanetSelection();
		else 
			spaceshipBehaviour_CameraChase();

        //Update Speed Display
        if(displaySpeed != null){
            displaySpeed.text = 
                "Speed: " + shipForwardSpeed.ToString() + "\n" + 
                "Boosting: " + boosting.ToString();
            Needle.MoveNeedle(shipForwardSpeed, maxSpeed, minSpeed);
        }

    }


    void spaceshipBehaviour_CameraChase(){
        //Forward Movement - acceleration setting
        acceleration = maxSpeed - shipForwardSpeed;
        deceleration = shipForwardSpeed - minSpeed;

        //Boost's effect
        if(boosting)
            acceleration = maxSpeed*1.5f - shipForwardSpeed;
        if(!boosting && shipForwardSpeed > maxSpeed)
            shipForwardSpeed += acceleration * Time.smoothDeltaTime;

        //Forward Movement
        if (Input.GetKey(KeyCode.Mouse0))
        {
            shipForwardSpeed += acceleration * Time.smoothDeltaTime;
        }
        else if (Input.GetKey(KeyCode.Mouse1))
        {
            shipForwardSpeed -= deceleration * Time.smoothDeltaTime;
        }

        //THIS HURTS EVERY TIME I SEE IT
        shipForwardSpeed = Mathf.Round(shipForwardSpeed * 100)/100;

        //Apply Momentum
        Vector3 targetPosition = transform.position + transform.forward * shipForwardSpeed;
		transform.position = Vector3.Lerp(transform.position, targetPosition, Time.smoothDeltaTime);

        //Update directional facing based on camera
        if(actionTimer < 0)
            transform.rotation = Quaternion.Slerp(transform.rotation, shipCamera.transform.rotation, 0.1f);

    }
    void spaceshipBehaviour_OutOfBounds(){
        //When the ship leaves, it should initially go forward, spin a bit, and then 
        //turn around towards the center of the space area.  

        //Forward Momentum
        Vector3 targetPosition = transform.position + transform.forward * shipForwardSpeed;
		transform.position = Vector3.Lerp(transform.position, targetPosition, Time.smoothDeltaTime);

        if(actionTimer > 0){
            //Barrelrolling the ship
            transform.Rotate(new Vector3(0,0,Time.smoothDeltaTime * 360));
        } else {
            //Spin the ship back towards the origin
            Quaternion targetRotation = Quaternion.LookRotation(Vector3.zero - transform.position);
   		    transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * 60f);
        }

    }
    void spaceshipBehaviour_PlanetSelection(){
        //The ship should rotate around the planet being selected.
        if(planetVars != null){
            if(!planetLeaving){
                /*
                    Orbiting the planet
                 */

                //Movement
                transform.RotateAround(planetVars.planetPosition, Vector3.down, 60f * Time.deltaTime);
                Vector3 orbitDesiredPosition = (transform.position - planetVars.planetPosition).normalized * planetVars.orbitRadius + planetVars.planetPosition;
                transform.position = Vector3.Slerp(transform.position, orbitDesiredPosition, Time.deltaTime * 0.5f);
                //Rotation
                Vector3 relativePos = transform.position - previousPosition;
                Quaternion rotation = Quaternion.LookRotation(relativePos);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.5f * Time.deltaTime);
                previousPosition = transform.position;


            } else {
                /*
                    Multiple step motion to abandon orbit, crossing near the camera
                 */
                if (actionTimer > 0){
                    //Find a spot within the camera's focus to move to
                    Vector3 targetPosition = shipCamera.transform.position + shipCamera.transform.forward*5 - shipCamera.transform.up + shipCamera.transform.right*3;
                    //Move there using ActionTimer
                    float fracJourney = (3-actionTimer)/3;
                    transform.position = Vector3.Slerp(previousPosition, targetPosition, fracJourney);
                    //The ship faces the origin
                    transform.LookAt(targetPosition);     
                } else {
                    transform.LookAt(Vector3.zero);  
                    SelectingPlanet = false;
                    planetLeaving = false;
                    actionTimer = 2f;
                    shipForwardSpeed = 15f;
                }
            }
            
        } else {
            Debug.Log("MISSING PLANET VARS!!");
            SelectingPlanet = false;
            actionTimer = -1f;
        }
    }

    public void initiateBoost(){
        boosting = true;
        boostTime = 10f;
    }
    public bool isBoosting(){
        return boosting;
    }
    public void setOutsideBounds(){
        OutsideBounds = true;
        shipForwardSpeed = 25f;
        actionTimer = 3f;
    }
    public void unsetOutsideBounds(){
        OutsideBounds = false;
        shipForwardSpeed = 10f;
        actionTimer = 3f;
    }
    public bool isOutsideBounds(){
        return OutsideBounds;
    }
    public void setPlanetSelection(PlanetSelectionVars vars){
        SelectingPlanet = true;
        planetVars = vars;
    }
    public bool isSelectingPlanet(){
        return SelectingPlanet;
    }
    public PlanetSelectionVars getPlanetVars(){
        return planetVars;
    }
    public void setLeavingPlanet(){
        planetLeaving = true;
        actionTimer = 3.0f;
    }
}
