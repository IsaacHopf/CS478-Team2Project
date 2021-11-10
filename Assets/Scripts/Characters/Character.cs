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

    [Header("Attack Variables")]
    [SerializeField] protected Transform attackPoint;
    [SerializeField] protected float attackRange = 0.5f;
    [SerializeField] protected LayerMask enemyLayers; 

    [Header("Character Stats")]
    [SerializeField] protected int maxHealth;
    [SerializeField] protected int currentHealth; //temporarily serialized for testing

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

    protected void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach(Collider2D enemy in hitEnemies)
        {
            Debug.Log("We hit " + enemy.name);
        }
    }

    protected abstract void HandleAttack();

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

        if (attackPoint != null)
        {
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }

    }

    protected abstract void Death();
}
