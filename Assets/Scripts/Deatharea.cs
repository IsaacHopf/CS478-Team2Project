using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deatharea : MonoBehaviour
{
    Player player;
    Collider2D playerCollider;
    
    // Start is called before the first frame update


    void Start()
    {

        player = FindObjectOfType<Player>();
        playerCollider = player.GetComponent<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
       int fulldeath = player.GetMaxHealth();

       player.AdjustCurrentHealth(-fulldeath);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
