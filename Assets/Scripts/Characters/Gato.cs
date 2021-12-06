using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gato : Character
{

    private Transform target; //this will be the target the enemy chases.
    public Transform grounddetection;
    public float initialDirection;
    public float attackDistance = 2;
    private bool playerWithinRange = false;
    private float distanceToPlayer;
    private float oldDirection;
    private bool hasBeenDamaged = false;
    private bool walking = true;
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
        oldDirection = direction;
        currentHealth = maxHealth;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

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

        //change the speed of Gato depending on if its walking or not
        if (walking)
        {
            speed = walkSpeed;
        }
        else
        {
            speed = runSpeed;
        }

        //If the target (player) is within chaseDistance but out of range
        if (distanceToPlayer < chaseDistance)
        {

            walking = false;
            //If the player is to the LEFT of the Enemy, move the Enemy LEFT
            if (target.position.x < transform.position.x)
            {
                myAnimator.SetTrigger("walk");
                direction = -1;

                TurnAround(direction);


            }
            //ELSE, the player is to the RIGHT of the Enemy, so move the Enemey RIGHT
            else
            {
                myAnimator.SetTrigger("walk");
                direction = 1;
                TurnAround(direction);

            }


        }


        //OTHERWISE just patrol.
        else
        {
            walking = true;
            RaycastHit2D groundinfo = Physics2D.Raycast(grounddetection.position, Vector2.right, .1f);
            direction = oldDirection;
            TurnAround(direction);

            if (groundinfo.collider == true)
            {
                if (direction == 1) //moving right
                {
                    myAnimator.SetTrigger("walk");
                    direction = -1;
                    oldDirection = direction;
                    TurnAround(direction);

                }
                else if (direction == -1) //moving left
                {
                    myAnimator.SetTrigger("walk");
                    direction = 1;
                    oldDirection = direction;
                    TurnAround(direction);
                }

            }
        }


    }

    protected override void HandleAttack()
    {


    }

    protected override void HandleJumping()
    {

    }

    protected override void Attack()
    {

    }

    protected override void Death()
    {

    }

    private void DeactivateEnemy()
    {

    }
}
