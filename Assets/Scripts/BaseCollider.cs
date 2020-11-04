using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCollider: MonoBehaviour
{
    
    // State variables
    LivesDisplay livesDisplay;

    private void Start()
    {
        livesDisplay = FindObjectOfType<LivesDisplay>();
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        Attacker currentAttacker = otherCollider.gameObject.GetComponent<Attacker>();
        if (currentAttacker)
        {
            livesDisplay.DealDamage(currentAttacker.GetLivesToTake());
        }

        Destroy(otherCollider.gameObject);
    }

}
