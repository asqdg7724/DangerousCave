using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSoundList : MonoBehaviour
{
    AudioSource myAudio;

    public AudioClip[] sounds;


    void Start()
    {
        myAudio = GetComponent<AudioSource>();
    }

    public void ObjectSFX_SoundPlay(int SoundNumber)
    {
        myAudio.clip = sounds[SoundNumber];

        myAudio.Play();

    }
}
