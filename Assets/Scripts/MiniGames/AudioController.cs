using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioSource musicSource;
    public AudioClip[] sfx;
    public AudioClip music;
    public AudioClip musicLoop;

    // Start is called before the first frame update
    void Start()
    {
        musicSource.PlayOneShot(music);
        musicSource.PlayDelayed(music.length);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
