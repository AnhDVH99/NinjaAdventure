using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : Singleton<WeaponManager>
{
    [Header("Congif")] 
    [SerializeField] private Image weaponIcon;
    [SerializeField] private TextMeshProUGUI weaponManaTMP;

    public void EquipWeapon(Weapon weapon)
    {
        weaponIcon.sprite = weapon.icon;
        weaponIcon.gameObject.SetActive(true);
        weaponManaTMP.text = weapon.requiredMana.ToString();
        weaponManaTMP.gameObject.SetActive(true);
        weaponIcon.SetNativeSize();
        GameManager.Instance.Player.PlayerAttack.EquipWeapon(weapon);
    }
}