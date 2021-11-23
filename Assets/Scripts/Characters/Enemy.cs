using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : Character
{

    private Transform target; //this will be the target the enemy chases.

    public override void Start()
    {
        base.Start();
        currentHealth = maxHealth;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    public override void Update()
    {
        base.Update();

        if (target.position.x < transform.position.x)
        {
            direction = 1;
        }
        else
        {
            direction = -1;
        }

        TurnAround(direction);
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
    protected override void HandleAttack()

    {
    }
    protected override void HandleJumping()
    {
        
    }

    protected override void Death()
    {
        myAnimator.SetTrigger("death");
        Invoke("DeactivateEnemy", 10); //deactivates the enemy after death (10 secs)
    }

    private void DeactivateEnemy()
    {
        gameObject.SetActive(false);
    }
}
