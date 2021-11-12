using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : Character
{
   


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
