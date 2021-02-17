using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour
{

    [SerializeField] [Range(0, 360)] float defaultRotation = 180;
    [SerializeField] float damage = 20;
    [SerializeField] int livesToTake = 1;

    [Range (0f, 5f)] [SerializeField] float currentSpeed = 1f;
    [Range (0f, 5f)] [SerializeField] float baseSpeed = 1f;

    // States
    GameObject currentTarget;

    private void Awake()
    {
        var levelController = FindObjectOfType<LevelController>();
        if (levelController)
        {
            levelController.AttackerSpawned();
        } 
    }

    private void OnDestroy()
    {
        var levelController = FindObjectOfType<LevelController>();
        if (levelController)
        {
            levelController.AttackerKilled();
        } 
    }

    void Update()
    {
        transform.Translate(Vector2.left * Time.deltaTime * currentSpeed, Space.World);
        UpdateAnimationState();
    }

    public float GetDefaultRotation() {
        return defaultRotation;
    }

    public float GetDamage()
    {
        return damage;
    }

    public int GetLivesToTake()
    {
        return livesToTake;
    }

    public void MultiplyMovmentSpeed(float multiplier)
    { 
        currentSpeed *= multiplier;
    }

    public void SetMovmentSpeed(float speed) {
        currentSpeed = speed;
    }

    public void ResetMovmentSpeed() {
        currentSpeed = baseSpeed; 
    }

    public void Jump(float hight) { 
        transform.Translate(Vector2.up * Time.deltaTime * hight, Space.World);
    }

    public void Attack(GameObject target)
    {
        GetComponent<Animator>().SetBool("isAttacking", true);
        currentTarget = target;
    }

    public void StrikeCurrentTarget()
    { 
        if (!currentTarget) { return; }

        Health health = currentTarget.GetComponent<Health>();
        if (health)
        {
            health.DealDamage(damage);
        }
    }

    private void UpdateAnimationState()
    {
        if (!currentTarget)
        { 
            GetComponent<Animator>().SetBool("isAttacking", false);
        }
    }

}
