using System.Collections;
using UnityEngine;

public class Rescue : MonoBehaviour
{
    GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        StartCoroutine(RescueHero());
    }

    private IEnumerator RescueHero()
    {
        GetComponent<Animator>().SetTrigger("Rescue");
        gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, -3, 0);
        gameObject.transform.localScale = new Vector3(-1, 1, 1);
        yield return new WaitForSeconds(2);
        StartCoroutine(gameManager.LoadNextScene());
    }
}
