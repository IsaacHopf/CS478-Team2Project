using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float yOffset;

    Transform playerTransform;
    Vector3 startingPlayerPos;
    float solX;
    float eolX;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        startingPlayerPos = playerTransform.position;
        solX = FindObjectOfType<StartOfLevel>().transform.position.x;
        eolX = FindObjectOfType<EndOfLevel>().transform.position.x;
    }

    private void LateUpdate()
    {
        Vector3 tempPlayPos = playerTransform.position;
        Vector3 tempCamPos = transform.position;

        if (tempPlayPos.x >= solX && tempPlayPos.x <= eolX) //camera horizontally follows only when the player is between StartOfLevel and EndOfLevel
        {
            tempCamPos.x = tempPlayPos.x;
        }
        if (tempPlayPos.y >= startingPlayerPos.y - 0.5) //camera vertically follows only when the player is not below their starting Y position
        {
            tempCamPos.y = playerTransform.position.y;
            tempCamPos.y += yOffset;
        }

        transform.position = tempCamPos;
    }
}
