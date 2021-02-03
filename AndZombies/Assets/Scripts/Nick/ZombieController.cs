using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    // this script is used for controlling zombies, 
    // managing the input, 
    // and getting a new zombie after the previous zombie got set in place

    
    [Header("Alternating Values")]
    public GameObject currentZombie;
    public float freezeTimer = 0;
    public int spawnedZombies = 0;

    [Header("Set Values")]
    public GameObject zombiePrefab;
    public Transform spawnTransform;
    public float timeBeforeFreezing;
    public int maxZombieCount;

    [Header("Internal Values")]
    private SimpleObjectPool zombiePool;
    private ZombieMovement zombieMovementScript;

    private void Start()
    {
        zombiePool = new SimpleObjectPool(zombiePrefab, maxZombieCount);
        freezeTimer = 1;
    }

    private void Update()
    {
        Timer();
    }

    private void SpawnZombie()
    {
        // get a zombie from the pool, activate it and set it at the spawn position
        // add 1 to spawned zombies count
        // set freezeTimer;
        currentZombie = zombiePool.GetObject();
        currentZombie.SetActive(true);
        currentZombie.transform.position = spawnTransform.position;

        zombieMovementScript = currentZombie.GetComponent<ZombieMovement>();

        // safety measure
        zombieMovementScript.zombieState = ZombieMovement.zombieStates.Start;
        

        spawnedZombies++; 
        freezeTimer = timeBeforeFreezing;
    }

    private void Timer()
    {
        if (freezeTimer > 0)
        {
            // run a timer backwards
            freezeTimer -= Time.deltaTime;

            if (freezeTimer < 0)
            {
                freezeTimer = 0;
            
                // when the timer reaches zero, spawn a new zombie
                if (freezeTimer == 0)
                {
                    if (spawnedZombies != maxZombieCount)
                    {
                        SpawnZombie();
                    }

                    else
                    {
                        Debug.Log("No Zombies Left to Spawn");
                        // TODO: having the game over menu show up
                    }
                }
            }
        }
    }
}
