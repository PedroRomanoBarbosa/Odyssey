using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Needle : MonoBehaviour
{
    public float minAngle;
    public float maxAngle;

    public void MoveNeedle(float speed, float maxSpeed, float minSpeed)
    {
        float ang = Mathf.Lerp(minAngle, maxAngle, Mathf.InverseLerp(minSpeed, maxSpeed, speed));
        this.transform.eulerAngles = new Vector3(0, 0, ang);
    }
}
