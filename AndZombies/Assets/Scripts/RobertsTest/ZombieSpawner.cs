using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public int maxZombies = 20;
    public GameObject zombieClone;
    // Start is called before the first frame update
    private void Start()
    {
        SpawnZombie();
    }

    public void SpawnZombie()
    {
        if (maxZombies > 0)
        {
            GameObject clone = Instantiate(zombieClone, transform.position, transform.rotation);
            clone.GetComponent<ZombieMovement>().spawner = this;
        }
        else
        {
            print("GameOver");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
