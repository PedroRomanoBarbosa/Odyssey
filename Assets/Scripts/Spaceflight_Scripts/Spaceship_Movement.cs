using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship_Movement : MonoBehaviour
{
    private float shipSpeed;
    public float minSpeed = 0f;
    public float maxSpeed = 100f;

    void Start()
    {
        shipSpeed = minSpeed;
    }
    
    void Update()
    {
        //Forward Movement Test
        if (Input.GetKey(KeyCode.W))
        {
            ;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            ;
        }
        transform.Translate(0, 0, Time.deltaTime * shipSpeed);
        
    }
}
