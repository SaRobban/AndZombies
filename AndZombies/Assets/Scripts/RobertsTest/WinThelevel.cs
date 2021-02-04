using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinThelevel : MonoBehaviour
{
    public float timeToRestart = 3;
    public GameObject stars;
    public ScoreCounter score;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        score = GameObject.FindObjectOfType<ScoreCounter>();
        Camera.main.GetComponent<PrintToIngameUI>().PrintToDefault("You get To eat!\n" + score.GetScore() + " : Points");
        StartCoroutine(waitForRestart());

        Instantiate(stars, transform.position, Quaternion.identity);
    }

   IEnumerator waitForRestart()
    {
        yield return new WaitForSeconds(timeToRestart);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
