using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour
{
    public static SettingMenu Instance { get { return instance; } }
    private static SettingMenu instance;

    public Slider Slider;
    private float Volume = 0.8f;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        Slider = FindObjectOfType<Slider>();

        gameObject.SetActive(false);
    }

    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }
    }

    private void Update()
    {
        if (Slider == null)
        {
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                Slider = FindObjectOfType<Slider>();
                Slider.value = Volume;
            }
        }
    }


    public float GetVolume()
    {
        return Volume;
    }

    public void SetVolume(float volume)
    {
        Volume = volume;
    }
}
