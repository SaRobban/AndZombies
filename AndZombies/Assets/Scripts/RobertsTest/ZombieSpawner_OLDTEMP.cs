using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ZombieSpawner_OLDTEMP : MonoBehaviour
{
    public int maxZombies = 10;
    public int zombieNumber;
    public GameObject zombieClone;
    public bool stopSpawning;

    // Start is called before the first frame update
    private void Start()
    {
        //Camera.main.GetComponent<Resorces>().zSpawner = this;
        SpawnZombie();
    }

    public void SpawnZombie()
    {
        if (!stopSpawning)
        {
            if (zombieNumber < maxZombies + 1)
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
    }
   void UpdateUI() {
        Camera.main.GetComponent<PrintToIngameUI>().PrintToDefault("Zombie : " + zombieNumber + " / " + maxZombies);
    }
}
