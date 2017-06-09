using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemDisplay : MonoBehaviour
{
    public GameObject cuboid;
    public GameObject star;
    public GameObject penta;
    public GameObject spiral;
    private bool catchCuboid;
    private bool catchStar;
    private bool catchPenta;
    private bool catchSpiral;

    void Start()
    {
        catchCuboid = false;
        catchStar = false;
        catchPenta = false;
        catchSpiral = false;
    }

    void Update ()
    {
	    if(catchCuboid == true)
        {
            cuboid.SetActive(true);
        }
        if(catchStar == true)
        {
            star.SetActive(true);
        }	
        if(catchPenta == true)
        {
            penta.SetActive(true);
        }
        if(catchSpiral == true)
        {
            spiral.SetActive(true);
        }
	}

    public void SetCatchStar(bool b)
    {
        catchStar = b;
    }

    public void SetCatchSpiral(bool b)
    {
        catchSpiral = b;
    }

    public void SetCatchCuboid(bool b)
    {
        catchCuboid = b;
    }

    public void SetCatchPenta(bool b)
    {
        catchPenta = b;
    }
}
