using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SO;

public  class Soundtest:MonoBehaviour
{
    public static void bgmOn (AudioClip clip) {
        GameObject sound = Instantiate(SO_Player.Entity.bgmOn);
        sound.GetComponent<AudioSource>().clip = clip; 
    
    }
    public static void seOn(AudioClip clip)
    {
        GameObject sound = Instantiate(SO_Player.Entity.seOn);
        sound.GetComponent<AudioSource>().clip = clip;

    }
}
