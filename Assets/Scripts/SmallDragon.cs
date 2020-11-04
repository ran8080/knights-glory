using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallDragon : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D otherObject)
    {
        if (otherObject.GetComponent<Defender>())
        { 
            GetComponent<Attacker>().Attack(otherObject.gameObject);
        }
    }
}
