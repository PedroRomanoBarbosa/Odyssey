using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Needle : MonoBehaviour
{
    public static float minAngle = 765f;
    public static float maxAngle = 495f;
    public static Needle needle;
      
    void Start ()
    {
        needle = this;
	}

    public static void MoveNeedle(float speed, float maxSpeed, float minSpeed)
    {
        float ang = Mathf.Lerp(minAngle, maxAngle, Mathf.InverseLerp(minSpeed, maxSpeed, speed));
        needle.transform.eulerAngles = new Vector3(0, 0, ang);
    }
}
