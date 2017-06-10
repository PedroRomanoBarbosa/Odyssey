using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    private int musicVolume;
    private int effectsVolume;

	void Start ()
    {
        musicVolume = 1;
        effectsVolume = 1;
	}
	
    void Update()
    {
        Debug.Log(musicVolume);
    }

	public void SetMusicVolume(int v)
    {
        musicVolume = v;
    }

    public void SetEffectsVolume(int v)
    {
        effectsVolume = v;
    }

    public int GetMusicVolume()
    {
        return musicVolume;
    }

    public int GetEffectsVolume()
    {
        return effectsVolume;
    }
}
