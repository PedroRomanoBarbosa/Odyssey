using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectOnInput : MonoBehaviour
{
    private bool buttonSeleted;
    public EventSystem eventSystem;
    public GameObject selectedObject;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetAxisRaw("Vertical")!=0 && buttonSeleted == false)
        {
            eventSystem.SetSelectedGameObject(selectedObject);
            buttonSeleted = true;
        }
	}

    private void OnDisable()
    {
        buttonSeleted = false;
    }
}
