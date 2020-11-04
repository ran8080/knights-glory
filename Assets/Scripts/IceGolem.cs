using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceGolem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        GameObject otherObject = collider.gameObject;
        if (otherObject.GetComponent<Castle>())
        {
            GetComponent<Animator>().SetTrigger("jumpTrigger");
        }
        else if (otherObject.GetComponent<Defender>())
        { 
            GetComponent<Attacker>().Attack(otherObject);
        }
    }

}
