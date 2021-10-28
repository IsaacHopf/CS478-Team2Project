using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]

public class PlayerJump : MonoBehaviour
{

    [Header("Jump Details")]
    public float jumpForce;
    public float jumpTime;
    private float jumpTimeCounter;
    private bool stoppedJumping;


    [Header("Ground Details")]
    [SerializeField] private Transform groundcheck;
    [SerializeField] private float radOCircle;
    [SerializeField] private LayerMask whatIsGround;
    public bool grounded;

    [Header("Components")]
    private Rigidbody2D rb;
    private Animator myAnimator;



    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent< Animator >();
        jumpTimeCounter = jumpTime;

    }
    

    private void Update()
    {
        //grounded if:
        grounded = Physics2D.OverlapCircle(groundcheck.position,radOCircle,whatIsGround);

        //if we are grounded, reset jump time counter
        if (grounded)
        {
            jumpTimeCounter = jumpTime;
            myAnimator.ResetTrigger("jump");
            myAnimator.SetBool("falling", false);
        }



        //if we press the jump button
        if (Input.GetButtonDown("Jump") && grounded)
        {
            //jump:
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            stoppedJumping = false;
            myAnimator.SetTrigger("jump");
        }

        //if we hold jump button
        if (Input.GetButton("Jump") && !stoppedJumping && jumpTimeCounter > 0)
        {

            //jump
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpTimeCounter -= Time.deltaTime;
            myAnimator.SetTrigger("jump");
        }

        //if we release the jump button
        if (Input.GetButtonUp("Jump"))
        {
            jumpTimeCounter = 0;
            stoppedJumping = true;
            myAnimator.SetBool("falling", true);
            myAnimator.ResetTrigger("jump");
        }

        //if y velocity is below 0, you are falling.
        if (rb.velocity.y < 0)
        {
            myAnimator.SetBool("falling", true);

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(groundcheck.position, radOCircle);
    }

    private void FixedUpdate()
    {
        HandleLayers();
    }

    private void HandleLayers()
    {
        if (!grounded)
        {
            myAnimator.SetLayerWeight(1, 1);
        }
        else
        {

            myAnimator.SetLayerWeight(1, 0);
        }
    }
}
