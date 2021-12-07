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
        eolX = GetComponent<Transform>().position.x;

        if (playerX > eolX)
        {
            StartCoroutine(gameManager.LoadNextScene());
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 eolPosition = GetComponent<Transform>().position;
        Vector3 from = new Vector3(eolPosition.x, eolPosition.y - 25, eolPosition.z);
        Vector3 to = new Vector3(eolPosition.x, eolPosition.y + 25, eolPosition.z);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(from, to);
    }
}
