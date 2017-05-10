﻿using System.Collections;
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
    public float shipForwardSpeed;
    public float minSpeed = 0f;
    public float maxSpeed = 50f;
    private float acceleration, deceleration;

    //Text Display Object
    public Text displaySpeed;

    //SpeedBoost
    private bool boosting = false;
    private float boostTime = 0f;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
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
        shipForwardSpeed = Mathf.Round(shipForwardSpeed * 100)/100; //WHYYYYYY
        Vector3 targetPosition = transform.position + transform.forward * shipForwardSpeed;
		transform.position = Vector3.Lerp(transform.position, targetPosition, Time.smoothDeltaTime);

        //Update directional facing based on camera
        transform.rotation = Quaternion.Slerp(transform.rotation, shipCamera.transform.rotation, 0.1f);

        //Update Speed Display
        if(displaySpeed != null){
            displaySpeed.text = 
                "Speed: " + shipForwardSpeed.ToString() + "\n" + 
                "Boosting: " + boosting.ToString();
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
    }
    public bool isOutsideBounds(){
        return OutsideBounds;
    }
    public void setPlanetSelection(){
        OutsideBounds = true;
    }
    public bool isSelectingPlanet(){
        return OutsideBounds;
    }
}
