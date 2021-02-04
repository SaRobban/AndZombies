using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//Simple script. prints input to TextmeshPro UI text
public class PrintToIngameUI : MonoBehaviour
{
    public TextMeshProUGUI playerInfo;
    public TextMeshProUGUI playerScore;

    private void Start()
    {
    }
    // Start is called before the first frame update
    public void PrintToInfo(string text)
    {
        playerInfo.text = text;
    }

    public void PrintToScore(string text)
    {
        playerScore.text = text;
    }
}
