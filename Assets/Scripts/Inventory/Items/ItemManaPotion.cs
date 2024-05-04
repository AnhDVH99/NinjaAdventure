using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Mana Potion", fileName = "ItemManaPotion")]
public class ItemManaPotion : InventoryItem
{
   [Header("Config")] 
   public float manaValue;

   public override bool UseItem()
   {
      if (GameManager.Instance.Player.PlayerMana.CanRecoverMana())
      {
         GameManager.Instance.Player.PlayerMana.RecoverMana(manaValue);
         return true;
      }

      return false;
   }
}
