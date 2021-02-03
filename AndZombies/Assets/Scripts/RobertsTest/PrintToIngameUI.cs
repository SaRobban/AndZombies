using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PrintToIngameUI : MonoBehaviour
{
    public TextMeshProUGUI playerInfo;

    private void Start()
    {
        Camera.main.GetComponent<Resorces>().printToUI = this;
    }
    // Start is called before the first frame update
    public void PrintToDefault(string text)
    {
        playerInfo.text = text;
    }
}
