using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Spaceship_Movement : MonoBehaviour
{
    public GameObject shipCamera;

    public float shipForwardSpeed;
    public float minSpeed = 0f;
    public float maxSpeed = 50f;
    private float acceleration, deceleration;

    public Text display_speed;

    void Start()
    {
        shipForwardSpeed = minSpeed;
    }
    
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
        transform.rotation = Quaternion.Lerp(transform.rotation, shipCamera.transform.rotation, 0.1f);

        //Update Speed Display
        if(display_speed != null)
            display_speed.text = "Speed: " + shipForwardSpeed.ToString();
        
    }
}
