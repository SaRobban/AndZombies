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

        Instantiate(stars, transform.position, Quaternion.identity);
    }

   IEnumerator waitForRestart()
    {
        yield return new WaitForSeconds(timeToRestart);
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
