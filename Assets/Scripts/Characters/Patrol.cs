using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public float speed;
    public float chaseDistance = 10;
    private Transform target; //this will be the target the enemy chases.

    private bool movingright = true;

    public Transform grounddetection;

    private void Start()
    {
        //set the target as the player:
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        //check to see if the player target is out of range. If so, keep patroling.
        if (Vector2.Distance(transform.position, target.position) > chaseDistance)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);

            
        }
        //if the payer target is in range, then chase the player.
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.position.x, transform.position.y), speed * Time.deltaTime);
        }

        RaycastHit2D groundinfo = Physics2D.Raycast(grounddetection.position, Vector2.right, .1f);
        if (groundinfo.collider == true)
        {
            if (movingright == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingright = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingright = true;
            }

        }
    }
}
