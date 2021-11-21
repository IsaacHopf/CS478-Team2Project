using System.Collections;
using UnityEngine;

public class Popup : MonoBehaviour
{
    public GameObject popup;

    Collider2D playerCollider;
    Animator popupAnimator;

    bool hasBeenSeen = false;

    void Start()
    {
        playerCollider = FindObjectOfType<Player>().GetComponent<Collider2D>();
        popupAnimator = popup.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider == playerCollider && !hasBeenSeen)
        {
            popup.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D otherCollider)
    {
        if (otherCollider == playerCollider)
        {
            StartCoroutine(DisablePopup());
            hasBeenSeen = true;
        }
    }

    private IEnumerator DisablePopup()
    {
        yield return new WaitForSeconds(1);
        popupAnimator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1);
        popup.SetActive(false);
    }
}
