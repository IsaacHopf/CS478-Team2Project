using System.Collections;
using UnityEngine;

public class IntroAnimationData //this data is saved during a play session, and it can be accessed from any scene
{
    public static bool hasSeenIntro = false;
}

public class IntroAnimation : MonoBehaviour
{
    [SerializeField] private GameObject playButton, exitButton, background;
    [SerializeField] private Animator nameTextAnimator, theDemonSlayerTextAnimator, fadeOutIdleLadyAnimator, fadeInIdleHeroineAnimator;
    private Animator introAnimator;

    private void Start()
    {
        introAnimator = GetComponent<Animator>();

        if (IntroAnimationData.hasSeenIntro == false)
        {
            StartCoroutine(PlayIntroAnimation());
            IntroAnimationData.hasSeenIntro = true;
        }
        else if (IntroAnimationData.hasSeenIntro == true)
        {
            StartCoroutine(LoadMainMenu());
        }
    }

    private IEnumerator PlayIntroAnimation()
    {
        introAnimator.SetTrigger("PlayIntro");
        yield return new WaitForSeconds(52);
        nameTextAnimator.SetTrigger("Start");
        yield return new WaitForSeconds(2f);
        theDemonSlayerTextAnimator.SetTrigger("Start");
        yield return new WaitForSeconds(2.5f);
        playButton.SetActive(true);
        exitButton.SetActive(true);
    }

    private IEnumerator LoadMainMenu()
    {
        background.SetActive(true);
        yield return new WaitForSeconds(1f);
        nameTextAnimator.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        theDemonSlayerTextAnimator.SetTrigger("Start");
        yield return new WaitForSeconds(1.5f);
        playButton.SetActive(true);
        exitButton.SetActive(true);
    }

    private void StartFadeOutIdleLady()
    {
        fadeOutIdleLadyAnimator.SetTrigger("Start");
    }

    private void StartFadeInIdleHeroine()
    {
        fadeInIdleHeroineAnimator.SetTrigger("Start");
    }
}
