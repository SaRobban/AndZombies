using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundPlayer : MonoBehaviour
{
    public List<AudioClip> audioClips = new List<AudioClip>();

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {

        if (audioClips.Count == 0)
        {
            audioSource.volume = SettingMenu.Instance.GetVolume();
            audioSource.Play();
        }
    }

    public void PlaySound()
    {
        audioSource.volume = SettingMenu.Instance.GetVolume();

        if (!audioSource.isPlaying)
        {
            if (audioClips.Count != 0)
            {
                AudioClip clip = audioClips[Random.Range(0, audioClips.Count)];

                audioSource.clip = clip;
            }

            audioSource.Play();
        }
    }
}
