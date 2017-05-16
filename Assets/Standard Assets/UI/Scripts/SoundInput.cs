using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundInput : MonoBehaviour
{
    public Slider soundSlider;
    public Slider effectsSlider;
    public Button button;

    void Start ()
    {
        Button btn = button.GetComponent<Button>();
        btn.onClick.AddListener(SubmitSliderValue);
    }

	void Update ()
    {
		
	}

    void SubmitSliderValue()
    {
        Debug.Log(soundSlider.value);
        Debug.Log(effectsSlider.value);
    }
}
