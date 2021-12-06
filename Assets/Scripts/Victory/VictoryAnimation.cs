using System.Collections;
using TMPro;
using UnityEngine;

public class VictoryAnimation : MonoBehaviour
{
    [SerializeField] private Animator victoryAnimationTextAnimator, fadeInBlackBackgroundAnimator;
    [SerializeField] private TextMeshProUGUI victoryAnimationText;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        StartCoroutine(PlayVictoryAnimation());
    }

    private IEnumerator PlayVictoryAnimation()
    {
        yield return new WaitForSeconds(1);
        StartCoroutine(ShowNextVictoryAnimationText());
        yield return new WaitForSeconds(4f);
        StartCoroutine(ShowNextVictoryAnimationText());
        yield return new WaitForSeconds(4f);
        StartCoroutine(ShowNextVictoryAnimationText());
        yield return new WaitForSeconds(3.5f);
        fadeInBlackBackgroundAnimator.gameObject.SetActive(true);
        yield return new WaitForSeconds(4f);
        StartCoroutine(ShowNextVictoryAnimationText());
        yield return new WaitForSeconds(4f);
        StartCoroutine(ShowNextVictoryAnimationText());
        yield return new WaitForSeconds(4f);

        StartCoroutine(gameManager.LoadMainMenuScene());
    }

    private int victoryIndex = 0;
    private string[] victoryText = {"Rose slayed the demons ...",
        "and rescued her true love.",
        "The world was saved.",
        "Congratulations!",
        "Thanks for playing!"};

    private IEnumerator ShowNextVictoryAnimationText()
    {
        victoryAnimationText.text = victoryText[victoryIndex];
        victoryAnimationTextAnimator.SetTrigger("FadeIn");
        yield return new WaitForSeconds(3.5f);
        victoryAnimationTextAnimator.SetTrigger("FadeOut");

        victoryIndex++;
    }
}
