using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTest : MonoBehaviour
{

    ContactPoint2D[] contactpoints = new ContactPoint2D[64];
    int cLength;

    private bool isGrounded;
    public bool hitAWall;

    float groundedIfAngle = 45;
    float hitAWallAngle = 45;
    // Start is called before the first frame update
    void Start()
    {
        groundedIfAngle = Mathf.Cos(groundedIfAngle);
        hitAWallAngle = Mathf.Sin(hitAWallAngle);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (contact.normal.y > groundedIfAngle)
                isGrounded = true;

            if (contact.normal.x < -hitAWallAngle)
                hitAWall = true;

            //Debug.DrawRay(contact.point, contact.normal*50, Color.white,0.1f);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isGrounded)
            Debug.DrawRay(transform.position, Vector2.up, Color.green);
        else
            Debug.DrawRay(transform.position, Vector2.up, Color.red);

        if(hitAWall)
            Debug.DrawRay(transform.position, Vector2.right, Color.yellow);

        hitAWall = false;
        isGrounded = false;
    }
}
