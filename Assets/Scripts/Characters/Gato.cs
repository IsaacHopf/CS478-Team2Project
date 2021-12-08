using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;


public class Gato : Character
{
    private static System.Timers.Timer attackRunTimer;
    private static System.Timers.Timer crouchPhaseTimer;

    private Transform target; //this will be the target the enemy chases.
    public Transform grounddetection;
    public float initialDirection;
    public float attackDistance = 2;
    private bool playerWithinRange = false;
    private float distanceToPlayer;
    private float oldDirection;
    private bool hasBeenDamaged = false;
    private bool walking = true;
    private bool attackRun = false;
    private bool crouchPhase = false;
    public double attackRunInterval = 2000;
    public double crouchPhaseInterval = 1000;
    [SerializeField] protected float runSpeed = 12;
    [SerializeField] protected float walkSpeed = 6;
    [SerializeField] protected float chaseDistance = 7;

    // Start is called before the first frame update
    void Start()
    {
        //load the base Start() from the Character script.
        base.Start();

        //IF the initial direction is left, set facingRight as false
        if (initialDirection == -1)
        {
            facingRight = false;
        }

        //set some vars
        direction = initialDirection;
        currentHealth = maxHealth;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        distanceToPlayer = Vector2.Distance(transform.position, target.position);
        //set timers
        attackRunTimer = new System.Timers.Timer(attackRunInterval);
        crouchPhaseTimer = new System.Timers.Timer(crouchPhaseInterval);

    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            
            base.Update();
            distanceToPlayer = Vector2.Distance(transform.position, target.position);

            HandleAttack();
        }
    }

    protected override void HandleMovement()
    {
        base.HandleMovement();

        RaycastHit2D groundinfo = Physics2D.Raycast(grounddetection.position, Vector2.right, .1f);


        //change the speed of Gato depending on if its walking or not
        if (walking)
        {
            speed = walkSpeed;
        }
        else
        {
            speed = runSpeed;
        }

        //If the player is within chaseDistance, and we aren't already attacking, and we aren't in the crouch phase, begin an attack run
        if (distanceToPlayer < chaseDistance && !attackRun && !crouchPhase)
        {

            attackRun = true;
            crouchPhase = false;
            walking = false;
            hasBeenDamaged = false;

            //if player is to left of enemy
            if (target.position.x < transform.position.x)
            {
                direction = -1;
                TurnAround(direction);
            }
            //if player is to right of enemy
            else if (target.position.x > transform.position.x)
            {
                direction = 1;
                TurnAround(direction);
            }
        }

        if (attackRun)
        {
            //set the approriate triggers for running
            myAnimator.SetTrigger("run");
            myAnimator.ResetTrigger("walk");

            //TIMER
            attackRunTimer.Start();
            attackRunTimer.Elapsed += new System.Timers.ElapsedEventHandler(EndAttackRun);

            //GROUND DETECT


            TurnAround(direction);

            if (groundinfo.collider == true && groundinfo.transform.gameObject.tag != "Player")
            {

                /*//if player is to left of enemy
                if (target.position.x < transform.position.x)
                {
                    direction = -1;
                    TurnAround(direction);
                    attackRun = false;
                    crouchPhase = true;
                    attackRunTimer.Stop();
                }
                //if player is to right of enemy
                else if (target.position.x > transform.position.x)
                {
                    direction = 1;
                    TurnAround(direction);
                    attackRun = false;
                    crouchPhase = true;
                    attackRunTimer.Stop();
                }*/

                if (direction == 1) //moving right
                {

                    direction = -1;
                    TurnAround(direction);
                    attackRun = false;
                    crouchPhase = true;
                    attackRunTimer.Stop();
                }
                else if (direction == -1) //moving left
                {

                    direction = 1;
                    TurnAround(direction);
                    attackRun = false;
                    crouchPhase = true;
                    attackRunTimer.Stop();
                }

            }

        }

        if (crouchPhase)
        {
            myAnimator.SetTrigger("walk");
            myAnimator.ResetTrigger("run");
            walking = true;

            //if player is to left of enemy
            if (target.position.x < transform.position.x)
            {
                direction = -1;
                TurnAround(direction);
            }
            //if player is to right of enemy
            else if (target.position.x > transform.position.x)
            {
                direction = 1;
                TurnAround(direction);
            }

            crouchPhaseTimer.Start();
            crouchPhaseTimer.Elapsed += new System.Timers.ElapsedEventHandler(EndCrouchPhase);

        }

        //If the target (player) is within chaseDistance but out of range, and we aren't already attacking
        /*if (distanceToPlayer < chaseDistance && !attackRun)
        {
           
            walking = false;
            attackRun = true;
            //If the player is to the LEFT of the Enemy, move the Enemy LEFT
            if (target.position.x < transform.position.x)
            {
                myAnimator.SetTrigger("run");
                myAnimator.ResetTrigger("walk");
                direction = -1;

                TurnAround(direction);

                // Create a timer with a two second interval.
                aTimer = new System.Timers.Timer(2000);
                // Hook up the Elapsed event for the timer. 
                aTimer.Elapsed += new System.Timers.ElapsedEventHandler(EndAttackRun); ;
                aTimer.Enabled = true;


            }
            //ELSE, the player is to the RIGHT of the Enemy, so move the Enemey RIGHT
            else
            {
                myAnimator.SetTrigger("run");
                myAnimator.ResetTrigger("walk");
                direction = 1;
                TurnAround(direction);

                // Create a timer with a two second interval.
                aTimer = new System.Timers.Timer(2000);
                // Hook up the Elapsed event for the timer. 
                aTimer.Elapsed += new System.Timers.ElapsedEventHandler(EndAttackRun); ;
                aTimer.Enabled = true;

            }


        }

        else if(distanceToPlayer < chaseDistance)
        {
            walking = false;
            attackRun = true;
        }

        //OTHERWISE just patrol.
        else if ()
        {

            RaycastHit2D groundinfo = Physics2D.Raycast(grounddetection.position, Vector2.right, .1f);
            direction = oldDirection;
            TurnAround(direction);

            if (groundinfo.collider == true)
            {
                if (direction == 1) //moving right
                {
                    myAnimator.SetTrigger("walk");
                    myAnimator.ResetTrigger("run");
                    direction = -1;
                    oldDirection = direction;
                    TurnAround(direction);
                    walking = true;

                }
                else if (direction == -1) //moving left
                {
                    myAnimator.SetTrigger("walk");
                    myAnimator.ResetTrigger("run");
                    direction = 1;
                    oldDirection = direction;
                    TurnAround(direction);
                    walking = true;
                }

            }
        }*/


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

    private void EndAttackRun(object sender, ElapsedEventArgs elapsedEventArg)
    {
        attackRun = false;
        attackRunTimer.Stop();
        crouchPhase = true;
    }

    private void EndCrouchPhase(object sender, ElapsedEventArgs elapsedEventArg)
    {
        crouchPhase = false;
        crouchPhaseTimer.Stop();
    }
}
