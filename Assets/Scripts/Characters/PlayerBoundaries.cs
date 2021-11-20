/*
 * Player Boundaries ensures that the player stays on the level
 */

using UnityEngine;

public class PlayerBoundaries : MonoBehaviour
{
    Camera mainCamera;
    Vector2 screenBounds;
    float solX;
    float eolX;

    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        solX = FindObjectOfType<StartOfLevel>().transform.position.x;
        eolX = FindObjectOfType<EndOfLevel>().transform.position.x;
    }

    void LateUpdate()
    {
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, solX - screenBounds.x + 1, eolX + screenBounds.x - 1);
        transform.position = viewPos;
    }
}