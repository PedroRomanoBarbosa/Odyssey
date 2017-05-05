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
    private bool isBoosting = false;
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
        if(isBoosting){
            boostTime -= Time.deltaTime;

            if(boostTime < 0)
                isBoosting = false;
        }
    }

    /// <summary>
    /// LateUpdate is called every frame, if the Behaviour is enabled.
    /// It is called after all Update functions have been called.
    /// </summary>
    void LateUpdate()
    {
        //Forward Movement
        acceleration = maxSpeed - shipForwardSpeed;
        deceleration = shipForwardSpeed - minSpeed;
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
                "Boosting: " + isBoosting.ToString();
        }
        
    }

    public void initiateBoost(){
        isBoosting = true;
        boostTime = 15f;
    }

}
