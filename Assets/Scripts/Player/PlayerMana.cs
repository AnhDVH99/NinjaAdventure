using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMana : MonoBehaviour
{
    [Header("Config")] [SerializeField] private PlayerStats stats;
    public float currentMana { get;  private set; }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            UseMana(1f);
        }

        currentMana = stats.Mana;
    }

    public void RecoverMana(float amount)
    {
        stats.Mana += amount;
        stats.Mana = Mathf.Min(stats.Mana, stats.MaxMana);
    }
    public bool CanRecoverMana()
    {
        return stats.Mana >= 0 && stats.Mana < stats.MaxMana;
    }

    public void UseMana(float amount)
    {
        // shorten way
        stats.Mana = Mathf.Max(stats.Mana -= amount, 0f);
    }
}