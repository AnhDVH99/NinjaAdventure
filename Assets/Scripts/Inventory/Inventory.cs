using System;
using System.Collections;
using System.Collections.Generic;
using BayatGames.SaveGameFree;
using UnityEngine;

public class Inventory : Singleton<Inventory>
{
    [Header("Config")] [SerializeField] private GameContent gameContent;
    [SerializeField] private int inventorySize;
    [SerializeField] private InventoryItem[] inventoryItems;
    private readonly string INVENTORY_DATA_KEY = "MY_INVENTORY";
    public int InventorySize => inventorySize;
    public InventoryItem[] InventoryItems => inventoryItems;

    public PlayerAttack  Player { get; set; }
    public void Start()
    {
        inventoryItems = new InventoryItem[inventorySize];
        VerifyForItemDraw();
        LoadInventory();
        // SaveGame.Delete(INVENTORY_DATA_KEY);
    }


    public void AddItem(InventoryItem item, int quantity)
    {
        if (IsInventoryFull())
        {
            Debug.Log("Can't add more item");
            return;
        }
        if (item == null || quantity <= 0 ) return;
        List<int> itemIndexes = CheckItemStock(item.ID);
        if (item.isStackable && itemIndexes.Count > 0)
        {
            foreach (int index in itemIndexes)
            {
                int maxStack = item.MaxStack;
                if (inventoryItems[index].Quantity < maxStack)
                {
                    inventoryItems[index].Quantity += quantity;
                    if (inventoryItems[index].Quantity > maxStack && index < inventorySize)
                    {
                        int difAmount = inventoryItems[index].Quantity - maxStack;
                        inventoryItems[index].Quantity = maxStack;
                        Debug.Log("full");
                        AddItem(item, difAmount);
                    }

                    InventoryUI.Instance.DrawItem(inventoryItems[index], index);
                    SaveInventory();
                    return;
                }
            }
        }

        int quantityToAdd = quantity > item.MaxStack ? item.MaxStack : quantity;
        AddItemFreeSlot(item, quantityToAdd);
        int remainAmount = quantity - quantityToAdd;
        if (remainAmount > 0)
        {
            AddItem(item, remainAmount);
        }
        SaveInventory();
    }

    private void AddItemFreeSlot(InventoryItem item, int quantity)
    {
        for (int i = 0; i < inventorySize; i++)
        {
            if (inventoryItems[i] != null) continue;
            inventoryItems[i] = item.CopyItem();
            inventoryItems[i].Quantity = quantity;
            InventoryUI.Instance.DrawItem(inventoryItems[i], i);
            return;
        }
    }

    public bool IsInventoryFull()
    {
        for (int i = 0; i < inventorySize; i++)
        {
            if (inventoryItems[i] == null)
            {
                return false;
            }
        }
        return true;
    }

    public void UseItem(int index)
    {
        if (inventoryItems[index] == null) return;
        if (inventoryItems[index].ItemType != ItemType.Potion) return;
        if (inventoryItems[index].UseItem())
        {
            DecreseItemStack(index);
        }
        SaveInventory();
    }

    public void EquipItem(int index)
    {
        if (inventoryItems[index] == null) return;
        if (inventoryItems[index].ItemType != ItemType.Weapon) return;
        inventoryItems[index].EquipItem();
    }

    public void RemoveItem(int index)
    {
        if (inventoryItems[index] == null) return;
        inventoryItems[index].RemoveItem();
        inventoryItems[index] = null;
        InventoryUI.Instance.DrawItem(null, index);
        SaveInventory();
    }

    private void DecreseItemStack(int index)
    {
        inventoryItems[index].Quantity--;
        if (inventoryItems[index].Quantity <= 0)
        {
            inventoryItems[index] = null;
            InventoryUI.Instance.DrawItem(null, index);
        }
        else
        {
            InventoryUI.Instance.DrawItem(inventoryItems[index], index);
        }
    }

    private List<int> CheckItemStock(string itemID)
    {
        List<int> itemIndexes = new List<int>();
        for (int i = 0; i < inventoryItems.Length; i++)
        {
            if (inventoryItems[i] == null) continue;
            if (inventoryItems[i].ID == itemID)
            {
                itemIndexes.Add(i);
            }
        }

        return itemIndexes;
    }

    private void VerifyForItemDraw()
    {
        for (int i = 0; i < inventorySize; i++)
        {
            if (inventoryItems[i] == null)
            {
                InventoryUI.Instance.DrawItem(null, i);
            }
        }
    }

    private InventoryItem ItemExistInGameContent(string itemID)
    {
        for (int i = 0; i < inventorySize; i++)
        {
            if (gameContent.gameItems[i].ID == itemID)
            {
                return gameContent.gameItems[i];
            }
        }

        return null;
    }

    private void LoadInventory()
    {
        if (SaveGame.Exists(INVENTORY_DATA_KEY))
        {
            InventoryData loadData = SaveGame.Load<InventoryData>(INVENTORY_DATA_KEY);
            for (int i = 0; i < inventorySize; i++)
            {
                if (loadData.ItemContent[i] != null)
                {
                    InventoryItem itemFromContent =
                        ItemExistInGameContent(loadData.ItemContent[i]);
                    if (itemFromContent != null)
                    {
                        inventoryItems[i] = itemFromContent.CopyItem();
                        inventoryItems[i].Quantity = loadData.ItemQuantity[i];
                        InventoryUI.Instance.DrawItem(InventoryItems[i], i);
                    }
                    else
                    {
                        inventoryItems[i] = null;
                    }
                }
            }
        }
    }

    private void SaveInventory()
    {
        InventoryData saveData = new InventoryData();
        saveData.ItemContent = new string[inventorySize];
        saveData.ItemQuantity = new int[inventorySize];
        for (int i = 0; i < inventorySize; i++)
        {
            if (inventoryItems[i] == null)
            {
                saveData.ItemContent[i] = null;
                saveData.ItemQuantity[i] = 0;
            }
            else
            {
                saveData.ItemContent[i] = inventoryItems[i].ID;
                saveData.ItemQuantity[i] = inventoryItems[i].Quantity;
            }
        }

        SaveGame.Save(INVENTORY_DATA_KEY, saveData);
    }
}