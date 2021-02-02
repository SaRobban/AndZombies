using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{

    public GameObject zombieClone;
    // Start is called before the first frame update
    private void Start()
    {
        SpawnZombie();
    }

    public void SpawnZombie()
    {
        GameObject clone = Instantiate(zombieClone, transform.position, transform.rotation);
        clone.GetComponent<ZombieMovement>().spawner = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
