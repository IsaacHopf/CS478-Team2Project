using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private float runSpeed = 12.0f;
    private float walkSpeed = 1.0f;

    [SerializeField] private HealthBar healthBar;

    public override void Start()
    {
        base.Start();
        speed = runSpeed;

        healthBar.SetMaxHealth(maxHealth);
    }
    public override void Update()
    {
        base.Update();
        direction = Input.GetAxisRaw("Horizontal");
        HandleJumping();
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

    protected override void Death()
    {
        base.Death();
        FindObjectOfType<GameManager>().EndGame(); //finds GameManager instead of declaring an object bc an object cannot be linked to a prefab
    }
}
