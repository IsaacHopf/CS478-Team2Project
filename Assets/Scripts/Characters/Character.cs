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
    protected bool isDead = false;
    protected bool isAttacking = false;

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
    [SerializeField] protected Transform playerToAttack;

    [Header("Character Stats")]
    [SerializeField] protected int maxHealth;
    [SerializeField] protected int currentHealth; //temporarily serialized for testing
    [SerializeField] protected int damage = 50;

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
        if (!isDead)
        {
            //grounded if:
            grounded = Physics2D.OverlapCircle(groundcheck.position, radOCircle, whatIsGround);
        }


    }

    public virtual void FixedUpdate()
    {
        if (!isDead)
        {
            //handles mechanices and physics
            HandleMovement();
        }


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

    protected abstract void Attack();

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


    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(groundcheck.position, radOCircle);

        if (attackPoint != null)
        {
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }

    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public virtual void AdjustCurrentHealth(int health)
    {
        currentHealth += health;

        if (currentHealth <= 0)
        {
            Death();
        }
        else if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    protected abstract void Death();
}
