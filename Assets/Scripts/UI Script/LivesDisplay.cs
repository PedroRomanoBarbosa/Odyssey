using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesDisplay : MonoBehaviour
{
    public GameObject live1;
    public GameObject live2;
    public GameObject live3;
    public GameObject live4;
    public GameObject live5;
    public GameObject live6;
    public GameObject live7;
    public GameObject live8;
    public GameObject live9;
    public GameObject live10;

	void Update ()
    {
        //int lives = FindObjectOfType<Player>().lives;
        int lives = GameVariables.lives;

        if (lives == 0)
        {
            live1.SetActive(false);
            live2.SetActive(false);
            live3.SetActive(false);
            live4.SetActive(false);
            live5.SetActive(false);
            live6.SetActive(false);
            live7.SetActive(false);
            live8.SetActive(false);
            live9.SetActive(false);
            live10.SetActive(false);
        }
        else if (lives == 1)
        {
            live1.SetActive(true);
            live2.SetActive(false);
            live3.SetActive(false);
            live4.SetActive(false);
            live5.SetActive(false);
            live6.SetActive(false);
            live7.SetActive(false);
            live8.SetActive(false);
            live9.SetActive(false);
            live10.SetActive(false);
        }	
        else if(lives == 2)
        {
            live1.SetActive(true);
            live2.SetActive(true);
            live3.SetActive(false);
            live4.SetActive(false);
            live5.SetActive(false);
            live6.SetActive(false);
            live7.SetActive(false);
            live8.SetActive(false);
            live9.SetActive(false);
            live10.SetActive(false);
        }
        else if(lives == 3)
        {
            live1.SetActive(true);
            live2.SetActive(true);
            live3.SetActive(true);
            live4.SetActive(false);
            live5.SetActive(false);
            live6.SetActive(false);
            live7.SetActive(false);
            live8.SetActive(false);
            live9.SetActive(false);
            live10.SetActive(false);
        }
        else if(lives == 4)
        {
            live1.SetActive(true);
            live2.SetActive(true);
            live3.SetActive(true);
            live4.SetActive(true);
            live5.SetActive(false);
            live6.SetActive(false);
            live7.SetActive(false);
            live8.SetActive(false);
            live9.SetActive(false);
            live10.SetActive(false);
        }
        else if(lives == 5)
        {
            live1.SetActive(true);
            live2.SetActive(true);
            live3.SetActive(true);
            live4.SetActive(true);
            live5.SetActive(true);
            live6.SetActive(false);
            live7.SetActive(false);
            live8.SetActive(false);
            live9.SetActive(false);
            live10.SetActive(false);
        }
        else if(lives == 6)
        {
            live1.SetActive(true);
            live2.SetActive(true);
            live3.SetActive(true);
            live4.SetActive(true);
            live5.SetActive(true);
            live6.SetActive(true);
            live7.SetActive(false);
            live8.SetActive(false);
            live9.SetActive(false);
            live10.SetActive(false);
        }
        else if(lives == 7)
        {
            live1.SetActive(true);
            live2.SetActive(true);
            live3.SetActive(true);
            live4.SetActive(true);
            live5.SetActive(true);
            live6.SetActive(true);
            live7.SetActive(true);
            live8.SetActive(false);
            live9.SetActive(false);
            live10.SetActive(false);
        }
        else if(lives == 8)
        {
            live1.SetActive(true);
            live2.SetActive(true);
            live3.SetActive(true);
            live4.SetActive(true);
            live5.SetActive(true);
            live6.SetActive(true);
            live7.SetActive(true);
            live8.SetActive(true);
            live9.SetActive(false);
            live10.SetActive(false);
        }
        else if(lives == 9)
        {
            live1.SetActive(true);
            live2.SetActive(true);
            live3.SetActive(true);
            live4.SetActive(true);
            live5.SetActive(true);
            live6.SetActive(true);
            live7.SetActive(true);
            live8.SetActive(true);
            live9.SetActive(true);
            live10.SetActive(false);
        }
        else if(lives == 10)
        {
            live1.SetActive(true);
            live2.SetActive(true);
            live3.SetActive(true);
            live4.SetActive(true);
            live5.SetActive(true);
            live6.SetActive(true);
            live7.SetActive(true);
            live8.SetActive(true);
            live9.SetActive(true);
            live10.SetActive(true);
        }
	}
}
