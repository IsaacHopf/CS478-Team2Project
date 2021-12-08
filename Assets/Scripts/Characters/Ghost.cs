using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class Ghost : Character
{
    private static System.Timers.Timer attackDelay;

    private Transform target; //this will be the target the enemy chases.
    public Transform grounddetection;
    public float initialDirection;
    public float attackDistance = 1;
    private bool playerWithinRange = false;
    private float distanceToPlayer;
    private bool hasBeenDamaged = false;
    private bool chasePlayer = false;
    private Vector2 originalPosition;
    public float attackDelayInterval = 200;
    [SerializeField] protected float chaseDistance = 10;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        if (initialDirection == -1)
        {
            facingRight = false;
        }

        direction = initialDirection;
        currentHealth = maxHealth;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        originalPosition = transform.position;

        attackDelay = new System.Timers.Timer(attackDelayInterval);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            base.Update();
            distanceToPlayer = Vector3.Distance(transform.position, target.position);

            HandleAttack();
        }
    }

    protected override void HandleMovement()
    {

        Vector3 ghostDirection = target.position - transform.position;
        if (distanceToPlayer < chaseDistance)
        {
            chasePlayer = true;
        }
        else
        {
            chasePlayer = false;
        }

        if (chasePlayer)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, originalPosition, speed * Time.deltaTime);
        }

        if (distanceToPlayer < chaseDistance && !playerWithinRange)
        {
            //If the player is to the LEFT of the Enemy, move the Enemy LEFT
            if (target.position.x < transform.position.x)
            {

                direction = -1;
                TurnAround(direction);


            }
            //ELSE, the player is to the RIGHT of the Enemy, so move the Enemey RIGHT
            else
            {
                direction = 1;
                TurnAround(direction);

            }
        }
    }

    protected override void HandleAttack()

    {

        //if the player is within range, set playerWithinRange to true
        if (attackDistance >= distanceToPlayer)
        {
            playerWithinRange = true;

        }
        else
        {
            playerWithinRange = false;
        }

        //attack if player is within range
        if (playerWithinRange)
        {
            Attack();

        }

    }
    protected override void HandleJumping()
    {

    }
    protected override void Attack()
    {

        attackDelay.Start();
        attackDelay.Elapsed += new System.Timers.ElapsedEventHandler(AttackDelayOver);

        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D player in hitPlayers)
        {
            if (!hasBeenDamaged)
            {

                player.GetComponent<Player>().AdjustCurrentHealth(damage * -1);
                hasBeenDamaged = true;
            }
        }


    }

    protected override void Death()
    {
        isDead = true;
        direction = 0;
        myAnimator.SetTrigger("death");
        Invoke("DeactivateEnemy", 1); //deactivates the enemy after death (10 secs)
    }

    private void DeactivateEnemy()
    {
        gameObject.SetActive(false);
    }

    private void AttackDelayOver(object sender, ElapsedEventArgs elapsedEventArg)
    {
        hasBeenDamaged = false;
        attackDelay.Stop();
    }
}
