using System.Collections;
using TMPro;
using UnityEngine;

public class IntroAnimationData //this data is saved during a play session, and it can be accessed from any scene at any time
{
    public static bool hasSeenIntroAnimation = false;
}

public class IntroAnimation : MonoBehaviour
{
    //Necessary game objects and components
    [SerializeField] 
    private GameObject playButton, exitButton, background;
    [SerializeField] 
    private Animator fadeInNameTextAnimator, fadeInTheDemonSlayerTextAnimator, fadeOutIdleLadyAnimator, fadeInIdleHeroineAnimator, introAnimationTextAnimator, fadeOutBlackBackgroundAnimator;
    [SerializeField] 
    private TextMeshProUGUI introAnimationText;

    private Animator introAnimator;

    private void Start()
    {
        introAnimator = GetComponent<Animator>();

        if (IntroAnimationData.hasSeenIntroAnimation == false)
        {
            StartCoroutine(PlayIntroAnimation());
            IntroAnimationData.hasSeenIntroAnimation = true;
        }
        else
        {
            StartCoroutine(LoadMainMenu());
        }
    }

    private IEnumerator PlayIntroAnimation()
    {
        introAnimator.SetTrigger("PlayIntro");
        yield return new WaitForSeconds(63f);
        fadeInNameTextAnimator.SetTrigger("Start");
        yield return new WaitForSeconds(2f);
        fadeInTheDemonSlayerTextAnimator.SetTrigger("Start");
        yield return new WaitForSeconds(2.5f);
        playButton.SetActive(true);
        exitButton.SetActive(true);
    }

    private IEnumerator LoadMainMenu()
    {
        fadeOutBlackBackgroundAnimator.gameObject.SetActive(false);

        background.SetActive(true);
        yield return new WaitForSeconds(1f);
        fadeInNameTextAnimator.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        fadeInTheDemonSlayerTextAnimator.SetTrigger("Start");
        yield return new WaitForSeconds(1.5f);
        playButton.SetActive(true);
        exitButton.SetActive(true);
    }

    //Functions called during IntroAnimation
    private void StartFadeOutIdleLady()
    {
        fadeOutIdleLadyAnimator.SetTrigger("Start");
    }

    private void StartFadeInIdleHeroine()
    {
        fadeInIdleHeroineAnimator.SetTrigger("Start");
    }

    private void StartFadeOutBlackBackground()
    {
        fadeOutBlackBackgroundAnimator.SetTrigger("Start");
    }

    private int storyIndex = 0;
    private string[] storyText = {"Long ago ...",
        "Demons infested the Outer Lands ...",
        "until the world's hero fought back.",
        "He was beloved by many ...",
        "including his true love, Rose.",
        "But the demons returned ...",
        "and captured the world's hero.",
        "Rose vowed to slay the demons.",
        "Will Rose save the world ...",
        "and her true love?"};

    private IEnumerator ShowNextIntroAnimationTextShort()
    {
        introAnimationText.text = storyText[storyIndex];
        introAnimationTextAnimator.SetTrigger("FadeIn");
        yield return new WaitForSeconds(2.5f);
        introAnimationTextAnimator.SetTrigger("FadeOut");

        storyIndex++;
    }

    private IEnumerator ShowNextIntroAnimationTextLong()
    {
        introAnimationText.text = storyText[storyIndex];
        introAnimationTextAnimator.SetTrigger("FadeIn");
        yield return new WaitForSeconds(3.5f);
        introAnimationTextAnimator.SetTrigger("FadeOut");

        storyIndex++;
    }
}
