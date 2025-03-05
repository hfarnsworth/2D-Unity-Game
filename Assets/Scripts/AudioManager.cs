using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        //_audioSource = GetComponent<AudioSource>();
        //if (_audioSource == null) Debug.LogError("AudioSource on Audio Manager is NULL.");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound(AudioClip sound)
    {
        //_audioSource.clip = sound;
        //_audioSource.Play();
    }
}
