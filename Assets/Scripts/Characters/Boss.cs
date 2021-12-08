using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class Boss : Character
{

    private static System.Timers.Timer bossPhase1Timer;
    private static System.Timers.Timer bossPhase2Timer;
    private static System.Timers.Timer bossPhase3Timer;

    private Transform target; //this will be the target the enemy chases.
    private string targetTag;
    public Transform grounddetection;
    public float initialDirection;
    public float attackDistance = 1;
    private bool playerWithinRange = false;
    private float distanceToPlayer;
    private bool hasBeenDamaged = false;
    private bool chasePlayer = false;
    private Vector3 originalPosition;
    private Vector3 targetPosition;
    public float attackDelayInterval = 200;
    private bool phase1Begin = true;
    private bool phase2Begin = true;
    [SerializeField] protected float chaseDistance = 15;

    [Header("Timer Intervals")]
    [SerializeField] private float bossPhase1TimerInterval = 5000;
    [SerializeField] private float bossPhase2TimerInterval = 5000;
    [SerializeField] private float bossPhase3TimerInterval = 5000;

    [Header("Boss Phase Booleans")]
    [SerializeField] private bool bossPhase1 = false;
    [SerializeField] private bool bossPhase2 = false;
    [SerializeField] private bool bossPhase3 = false;

    [Header("Phase variables")]
    [SerializeField] private float bossPhase1Speed = 8;
    [SerializeField] private float bossPhase2AttackSpeed = 12;
    [SerializeField] private float bossPhase2ReturnSpeed = 4;
    [SerializeField] private float bossPhase3Speed = 8;

    [Header("Activate Exit Objects")]
    [SerializeField] private GameObject exitPlatforms;
    [SerializeField] private GameObject exitDecorations;
    [SerializeField] private GameObject endOfLevel;

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
        target = GameObject.FindGameObjectWithTag("BossBoundLeft").GetComponent<Transform>();
        targetTag = "BossBoundLeft";
        originalPosition = transform.position;
        bossPhase1 = true;

        //initialize timers
        bossPhase1Timer = new System.Timers.Timer(bossPhase1TimerInterval);
        bossPhase2Timer = new System.Timers.Timer(bossPhase2TimerInterval);
        bossPhase3Timer = new System.Timers.Timer(bossPhase3TimerInterval);
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


        //PHASE 1
        if (bossPhase1)
        {
            bossPhase1Timer.Start();
            bossPhase1Timer.Elapsed += new System.Timers.ElapsedEventHandler(EndPhase1);

            if (phase1Begin)
            {
                target = GameObject.FindGameObjectWithTag("BossBoundLeft").GetComponent<Transform>();
                targetTag = "BossBoundLeft";
                phase1Begin = false;
            }

            speed = bossPhase1Speed;
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

            //if it hits the left bound, go back right
            if (transform.position == target.position && targetTag == "BossBoundLeft")
            {
                target = GameObject.FindGameObjectWithTag("BossBoundRight").GetComponent<Transform>();
                targetTag = "BossBoundRight";
                direction = 1;
                TurnAround(direction);
            }

            //if it hits the right bound, go back left
            if (transform.position == target.position && targetTag == "BossBoundRight")
            {
                target = GameObject.FindGameObjectWithTag("BossBoundLeft").GetComponent<Transform>();
                targetTag = "BossBoundLeft";
                direction = -1;
                TurnAround(direction);
            }
        }

        //PHASE 2
        else if (bossPhase2)
        {

            bossPhase2Timer.Start();
            bossPhase2Timer.Elapsed += new System.Timers.ElapsedEventHandler(EndPhase2);

            if (phase2Begin)

            {
                target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
                targetTag = "Player";
                targetPosition = target.position;
                originalPosition = transform.position;
                phase2Begin = false;
                chasePlayer = true;
            }

            if (chasePlayer)
            {
                speed = bossPhase2AttackSpeed;
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            }
            else
            {
                speed = bossPhase2ReturnSpeed;
                transform.position = Vector3.MoveTowards(transform.position, originalPosition, speed * Time.deltaTime);
            }

            if (transform.position == targetPosition && targetTag == "Player")
            {
                chasePlayer = false;
                targetTag = "Origin";
                Attack();
            }
            
            if (transform.position == originalPosition && targetTag == "Origin")
            {
                chasePlayer = true;
                phase2Begin = true;
                hasBeenDamaged = false;
            }


        }
        //PHASE 3

        /*Vector3 ghostDirection = target.position - transform.position;
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
        ActivateExit();
    }

    private void ActivateExit()
{
    exitPlatforms.SetActive(true);
    exitDecorations.SetActive(true);
    endOfLevel.transform.position = new Vector3(11.98f, endOfLevel.transform.position.y, endOfLevel.transform.position.z);
}

    private void EndPhase1(object sender, ElapsedEventArgs elapsedEventArg)
    {
        Debug.Log("PHASE 1 end");
        bossPhase1Timer.Stop();
        bossPhase1 = false;
        bossPhase2 = true;
        phase2Begin = true;
        hasBeenDamaged = false;

    }

    private void EndPhase2(object sender, ElapsedEventArgs elapsedEventArg)
    {
        bossPhase2Timer.Stop();
        bossPhase1 = true;
        bossPhase2 = false;
        phase1Begin = true;

        Debug.Log("PHASE 2 end");

    }
}


