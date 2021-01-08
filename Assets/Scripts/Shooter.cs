using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{

    [SerializeField] GameObject projectile;
    [SerializeField] GameObject wepon;

    // States
    AttackerSpawner myLaneSpawner;
    Animator animator;
    GameObject projectileParent;

    const string PROJECTILE_PARENT_NAME = "Projectiles";

    private void Start()
    {
        SetLaneSpawner();
        animator = GetComponent<Animator>();
        CreateProjectileParent();
    }

    private void CreateProjectileParent()
    {
        projectileParent = GameObject.Find(PROJECTILE_PARENT_NAME);
        if (!projectileParent)
        {
            projectileParent = new GameObject(PROJECTILE_PARENT_NAME);
        }
    }

    private void Update()
    {
        if(IsAttackerInLane()) 
        {
            animator.SetBool("isAttacking", true);
        }
        else
        {
            animator.SetBool("isAttacking", false);
        }
    }

    private void SetLaneSpawner()
    {
        AttackerSpawner[] spawners = FindObjectsOfType<AttackerSpawner>();

        foreach(AttackerSpawner spawner in spawners)
        {
            var spawnerYPlusOffset = spawner.transform.position.y - spawner.GetSpwanerOffsetOnGrid();
            var isCloseEnough = 
                (Mathf.Abs(spawnerYPlusOffset - transform.position.y) <= Mathf.Epsilon);

            if (isCloseEnough) { 
                myLaneSpawner = spawner;
                Debug.Log("Set Lane Spawner " + myLaneSpawner.name + " For Shooter " + gameObject.name);
                return;
            }
        }
        Debug.LogWarning("Couldn't Set A Lane Spawner For Shooter " + gameObject.name);
    }

    private bool IsAttackerInLane()
    {
        if (myLaneSpawner.transform.childCount <= 0) 
        { 
            return false; 
        }
        else
        { 
            return true;
        }
    }

    public void fire() 
    {
        Debug.Log("Firing!");
        var projectileObject = Instantiate(
            projectile, wepon.transform.position, Quaternion.identity) as GameObject;
        projectileObject.transform.parent = projectileParent.transform;
    }
}
