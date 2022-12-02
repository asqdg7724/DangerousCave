using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMList : MonoBehaviour
{
    AudioSource myAudio;

    public AudioClip[] sounds;


    void Start()
    {
        myAudio = GetComponent<AudioSource>();
    }

    public void BGM_Play(int SoundNumber)
    {
        myAudio.clip = sounds[SoundNumber];

        myAudio.Play();

        myAudio.loop = false;

    }
}
