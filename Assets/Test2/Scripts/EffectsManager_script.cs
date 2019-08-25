using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsManager_script : MonoBehaviour
{
    private AudioSource _audioSource;
    public AudioClip[] AudioClips;


    void Start()
    {
        _audioSource = GetComponents<AudioSource>()[1];
    }

    public void PlayAudioClip(int i)
    {
        float p = Random.Range(0.8f, 1.2f);
        _audioSource.pitch = p;
        _audioSource.PlayOneShot(AudioClips[i]);
    }
}
