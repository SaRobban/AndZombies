using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundPlayer : MonoBehaviour
{
    public List<AudioClip> audioClips = new List<AudioClip>();

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound()
    {
        if (!audioSource.isPlaying)
        {
            AudioClip clip = audioClips[Random.Range(0, audioClips.Count)];

            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}
