using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ZombieSpawner : MonoBehaviour
{
    public int maxZombies = 10;
    public int zombieNumber;
    public GameObject zombieClone;

    public TextMeshProUGUI uiText;
    // Start is called before the first frame update
    private void Start()
    {
        SpawnZombie();
    }

    public void SpawnZombie()
    {
        if (zombieNumber<maxZombies+1)
        {
            GameObject clone = Instantiate(zombieClone, transform.position, transform.rotation);
            clone.GetComponent<ZombieMovement>().spawner = this;
            zombieNumber++;
        }
        else
        {
            print("GameOver");
        }

        UpdateUI();
    }
   void UpdateUI() {
        uiText.text = "Zombie : " + zombieNumber + " / " + maxZombies;
    }
}
