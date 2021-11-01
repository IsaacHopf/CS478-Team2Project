using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]

public abstract class Character : MonoBehaviour
{

    [Header("Movement Variables")]

    [SerializeField] protected float speed = 1.0f;
    protected float direction;

    protected bool facingRight = true;

    [Header("Jump Variables")]
    [SerializeField] protected float jumpForce;
    [SerializeField] protected float jumpTime;
    [SerializeField] protected Transform groundcheck;
    [SerializeField] protected float radOCircle;
    [SerializeField] protected LayerMask whatIsGround;
    [SerializeField] protected bool grounded;
    protected float jumpTimeCounter;
    protected bool stoppedJumping;

    //[Header("Attack Variables")]

    [Header("Character Stats")]
    [SerializeField] protected int maxHealth;
    protected int currentHealth;

    protected Rigidbody2D rb;
    protected Animator myAnimator;

    public virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        jumpTimeCounter = jumpTime;

        currentHealth = maxHealth;
    }

    public virtual void Update()
    {
        //grounded if:
        grounded = Physics2D.OverlapCircle(groundcheck.position, radOCircle, whatIsGround);

        //check vertical velocity
        if (rb.velocity.y < 0)
        {
            myAnimator.SetBool("falling", true);

        }

        //note that hp should be checked only when hp is changed (i.e. combat), but this is here for testing
        if (currentHealth <= 0)
        {
            Death();
        }
    }

    public virtual void FixedUpdate()
    {
        //handles mechanices and physics
        HandleMovement();
        HandleLayers();
    }
    //actually moves the chacter
    protected void Move()
    {
        rb.velocity = new Vector2(direction * speed, rb.velocity.y);
    }

    //actually makes the character jump
    protected void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    protected abstract void HandleJumping();

    //handles everything with movement
    protected virtual void HandleMovement()
    {
        Move();
    }

    //turns the character around
    protected void TurnAround(float horizontal)
    {
        if (horizontal < 0 && facingRight || horizontal > 0 && !facingRight)
        {
            facingRight = !facingRight;
            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);

        }
    }

    //handles the animation layers
    protected void HandleLayers()
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
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(groundcheck.position, radOCircle);
    }

    protected void Death()
    {
        //Death is called when hp <= 0
        //note that hp should be checked when engaging in combat (it is unecessary to check in Update())

        //activate death animation
        //myAnimator.SetBool("isDead", true);

        //reset hp
        //currentHealth = maxHealth;

        //send player to last checkpoint


        //deactivate death animation
        //myAnimator.SetBool("isDead", false);
    }
}
