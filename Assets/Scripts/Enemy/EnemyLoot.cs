using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class EnemyLoot : MonoBehaviour
{
    [Header("Config")] [SerializeField] private float expDrop;

    [SerializeField] private DropItem[] dropItems;
    public List<DropItem> Items { get; private set; }

    private void Start()
    {
        LoadDropItem();
    }

    private void LoadDropItem()
    {
        Items = new List<DropItem>();
        foreach (DropItem item in dropItems)
        {
            float chance = Random.Range(0f, 100);
            if (chance <= item.DropChance)
            {
                Items.Add(item);
            }
        }
    }
    public float ExpDrop => expDrop;
}

[Serializable]
public class DropItem
{
    [Header("Config")] public string Name;
    public InventoryItem Item;
    public int Quantity;

    [Header("Drop Chance")] 
    public float DropChance;
    public bool PickedItem { get; set; }
}