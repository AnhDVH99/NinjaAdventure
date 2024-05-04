using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Config")] [SerializeField] private float speed;

    public Vector3 Direction { get; set; }
    public float Damage { get; set; }
    private void Update()
    {
        transform.Translate(Direction * (speed * Time.deltaTime));
        Destroy(gameObject, 1.5f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<IDamageable>()?.TakeDamage(Damage); 
            Destroy(gameObject);
        }
    }
}