using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlaySound : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private List<DataSound> _dataSounds = new List<DataSound>();

    

    public void Play(string nameClip)
    {
        var audioClip = GetAudioClip(nameClip);
        _audioSource.clip = audioClip;
        _audioSource.Play();
    }

    public void Stop()
    {
        _audioSource.Stop();
    }

    public AudioClip GetAudioClip(string nameClip)
    {
        AudioClip clip = null;

        foreach (var sound in _dataSounds)
        {
            if (sound.name == nameClip)
                clip = sound.audioClip;
        }

        return clip;
    }

    [Serializable]
    private class DataSound
    {
        public string name;
        public AudioClip audioClip;
    }
}

