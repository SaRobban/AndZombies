using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(ZombieMovement))]

public class ZombieAnimationControll : MonoBehaviour
{
    private ZombieMovement zm;
    // Start is called before the first frame update
    void Start()
    {
        zm = GetComponent<ZombieMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (zm.zombieState)
        {
            case ZombieMovement.zombieStates.Start:
                //PUT CODE FOR ANIMATIONHERE
                break;
            case ZombieMovement.zombieStates.Walk:
                //PUT CODE FOR ANIMATIONHERE
                print("Zombie Walk");
                break;

            case ZombieMovement.zombieStates.JumpStart:
                //PUT CODE FOR ANIMATIONHERE
                print("Zombie JumpStart");
                break;

            case ZombieMovement.zombieStates.Jump:
                //PUT CODE FOR ANIMATIONHERE
                print("Zombie Fly");
                break;

            case ZombieMovement.zombieStates.Fall:
                //PUT CODE FOR ANIMATIONHERE
                print("Zombie Fall");
                break;

            case ZombieMovement.zombieStates.Hit:
                //PUT CODE FOR ANIMATIONHERE
                break;

            default:
                print("no zombie case");
                break;
        }
    }
}
