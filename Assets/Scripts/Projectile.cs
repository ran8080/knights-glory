using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] [Range (0f, 5f)] float currentSpeed = 1f;
    [SerializeField] int damage = 50;

    void Update()
    {
        transform.Translate(Vector2.right * Time.deltaTime * currentSpeed, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var health = collision.gameObject.GetComponent<Health>();

        if (isObjectAttacker(collision.gameObject) && health != null) {
            health.DealDamage(damage);
            Destroy(gameObject);
        }
    }

    private bool isObjectAttacker(GameObject gameObject) { 
        if (gameObject.GetComponent<Attacker>() == null) 
        {
            return false;
        }

        return true;
    }

}
