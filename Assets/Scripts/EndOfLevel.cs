using UnityEngine;

public class EndOfLevel : MonoBehaviour
{
    GameManager gameManager;

    Player player;
    float playerX;
    float eolX;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        player = FindObjectOfType<Player>();
        playerX = player.transform.position.x;
        eolX = GetComponent<Transform>().position.x;
    }

    private void FixedUpdate()
    {
        playerX = player.transform.position.x;

        if (playerX > eolX)
        {
            gameManager.LoadNextLevel();
        }
    }

    Vector3 eolPosition;
    Vector3 from;
    Vector3 to;

    private void OnDrawGizmos()
    {
        eolPosition = GetComponent<Transform>().position;
        from = new Vector3(eolPosition.x, eolPosition.y - 25, eolPosition.z);
        to = new Vector3(eolPosition.x, eolPosition.y + 25, eolPosition.z);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(from, to);
    }
}
