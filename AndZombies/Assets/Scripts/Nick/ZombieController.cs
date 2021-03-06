using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ZombieController : MonoBehaviour
{
    public static ZombieController Instance { get { return instance; } }
    private static ZombieController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }
    }

    // this script is used for controlling zombies, 
    // managing the input, 
    // and getting a new zombie after the previous zombie got set in place


    [Header("Alternating Values")]
    public GameObject currentZombie; // the current zombie to control
    public float freezeTimer = 0; // the timer before the next zombie spawns
    public int spawnedZombies = 0; // amount of spawned zombies

    [Header("Set Values")]
    public GameObject zombiePrefab;
    public Transform spawnTransform;
    public float timeBeforeFreezing;
    public int maxZombieCount;

    [Header("Internal Values")]
    private SimpleObjectPool zombiePool;
    private ZombieMovement zombieMovementScript;
    private SoundPlayer jumpSoundPlayer;
    private SoundPlayer groanSoundPlayer;

    private void Start()
    {
        jumpSoundPlayer = GetComponentsInChildren<SoundPlayer>()[0];
        groanSoundPlayer = GetComponentsInChildren<SoundPlayer>()[1];
        zombiePool = new SimpleObjectPool(zombiePrefab, maxZombieCount);
        freezeTimer = 1;
    }

    private void Update()
    {
        Timer();

        if (zombieMovementScript != null)
        {
            zombieMovementScript.InputControls(inputAxis, jump);
        }
    }

    void PrintZombieCount()
    {
        GameObject.FindObjectOfType<PrintToIngameUI>().PrintToInfo("ZombieCount : " + spawnedZombies + " / " + maxZombieCount);
    }

    public void SpawnZombie()
    {
        if (spawnedZombies != maxZombieCount)
        {
            // get a zombie from the pool, activate it and set it at the spawn position
            // add 1 to spawned zombies count
            // set freezeTimer;
            currentZombie = zombiePool.GetObject();
            currentZombie.transform.position = spawnTransform.position;
            currentZombie.SetActive(true);
            zombieMovementScript = currentZombie.GetComponent<ZombieMovement>();

            // safety measure
            zombieMovementScript.zombieState = ZombieMovement.zombieStates.Start;
            groanSoundPlayer.PlaySound();
            spawnedZombies++;

            freezeTimer = timeBeforeFreezing;
        }

        else

        {
            currentZombie = null;
            Debug.Log("No Zombies Left to Spawn");
            GameObject.FindObjectOfType<PrintToIngameUI>().PrintToInfo("GAME OVER!");
            // TODO: having the game over menu show up
        }

        PrintZombieCount();
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
                    SpawnZombie();
                }
            }
        }
    }

    private bool jump;

    public void JumpAction(InputAction.CallbackContext context)
    {
        if (context.performed) // jump button is pressed/held
        {
            jump = true;
            if (!groanSoundPlayer.GetComponent<AudioSource>().isPlaying && currentZombie != null)
            {
                jumpSoundPlayer.PlaySound();
            }
        }

        else if (context.canceled) // jump button is released
        {
            jump = false;
        }
    }

    private float inputAxis;

    public void RotateZombieAction(InputAction.CallbackContext context)
    {
        // when A or D is pressed it returns a float ranging from -1 to 1,
        // which is passed into the inputAxis variable
        inputAxis = context.ReadValue<float>();
    }

    public void ReturnToMainMenu(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GameObject musicPlayer = GameObject.FindGameObjectWithTag("MusicPlayer");

            Destroy(musicPlayer);
            SceneManager.LoadScene(0);
        }
    }
}
