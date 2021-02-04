using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    private ZombieController zC;
    private WinThelevel wL;
    private int score = 0;

    void Awake()
    {
        //Singelton
        ScoreCounter[] scores = GameObject.FindObjectsOfType<ScoreCounter>();
        if (scores.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
    }

    // Start is called before the first frame update
    public int GetScore()
    {
        return score;
    }

    public int GetAddScore()
    {
        zC = GameObject.FindObjectOfType<ZombieController>();
        int zombieScore = zC.maxZombieCount - zC.spawnedZombies;
        score += zombieScore;
        return score;
    }
}
