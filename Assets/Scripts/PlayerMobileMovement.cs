using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMobileMovement : MonoBehaviour
{
    public Rigidbody2D thePlayerRigidBody;
    public float moveSpeed;
    private bool facingRight;
    // Use this for initialization
    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        moveSpeed = 5f;
        facingRight = true;
    }

    private void FixedUpdate()
    {
#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.UpArrow))
        {

            thePlayerRigidBody.velocity = new Vector3(thePlayerRigidBody.velocity.x, moveSpeed, 0f);
        }
#endif
        //creating neutral zone for character movements
        if (Input.acceleration.x > .025f)
        {
            thePlayerRigidBody.velocity = new Vector3(10f * Input.acceleration.x, thePlayerRigidBody.velocity.y, 0f);
        }
        //thePlayerRigidBody.AddRelativeForce(Vector3.right * 4f);
        else if (Input.acceleration.x < -.025f)
            thePlayerRigidBody.velocity = new Vector3(10f * Input.acceleration.x, thePlayerRigidBody.velocity.y, 0f);
        // thePlayerRigidBody.AddRelativeForce(Vector3.left * 4f);
    }
    // Update is called once per frame
    void Update()
    {
        //creating neutral zone for flip
        if (thePlayerRigidBody.velocity.x < -.025f && facingRight)
        {
            facingRight = false;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 1f);
        }
        else if (thePlayerRigidBody.velocity.x > .025f && !facingRight)
        {
            facingRight = true;
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, 1f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("RightCamera"))
        {
            thePlayerRigidBody.position = new Vector3(-2.80f, thePlayerRigidBody.position.y, 0f);
        }
        else if (collision.gameObject.tag.Equals("LeftCamera"))
        {
            thePlayerRigidBody.position = new Vector3(2.80f, thePlayerRigidBody.position.y, 0f);
        }
    }
}

