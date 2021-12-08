using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossOld : Character
{
    private Transform target; //this will be the target the enemy chases.
    public Transform grounddetection;
    public float initialDirection;
    public float attackDistance = 2;
    private bool playerWithinRange = false;
    private float distanceToPlayer;
    private float oldDirection;
    private bool hasBeenDamaged = false;
    [SerializeField] protected float chaseDistance = 7;

    [Header("Activate Exit Objects")]
    [SerializeField] private GameObject exitPlatforms;
    [SerializeField] private GameObject exitDecorations;
    [SerializeField] private GameObject endOfLevel;

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
            distanceToPlayer = Vector2.Distance(transform.position, target.position);
            HandleAttack();
        }

    }

    protected override void HandleMovement()
    {
        base.HandleMovement();
        

        //If the target (player) is within chaseDistance but out of range
        if (distanceToPlayer < chaseDistance && !playerWithinRange)
        {
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

        //if the player is within range to attack
        else if (playerWithinRange)
        {

            direction = 0;
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
            myAnimator.SetTrigger("attack");
            myAnimator.ResetTrigger("walk");

        }
        else
        {
            myAnimator.ResetTrigger("attack");

        }
    }
    protected override void HandleJumping()
    {

    }
    protected override void Attack() {

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
        Invoke("DeactivateEnemy", 5); //deactivates the enemy after death (10 secs)

        ActivateExit();
    }
    private void DeactivateEnemy()
    {
        gameObject.SetActive(false);
    }
    private void ActivateExit()
    {
        exitPlatforms.SetActive(true);
        exitDecorations.SetActive(true);
        endOfLevel.transform.position = new Vector3(11.98f, endOfLevel.transform.position.y, endOfLevel.transform.position.z);
    }
}
