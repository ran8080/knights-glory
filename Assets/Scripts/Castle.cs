using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D otherCollider)
    {
        Attacker attacker = otherCollider.GetComponent<Attacker>();
        //if (attacker)
        //{
        //    // TODO Add some sort of animation
        //    Debug.Log("Touched the castle");
        //}
    }
}
