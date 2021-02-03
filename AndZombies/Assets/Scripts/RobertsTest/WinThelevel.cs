using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinThelevel : MonoBehaviour
{
    private void Start()
    {
        Camera.main.GetComponent<Resorces>().winTheLevel = this;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Camera.main.GetComponent<PrintToIngameUI>().PrintToDefault("You get to eat");
        print("YOU GET TO EAT \n Player has won the level");

        Resorces res = Camera.main.GetComponent<Resorces>();
        res.zSpawner.stopSpawning = true;
        res.printToUI.PrintToDefault("You get To eat!");
    }
   
}
