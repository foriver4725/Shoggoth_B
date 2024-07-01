using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SO;

public class Soundtest : MonoBehaviour
{
    public static AudioSource bgmOn(AudioClip clip)
    {
        GameObject sound = Instantiate(SO_Player.Entity.bgmOn);
        AudioSource source = sound.GetComponent<AudioSource>();
        source.clip = clip;
        return source;
    }

    public static AudioSource seOn(AudioClip clip)
    {
        GameObject sound = Instantiate(SO_Player.Entity.seOn);
        AudioSource source = sound.GetComponent<AudioSource>();
        source.clip = clip;
        return source;
    }
}
