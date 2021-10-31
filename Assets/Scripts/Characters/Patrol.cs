using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public float speed;

    private bool movingright = true;

    public Transform grounddetection;

    private void Update()
    {
        transform.Translate(Vector2.right*speed*Time.deltaTime);

        RaycastHit2D groundinfo = Physics2D.Raycast(grounddetection.position,Vector2.right, .1f);
        if(groundinfo.collider == true)
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
