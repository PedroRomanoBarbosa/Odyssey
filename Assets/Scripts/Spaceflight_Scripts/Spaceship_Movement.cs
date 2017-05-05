using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Spaceship_Movement : MonoBehaviour
{
    public GameObject shipCamera;

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

    void Start()
    {
        shipForwardSpeed = minSpeed;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        //Check if boosting is active and deactivate once over
        if(boosting){
            boostTime -= Time.deltaTime;

            if(boostTime < 0)
                boosting = false;
        }
    }

    void LateUpdate()
    {


        //Forward Movement - acceleration setting
        acceleration = maxSpeed - shipForwardSpeed;
        deceleration = shipForwardSpeed - minSpeed;

        //Boost's effect
        if(boosting)
            acceleration = maxSpeed*1.5f - shipForwardSpeed;
        if(!boosting && shipForwardSpeed > maxSpeed)
            shipForwardSpeed += acceleration * Time.deltaTime;

        //Forward Movement
        if (Input.GetKey(KeyCode.Mouse0))
        {
            shipForwardSpeed += acceleration * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.Mouse1))
        {
            shipForwardSpeed -= deceleration * Time.deltaTime;
        }
        transform.Translate(0, 0, Time.deltaTime * shipForwardSpeed);

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

}
