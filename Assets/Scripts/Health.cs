using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float health = 100;
    [SerializeField] AudioClip dieSFX;
    [SerializeField] AudioClip hurtSFX;
    [SerializeField] [Range(0f, 1f)] float dieSFXVolume = 0.5f;
    [SerializeField] [Range(0f, 1f)] float hurtSFXVolume = 0.5f;
    [SerializeField] GameObject explosionVFX;
    [SerializeField] GameObject smallExplosionVFX;
    [SerializeField] GameObject hitTarget;
    [SerializeField] float durationOfExplosion = 1f;
    [SerializeField] float durationOfSmallExplosion = 0.7f;

    public void DealDamage(float damage) 
    {
        health -= damage; 
        if (health < 0) {
            Die();
        }
        else
        { 
            AudioSource.PlayClipAtPoint(hurtSFX, Camera.main.transform.position, hurtSFXVolume);
            TriggerHurtVFX();
        } 
    }

    private void Die()
    {
        AudioSource.PlayClipAtPoint(dieSFX, Camera.main.transform.position, dieSFXVolume);
        TriggerDeathVFX();
        Destroy(gameObject);

    }


    private void TriggerHurtVFX()
    {
        if (!smallExplosionVFX) { return; }
        GameObject explosion;

        if (hitTarget) { 
             explosion = Instantiate(
                smallExplosionVFX, hitTarget.transform.position, Quaternion.identity) as GameObject;
        } else { 
            explosion = Instantiate(
                smallExplosionVFX, transform.position, Quaternion.identity) as GameObject;
        }

        Destroy(explosion, durationOfExplosion);
    }

    private void TriggerDeathVFX()
    {
        if (!explosionVFX) { return; }
        GameObject explosion;

        if (hitTarget) { 
             explosion = Instantiate(
                explosionVFX, hitTarget.transform.position, Quaternion.identity) as GameObject;
        } else { 
            explosion = Instantiate(
                explosionVFX, transform.position, Quaternion.identity) as GameObject;
        }

        Destroy(explosion, durationOfExplosion);
    }
}
