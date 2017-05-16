using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundInput : MonoBehaviour
{
    public Slider soundSlider;
    public Slider effectsSlider;
    public Button backButton;

    void Start ()
    {
        Button button = backButton.GetComponent<Button>();
        button.onClick.AddListener(SubmitSliderValue);
    }

    void SubmitSliderValue()
    {
        Debug.Log("Sound: " + soundSlider.value);
        Debug.Log("Effects: " + effectsSlider.value);
    }
}
