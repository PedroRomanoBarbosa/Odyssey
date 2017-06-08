using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolSwitch : MonoBehaviour
{
    public GameObject flame;
    public GameObject minning;
    public GameObject missile;
    public GameObject can;
    private Tool tool;

	void Update ()
    {
        if(tool != null)
        {
            Debug.Log(tool.name);
            if (tool.name == "MiningPick")
            {
                flame.SetActive(false);
                missile.SetActive(false);
                can.SetActive(false);
                minning.SetActive(true);
            }
            else if (tool.name == "MissileLauncher")
            {
                flame.SetActive(false);
                can.SetActive(false);
                minning.SetActive(false);
                missile.SetActive(true);
            }
            else if (tool.name == "Flamethrower")
            {
                can.SetActive(false);
                minning.SetActive(false);
                missile.SetActive(false);
                flame.SetActive(true);
            }
            else if (tool.name == "WateringCan")
            {
                minning.SetActive(false);
                missile.SetActive(false);
                flame.SetActive(false);
                can.SetActive(true);
            }
            else
            {
                minning.SetActive(false);
                missile.SetActive(false);
                flame.SetActive(false);
                can.SetActive(false);
            }
        }
	}

    public void SetTool(Tool t)
    {
        tool = t;
    }
}
