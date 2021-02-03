using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMovement : MonoBehaviour
{
    [Header("Stuff")]
    public Rigidbody2D rb2d;
    public ZombieSpawner spawner;
    public enum zombieStates { Start, Walk, JumpStart, Jump, Fall, Hit };
    public zombieStates zombieState = zombieStates.Start;
    private bool grounded;
    private bool jump;

    public bool xVelocityWhileHoldSpace;

    [Header("Tweeks")]
    public float initJumpForce = 5f;
    public float constantJumpForce = 5f;
    public float jumpLoss = 5f;
    public float jumpTorque = 90f;
    public float walkspeed = 5f;
    public bool aircontroll = true;
    private float mouseOrgin;

    public float airRotationSpeedKey = 3;
    public float airRotationSpeedMouse = 3;


    public RigidbodyConstraints2D onWalking;
    public RigidbodyConstraints2D onJumping;
    public RigidbodyConstraints2D onHitAfterJump;

    float mouseInputToAxis()
    {
        float mouseX = Input.mousePosition.x - mouseOrgin;
       // mouseOrgin = mouseX;
        return mouseX;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {

        if (zombieState == zombieStates.Fall || zombieState == zombieStates.Jump && !jump)
        {
            print("Zombie hit");
            if (collision.gameObject.GetComponent<Rigidbody2D>())
            {
                AddJoint(collision.gameObject.GetComponent<Rigidbody2D>());
            }
            else
            {
                rb2d.constraints = onHitAfterJump;
            }
        }
        if (rb2d.velocity.y < 0)
        {
            grounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (rb2d.velocity.y > 0)
            grounded = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = Vector2.right * walkspeed;
        rb2d.constraints = onWalking;
        // Time.timeScale = 0.25f;
    }

    void Update()
    {
        print(Input.mousePosition.x);
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
        if (Input.GetButtonUp("Jump"))
        {
            jump = false;
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (zombieState)
        {
            case zombieStates.Start:
                StartZombie();
                print("Zombie Start");
                break;
            case zombieStates.Walk:
                WalkZombie();
                print("Zombie Walk");
                break;

            case zombieStates.JumpStart:
                JumpZombie();
                print("Zombie JumpStart");
                break;

            case zombieStates.Jump:
                FlyZombie();
                print("Zombie Fly");
                break;

            case zombieStates.Fall:
                FallZombie();
                print("Zombie Fall");
                break;

            case zombieStates.Hit:
                //AddJoint();
                print("ControllerColliderHit");
                break;

            default:
                print("no zombie case");
                break;
        }
    }


    void StartZombie()
    {
        mouseOrgin = Input.mousePosition.x;
        zombieState = zombieStates.Walk;
    }

    void WalkZombie()
    {
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
        // if (rb2d.velocity.y < 0 || !jump)
        else
        {
            zombieState = zombieStates.Fall;
        }

        AirRotationControll();
    }
    void AirRotationControll()
    {
        if (aircontroll)
        {
            float inputKeyMouse = mouseInputToAxis() * airRotationSpeedMouse + -Input.GetAxis("Horizontal") * airRotationSpeedKey;
            transform.rotation *= Quaternion.Euler(0, 0, inputKeyMouse * Time.fixedDeltaTime);
        }
        else
        {
            transform.up = rb2d.velocity;
        }
    }
    void FallZombie()
    {
        //Need to check if joint first maybe!?
        AirRotationControll();
        return;
    }

    void AddJoint(Rigidbody2D otherBody)
    {
        zombieState = zombieStates.Hit;
        if (otherBody != null)
        {
            print("Zombie Joint");
            FixedJoint2D joint = gameObject.AddComponent<FixedJoint2D>();
            joint.connectedBody = otherBody;
            spawner.SpawnZombie();
            Destroy(gameObject.GetComponent<ZombieMovement>());
        }
    }

    /*
        bool GroundCheck()
        {
            //Hit self
            // Collider2D[] colliderHit = Physics2D.OverlapCapsule(transform.position, new Vector2(1.1f, 0.55f), transform.GetComponent<CapsuleCollider2D>().direction, transform.rotation.z);
            if (Physics2D.OverlapCapsule(transform.position, new Vector2(1.1f, 0.55f), transform.GetComponent<CapsuleCollider2D>().direction, transform.rotation.z))
            {
                print("CheckOverlap");
                return true;
            }
            return false;
        }
    */
}
