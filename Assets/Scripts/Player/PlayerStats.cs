using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttributeType
{
    Strength,
    Dexterity,
    Intelligence
}
[CreateAssetMenu(fileName = "PlayerStats", menuName = "Player Stats")]
public class PlayerStats : ScriptableObject
{
    [Header("Config")] public int Level;

    [Header("Health")] public float Health;
    public float initialHealth;
    public float MaxHealth;

    [Header("Mana")] public float Mana;
    public float initialMana;
    public float MaxMana;

    [Header("EXP")] public float CurrentExp;
    public float NextLevelExp;
    public float InitialNextLevelExp;
    [Range(1f, 100f)] public float ExpMultiplier;

    [Header("Attack")] 
    public float BaseDamage;
    public float CriticalChance;
    public float CriticalDamage;

    [Header("Config")] 
    public int Strength;
    public int Dexterity;
    public int Intelligence;
    public int AttributePoints;
    

    [HideInInspector] public float TotalExp;
    [HideInInspector] public float TotalDamage;
    public void ResetPlayer()
    {
        Health = initialHealth;
        Mana = initialMana;
        MaxHealth = initialHealth;
        MaxMana = initialMana;
        Level = 1;
        CurrentExp = 0f;
        NextLevelExp = InitialNextLevelExp;
        TotalExp = 0;
        BaseDamage = 2;
        CriticalChance = 10;
        CriticalDamage = 50;
        Strength = 0;
        Dexterity = 0;
        Intelligence = 0;
        AttributePoints = 0;
    }
}