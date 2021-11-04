/*
 * GameManger is responsible for changing states in our game
 * like starting and stopping the game, restarting the game, displaying UI, switching scenes, etc.
 */

using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Animator fadeAnimator;

    public void Restart()
    {
        fadeAnimator.SetTrigger("FadeOut");
        Invoke("RestartScene", 1.2f); //restart the scene after fade out (1.2 secs)
    }
    private void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
