using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMovement : MonoBehaviour
{
    
    //neccessary for animations
    private Rigidbody2D rb2D;
    private Animator myAnimator;

    //variables
    public float speed = 2.0f;
    public float horizontalMovement; //either 1, -1, or 0
    private bool facingRight = true;
    
    void Start()
    {
        //define the gameobjects on the player
        rb2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();

    }

    //handles the input for the physics
    void Update()
    {
        //check direction
        horizontalMovement = Input.GetAxis("Horizontal");


    }

    //handles running the physics
    private void FixedUpdate()
    {
        //move the character left and right
        rb2D.velocity = new Vector2 (horizontalMovement*speed, 0);
        Flip(horizontalMovement);
        myAnimator.SetFloat("speed", Mathf.Abs(horizontalMovement));

    }

    private void Flip(float horizontalMovement)
    {
        if (horizontalMovement < 0 && facingRight || horizontalMovement > 0 && !facingRight)
        {
            facingRight = !facingRight;

            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;

        }
    }

}
