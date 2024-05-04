using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private PlayerStats stats;

    public ItemHealthPotion HealthPotion;
    public ItemManaPotion ManaPotion;
    public PlayerStats Stats => stats;
    public PlayerHealth PlayerHealth { get; private set; }

    public PlayerAttack PlayerAttack { get; private set; }
    public PlayerMana PlayerMana { get; private set; }

    private PlayerAnimations animations;
    

    private void Awake()
    {
        PlayerAttack = GetComponent<PlayerAttack>();
        PlayerMana = GetComponent<PlayerMana>();
        animations = GetComponent<PlayerAnimations>();
        PlayerHealth = GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (HealthPotion.UseItem())
            {
                Debug.Log("Using Health Potion");
            }
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            if (ManaPotion.UseItem())
            {
                Debug.Log("Using Mana Potion");
            }
        }
        
    }

    public void ResetPlayer()
    {
        stats.ResetPlayer();
        animations.ResetPlayer();
    }
    
}
