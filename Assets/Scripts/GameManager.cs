/*
 * GameManger is responsible for changing states in our game
 * like starting and stopping the game, restarting the game, displaying UI, switching scenes, etc.
 */

using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Animator fadeAnimator;

    public void RestartLevel()
    {
        fadeAnimator.SetTrigger("FadeOut");
        Invoke("RestartScene", 1.2f); //restart the scene after fade out (1.2 secs)
    }
    private void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadNextLevel()
    {
        fadeAnimator.SetTrigger("FadeOut");
        Invoke("LoadNextScene", 1.2f); //load the next scene after fade out (1.2 secs)
    }
    private void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
