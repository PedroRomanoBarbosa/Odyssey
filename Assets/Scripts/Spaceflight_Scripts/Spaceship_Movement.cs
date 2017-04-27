using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Spaceship_Movement : MonoBehaviour
{
    public float shipSpeed;
    public float minSpeed = 0f;
    public float maxSpeed = 50f;

    private float acceleration, deceleration;

    public Text display_speed;

    void Start()
    {
        shipSpeed = minSpeed;
    }
    
    void Update()
    {
        acceleration = maxSpeed - shipSpeed;
        deceleration = shipSpeed - minSpeed;

        //Forward Movement Test
        if (Input.GetKey(KeyCode.Mouse0))
        {
            shipSpeed += acceleration * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.Mouse1))
        {
            shipSpeed -= deceleration * Time.deltaTime;
        }
        

        transform.Translate(0, 0, Time.deltaTime * shipSpeed);

        //Update Speed Display
        if(display_speed != null)
            display_speed.text = "Speed: " + shipSpeed.ToString();

    }
}
