/*
 * GameManger is responsible for restarting the game and switching scenes
 */

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Animator fadeAnimator;
    public float fadeTime;

    public IEnumerator RestartScene()
    {
        fadeAnimator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public IEnumerator LoadNextScene()
    {
        fadeAnimator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public IEnumerator LoadMainMenuScene()
    {
        fadeAnimator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene("MainMenu");
    }
}
