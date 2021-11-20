using UnityEngine;

public class StartOfLevel : MonoBehaviour
{
    Player player;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        player.transform.position = new Vector3(transform.position.x, player.transform.position.y, player.transform.position.z);
    }

    private void OnDrawGizmos()
    {
        Vector3 solPosition = GetComponent<Transform>().position;
        Vector3 from = new Vector3(solPosition.x, solPosition.y - 25, solPosition.z);
        Vector3 to = new Vector3(solPosition.x, solPosition.y + 25, solPosition.z);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(from, to);
    }
}
