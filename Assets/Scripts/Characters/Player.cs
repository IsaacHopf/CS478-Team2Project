using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private float runSpeed = 12.0f;

    private HealthBar healthBar;

    public override void Start()
    {
        base.Start();
        speed = runSpeed;

        healthBar = FindObjectOfType<HealthBar>();
        healthBar.SetMaxHealth(maxHealth);
    }
    public override void Update()
    {
        base.Update();
        //check vertical velocity
        if (rb.velocity.y < 0)
        {
            myAnimator.SetBool("falling", true);

        }
        direction = Input.GetAxisRaw("Horizontal");
        Debug.Log(direction);
        HandleJumping();
        HandleAttack();
        HandleLayers();
    }

    protected override void HandleAttack()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            myAnimator.SetTrigger("Attack");
            Attack();
        }
    }
    protected override void HandleMovement()
    {
        base.HandleMovement();
        myAnimator.SetFloat("speed", System.Math.Abs(direction));
        TurnAround(direction);
    }

    protected override void HandleJumping()
    {
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
            Jump();
            stoppedJumping = false;
            myAnimator.SetTrigger("jump");
        }

        //if we hold jump button
        if (Input.GetButton("Jump") && !stoppedJumping && jumpTimeCounter > 0)
        {

            //jump
            Jump();
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
    }

    public override void AdjustCurrentHealth(int health)
    {
        base.AdjustCurrentHealth(health);
        healthBar.SetHealth(currentHealth);
    }

    protected override void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.transform.gameObject.tag == "Gato")
            {
                enemy.GetComponent<Gato>().AdjustCurrentHealth(damage * -1);
            }

            if (enemy.transform.gameObject.tag == "Skeleton")
            { 
                enemy.GetComponent<Enemy>().AdjustCurrentHealth(damage * -1);
            }

        }





    }

    protected override void Death()
    {
        StartCoroutine(FindObjectOfType<GameManager>().RestartScene());
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
}
