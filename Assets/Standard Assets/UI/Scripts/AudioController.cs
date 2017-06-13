using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    private static int musicVolume = 1;
    private static int effectsVolume = 1;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
	
    void Update()
    {
        //Debug.Log(musicVolume);
    }

	public static void SetMusicVolume(int v)
    {
        musicVolume = v;
    }

    public static void SetEffectsVolume(int v)
    {
        effectsVolume = v;
    }

    public static int GetMusicVolume()
    {
        return musicVolume;
    }

    public static int GetEffectsVolume()
    {
        return effectsVolume;
    }
}
