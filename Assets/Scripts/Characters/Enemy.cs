using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : Character
{

    private Transform target; //this will be the target the enemy chases.
    public Transform grounddetection;
    public float initialDirection;
    private float oldDirection;
    [SerializeField] protected float chaseDistance = 7;

    public override void Start()
    {
        base.Start();
        if (initialDirection == -1)
        {
            facingRight = false;
        }


        direction = initialDirection;
        oldDirection = direction;
        currentHealth = maxHealth;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    public override void Update()
    {
        if (!isDead)
        {
            base.Update();
        }

    }

    //this method may be redundent, but it makes the enemy take damage.
    public void takeDamage(int damage)
    {
        Debug.Log("take damage");
        currentHealth -= damage;

        if (currentHealth <= 0) {
            Debug.Log("WE DEAD");
            Death();
        }


    }

    protected override void HandleMovement()
    {
        base.HandleMovement();
        

        //If the target (player) is within chaseDistance, chase the player.
        if (Vector2.Distance(transform.position, target.position) < chaseDistance)
        {
            //If the position of the target (player) is to the LEFT of the Enemy, move the Enemy LEFT
            if (target.position.x < transform.position.x)
            {
                direction = -1;

                TurnAround(direction);


            }
            //ELSE, the position of the target (player) is to the RIGHT, so move the Enemey RIGHT
            else
            {
                direction = 1;

                TurnAround(direction);

            }
        }

        //OTHERWISE just patrol.
        else
        {
            
            RaycastHit2D groundinfo = Physics2D.Raycast(grounddetection.position, Vector2.right, .1f);
            direction = oldDirection;
            TurnAround(direction);
            
            if (groundinfo.collider == true)
            {
                if (direction == 1) //moving right
                {
                    direction = -1;
                    oldDirection = direction;
                    TurnAround(direction);
                }
                else if (direction == -1) //moving left
                {              
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

    protected override void Death()
    {
        isDead = true;
        myAnimator.SetTrigger("death");
        Invoke("DeactivateEnemy", 5); //deactivates the enemy after death (10 secs)
    }

    private void DeactivateEnemy()
    {
        gameObject.SetActive(false);
    }
}
