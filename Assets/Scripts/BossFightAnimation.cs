using System.Collections;
using TMPro;
using UnityEngine;

public class BossFightAnimationData //this data is saved during a play session, and it can be accessed from any scene
{
    public static bool hasSeenBossFightIntro = false;
}

public class BossFightAnimation : MonoBehaviour
{
    [SerializeField] private GameObject boss;

    [SerializeField] private Animator bossFightAnimationTextAnimator;
    [SerializeField] private TextMeshProUGUI bossFightAnimationText;

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        GetComponent<EdgeCollider2D>().enabled = false;
        StartCoroutine(TriggerBossFight());
    }

    private IEnumerator TriggerBossFight()
    {
        yield return new WaitForSeconds(1f);
        bossFightAnimationText.text = "Foolish Girl";
        bossFightAnimationTextAnimator.SetTrigger("FadeIn");
        yield return new WaitForSeconds(2.75f);
        bossFightAnimationTextAnimator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(0.25f);
        GetComponent<Animator>().SetTrigger("ShowIllusion");
        yield return new WaitForSeconds(5f);
        GetComponent<Animator>().SetTrigger("DeactivateIllusion");
        bossFightAnimationText.text = "Prepare to Die";
        bossFightAnimationTextAnimator.SetTrigger("FadeIn");
        yield return new WaitForSeconds(2.5f);
        bossFightAnimationTextAnimator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(0.5f);

        boss.SetActive(true);
        gameObject.SetActive(false);

        BossFightAnimationData.hasSeenBossFightIntro = true;
    }

    private void Start()
    {
        if (BossFightAnimationData.hasSeenBossFightIntro == true)
        {
            boss.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
