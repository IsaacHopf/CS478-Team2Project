using System.Collections;
using TMPro;
using UnityEngine;

public class BossFightAnimationData //this data is saved during a play session, and it can be accessed from any scene at any time
{
    public static bool hasSeenBossFightAnimation = false;
}

public class BossFightAnimation : MonoBehaviour
{
    [SerializeField] private GameObject boss;

    [SerializeField] private Animator bossFightAnimationTextAnimator;
    [SerializeField] private TextMeshProUGUI bossFightAnimationText;

    private int numTriggers = 0;

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        numTriggers++;

        if (numTriggers == 1)
        {
            if (BossFightAnimationData.hasSeenBossFightAnimation == false)
            {
                StartCoroutine(PlayFullBossFightAnimation());
                BossFightAnimationData.hasSeenBossFightAnimation = true;
            }
            else
            {
                StartCoroutine(PlayShortBossFightAnimation());
            }
        }    
    }

    private IEnumerator PlayFullBossFightAnimation()
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

        ActivateBossFight();
    }

    private IEnumerator PlayShortBossFightAnimation()
    {
        yield return new WaitForSeconds(0.2f);
        GetComponent<Animator>().SetTrigger("DeactivateIllusion");
        yield return new WaitForSeconds(1.5f);

        ActivateBossFight();
    }

    private void ActivateBossFight()
    {
        boss.SetActive(true);
        gameObject.SetActive(false);
    }
}
