using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHomingBullet : MonoBehaviour
{
    public float speed;
    public float damage;
    private GameObject player;
    

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        transform.position = 
            Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        Destroy(gameObject,1.5f );
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.GetComponent<IDamageable>().TakeDamage(damage);
            Destroy(gameObject);
        }
        
    }
}