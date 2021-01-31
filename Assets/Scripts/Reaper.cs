using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reaper : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        GameObject otherObject = collider.gameObject;
        if (otherObject.GetComponent<Castle>() || otherObject.GetComponent<ArcherWall>())
        {
            GetComponent<Animator>().SetTrigger("jumpTrigger");
        }
        else if (otherObject.GetComponent<Defender>())
        {
            GetComponent<Attacker>().Attack(otherObject);
        }
    }

}
