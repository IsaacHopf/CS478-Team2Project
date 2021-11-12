using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{

    public override void Start()
    {
        base.Start();
        currentHealth = maxHealth;
    }

    //this method may be redundent
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
