using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgrade : MonoBehaviour
{
    public static event Action OnPlayerUpgradeEvent;
    [SerializeField] private PlayerStats stats;
    [Header("Settings")]
    [SerializeField] private UpgradeSettings[] settings;

    private void UpgradePlayer(int upgradeIndex)
    {
        stats.BaseDamage += settings[upgradeIndex].DamageUpgrade;
        stats.TotalDamage += settings[upgradeIndex].DamageUpgrade;
        stats.MaxHealth += settings[upgradeIndex].HealthUpgrade;
        stats.Health +=  settings[upgradeIndex].HealthUpgrade;
        stats.MaxMana += settings[upgradeIndex].ManaUpgrade;
        stats.Mana += settings[upgradeIndex].ManaUpgrade;
        stats.CriticalChance += settings[upgradeIndex].CChanceUpgrade;
        stats.CriticalDamage += settings[upgradeIndex].CDamageUpgrade;
    }

    private void AttributeCallBack(AttributeType attributeType)
    {
        if(stats.AttributePoints == 0) return;
        switch (attributeType)
        {
            case AttributeType.Strength:
                UpgradePlayer(0);
                stats.Strength++;
                break;
            case AttributeType.Dexterity:
                UpgradePlayer(1);
                stats.Dexterity++;
                break;
            case AttributeType.Intelligence:
                UpgradePlayer(2);
                stats.Intelligence++;
                break;
        }

        stats.AttributePoints--;
        OnPlayerUpgradeEvent?.Invoke();
    }
    private void OnEnable()
    {
        AttributeButton.OnAttributeSelectedEvent += AttributeCallBack;
    }

    private void OnDisable()
    {
        AttributeButton.OnAttributeSelectedEvent -= AttributeCallBack;

    }
}

[Serializable]
public class UpgradeSettings
{
    public string name;
    
    [Header("Values")]
    public float DamageUpgrade;
    public float HealthUpgrade;
    public float ManaUpgrade;
    public float CChanceUpgrade;
    public float CDamageUpgrade;
    
    
}
