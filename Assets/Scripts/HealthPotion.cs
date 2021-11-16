using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    [SerializeField] private int potionHealth;

    Player player;
    Collider2D playerCollider;
    Animator potionAnimator;

    void Start()
    {
        player = FindObjectOfType<Player>();
        playerCollider = player.GetComponent<Collider2D>();
        potionAnimator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider == playerCollider)
        {
            if (player.GetCurrentHealth() < player.GetMaxHealth())
            {
                player.AdjustCurrentHealth(potionHealth);
                potionAnimator.SetTrigger("UsePotion");
                Invoke("DisableHealthPotion", 0.42f);
            }
        } 
    }
    private void DisableHealthPotion()
    {
        gameObject.SetActive(false);
    }
}
