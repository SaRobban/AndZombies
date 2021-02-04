using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class ZombieMovement : MonoBehaviour
{
    [Header("Stuff")]
    public Rigidbody2D rb2d;

    public enum zombieStates { Start, Walk, JumpStart, Jump, Fall, Hit }; //<- Diffrent zombie states used
    public zombieStates zombieState = zombieStates.Start;
    private bool grounded;
    private bool jump;
    private float axis;

    [Header("GamplayOptions")]
    public bool xVelocityWhileHoldSpace;
    public bool aircontroll = true;
    
    [Header("Tweeks")]
    public float initJumpForce = 5f;
    public float constantJumpForce = 5f;
    public float jumpLoss = 5f;
    public float walkspeed = 5f;

    public float airRotationSpeedKey = 3;


    private bool spawnedNew;


    public RigidbodyConstraints2D onWalking;
    public RigidbodyConstraints2D onJumping;
    public RigidbodyConstraints2D onHitAfterJump;

    private void OnCollisionStay2D(Collision2D collision)
    {
        //IF we made are jumping or falling but not holding the jumpbutton...
        //We stick to whatever hit by adding a joint or freeze the constraints...
        //Then Spawn new zombie (if we havent already).
        if (zombieState == zombieStates.Fall || zombieState == zombieStates.Jump && !jump)
        {
            if (collision.gameObject.GetComponent<Rigidbody2D>())
            {
                AddJoint(collision.gameObject.GetComponent<Rigidbody2D>());
            }
            else
            {
                rb2d.constraints = onHitAfterJump;
            }

            if (!spawnedNew)
            {
                ZombieController.Instance.SpawnZombie();
                spawnedNew = true;
            }
        }

        if (rb2d.velocity.y < 0)
        {
            grounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //IF rb is moving up we are not grounded.
        if (rb2d.velocity.y > 0)
            grounded = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = Vector2.right * walkspeed;
        rb2d.constraints = onWalking;
    }


    //The inputs from other script (Spawner)
    public void InputControls(float a, bool j)
    {
        this.jump = j;
        this.axis = a;
    }


    // Update is called once per frame
    void FixedUpdate()
    {

        //if zombieState is state. Do...
        switch (zombieState)
        {
            case zombieStates.Start:
                StartZombie();
                //print("Zombie Start");
                break;
            case zombieStates.Walk:
                WalkZombie();
                //print("Zombie Walk");
                break;

            case zombieStates.JumpStart:
                JumpZombie();
                //print("Zombie JumpStart");
                break;

            case zombieStates.Jump:
                FlyZombie();
                //print("Zombie Fly");
                break;

            case zombieStates.Fall:
                FallZombie();
                //print("Zombie Fall");
                break;

            case zombieStates.Hit:
                //print("ControllerColliderHit");
                break;

            default:
                print("no zombie case");
                break;
        }
    }


    void StartZombie()
    {
        zombieState = zombieStates.Walk;
    }

    void WalkZombie()
    {
        Vector2 modVel = rb2d.velocity;
        modVel.x = walkspeed * axis;
        rb2d.velocity = modVel;

        if (jump)
        {
            zombieState = zombieStates.JumpStart;
        }
    }

    void JumpZombie()
    {
        grounded = false;
        rb2d.constraints = onJumping;
        rb2d.AddForce(Vector2.up * initJumpForce, ForceMode2D.Impulse);
        //rb2d.AddTorque(jumpTorque);
        zombieState = zombieStates.Jump;
    }
    void FlyZombie()
    {
        if (jump)
        {
            //Use gravityScale?
            constantJumpForce -= jumpLoss * Time.fixedDeltaTime;
            grounded = false;

            rb2d.AddForce(Vector2.up * constantJumpForce + Vector2.right, ForceMode2D.Force);
            if (xVelocityWhileHoldSpace)
            {
                Vector2 modVel = rb2d.velocity;
                modVel.x = walkspeed;
                rb2d.velocity = modVel;
            }
        }
        else
        {
            zombieState = zombieStates.Fall;
        }

        AirRotationControll();
    }

    void FallZombie()
    {
        //Need to check if joint first maybe!?
        AirRotationControll();
        return;
    }

    void AirRotationControll()
    {
        if (aircontroll)
        {
            transform.rotation *= Quaternion.Euler(0, 0, -axis * airRotationSpeedKey * Time.fixedDeltaTime);
        }
        else
        {
            transform.up = rb2d.velocity;
        }
    }

    //Add a Joint between two rigidbodys
    void AddJoint(Rigidbody2D otherBody)
    {
        zombieState = zombieStates.Hit;
        if (otherBody != null)
        {
            //print("Zombie Joint");
            FixedJoint2D joint = gameObject.AddComponent<FixedJoint2D>();
            joint.connectedBody = otherBody;
        }
    }


    public void KillZombie()
    {
        zombieState = zombieStates.Hit;
        ZombieController.Instance.SpawnZombie();
    }
}
