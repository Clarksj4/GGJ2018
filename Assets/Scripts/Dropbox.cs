using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class Dropbox : MonoBehaviour
{
    public proper_destinations DropDestination;
    private new AudioSource audio;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    public void Play()
    {
        if (audio != null)
            audio.Play();
    }
}
