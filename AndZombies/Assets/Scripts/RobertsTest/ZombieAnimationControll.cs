using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//[RequireComponent(typeof(ZombieMovement))]

public class ZombieAnimationControll : MonoBehaviour
{
    private ZombieMovement zm;

    private ZombieMovement.zombieStates oldState = ZombieMovement.zombieStates.Start;

    bool animIdle;
    bool animWalk;
    bool animJump;
    bool animFall;


    // Start is called before the first frame update
    void Start()
    {
        zm = GetComponentInParent<ZombieMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        SetAnimState();
    }
    public void SetAnimState() { 
        switch (zm.zombieState)
        {
            case ZombieMovement.zombieStates.Start:
                //PUT CODE FOR ANIMATIONHERE
                animIdle = true;
                animWalk = false;
                animJump = false;
                animFall = false;
                setAnimation();
                break;

            case ZombieMovement.zombieStates.Walk:
                //PUT CODE FOR ANIMATIONHERE
                animIdle = false;
                animWalk = true;
                animJump = false;
                animFall = false;
                setAnimation();

              //  print("Zombie Walk");
                break;

            case ZombieMovement.zombieStates.JumpStart:
                //PUT CODE FOR ANIMATIONHERE
                animIdle = false;
                animWalk = false;
                animJump = true;
                animFall = false;
                setAnimation();
               // print("Zombie JumpStart");
                break;

            case ZombieMovement.zombieStates.Jump:
                //PUT CODE FOR ANIMATIONHERE
                animIdle = false;
                animWalk = false;
                animJump = true;
                animFall = false;
                setAnimation();
               // print("Zombie Fly");
                break;

            case ZombieMovement.zombieStates.Fall:
                //PUT CODE FOR ANIMATIONHERE
                animIdle = false;
                animWalk = false;
                animJump = false;
                animFall = true;
                setAnimation();
                //print("Zombie Fall");
                break;

            case ZombieMovement.zombieStates.Hit:
                //PUT CODE FOR ANIMATIONHERE
                animIdle = true;
                animWalk = false;
                animJump = false;
                animFall = false;
                setAnimation();
                print("AnimHit");
                break;

            default:
                print("no zombie case");
                break;
        }
    }

    void setAnimation()
    {
        if(oldState != zm.zombieState)
        {
            oldState = zm.zombieState;
            Animator anim = GetComponentInParent<Animator>();
            anim.SetBool("Idle", animIdle);
            anim.SetBool("Walk", animWalk);
            anim.SetBool("Jump", animJump);
            anim.SetBool("Fall", animFall);
        }
    }
}
