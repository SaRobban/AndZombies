using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinThelevel : MonoBehaviour
{
    public float timeToRestart = 3;
    PrintToIngameUI printerUI;
    public GameObject stars;
    public ScoreCounter score;
    public AudioSource levelCompleteSound;
    public SoundPlayer zombieBitingSound;

    public GameObject musicPlayer;

    private void Start()
    {
        printerUI = GameObject.FindObjectOfType<PrintToIngameUI>();
        score = GameObject.FindObjectOfType<ScoreCounter>();
        printerUI.PrintToScore(score.GetScore() + " : Points");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        score = GameObject.FindObjectOfType<ScoreCounter>();
        printerUI.PrintToScore("You get To eat!\n" + score.GetAddScore() + " : Points");
        StartCoroutine(waitForRestart());

        if (!levelCompleteSound.isPlaying)
        {
            levelCompleteSound.Play();
            zombieBitingSound.PlaySound();
        }

        Instantiate(stars, transform.position, Quaternion.identity);
    }

   IEnumerator waitForRestart()
    {
        yield return new WaitForSeconds(timeToRestart);
        
        if (SceneManager.GetActiveScene().buildIndex != 3)
        {
            SceneManager.LoadScene(SceneManager.GetSceneAt(SceneManager.GetActiveScene().buildIndex + 1).name);
        }

        else
        {
            musicPlayer = GameObject.FindGameObjectWithTag("MusicPlayer");

            Destroy(musicPlayer);
            SceneManager.LoadScene(SceneManager.GetSceneAt(0).name);
        }
    }
}
