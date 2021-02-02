using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsRigoMortes : MonoBehaviour
{
    public Rigidbody2D rb2d;


    public bool jump;
    public bool hasJumped;
    public float jumpForce = 5f;
    public float jumpTorque = 90f;

    public float walkspeed = 5f;

    public bool grounded;

    public RigidbodyConstraints2D onWalking;
    public RigidbodyConstraints2D onJumping;
    public RigidbodyConstraints2D onHitAfterJump;

    private void OnCollisionStay2D(Collision2D collision)
    {
        //grounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (rb2d.velocity.y < 0)
            grounded = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = Vector2.right * walkspeed;
        rb2d.constraints = onWalking;
        Time.timeScale = 0.25f;
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(rb2d.velocity.y < 0)
            grounded = GroundCheck();

        if (hasJumped)
        {
            if (grounded)
            {
                print("Grounded");
                rb2d.constraints = onHitAfterJump;
            }
        }
        else
        {
            if (jump)
            {
                grounded = false;
                rb2d.constraints = onJumping;
                rb2d.AddForce(Vector2.up * jumpForce);
                rb2d.AddTorque(jumpTorque);
                hasJumped = true;
            }
        }
    }

    bool GroundCheck()
    {
       // Collider2D[
        if (Physics2D.OverlapCapsule(transform.position, new Vector2(1.1f, 0.55f), transform.GetComponent<CapsuleCollider2D>().direction, transform.rotation.z))
        {
            print("CheckOverlap");
            return true;
        }
        return false;
    }
}
