using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_Follow : MonoBehaviour
{
    public Transform playerTransform;
    public float offset;
    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void LateUpdate()
    {
        // storing current camera position
        Vector3 temp = transform.position;

        temp.x = playerTransform.position.x;

        temp.y = playerTransform.position.y;
        temp.y += offset;

        transform.position = temp;
    }
}
