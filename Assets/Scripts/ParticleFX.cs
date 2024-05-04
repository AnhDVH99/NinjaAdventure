using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleFX : MonoBehaviour
{
    public float Damage { get; set; }

    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            IDamageable enemyHealth = GetComponent<IDamageable>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(Damage);
            }
        }
        
    }
}