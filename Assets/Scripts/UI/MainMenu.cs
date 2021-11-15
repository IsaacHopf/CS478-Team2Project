using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private static GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void PlayGame()
    {
        StartCoroutine(gameManager.LoadNextScene());
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
