using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LootButton : MonoBehaviour
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemQuantity;
    public DropItem ItemLoaded { get; private set; }
    

    public void ConfigLootButton(DropItem dropItem)
    {
        ItemLoaded = dropItem;
        itemIcon.sprite = dropItem.Item.Icon;
        itemName.text = dropItem.Item.Name;
        itemQuantity.text = $"X{dropItem.Quantity.ToString()}";
    }

    public void CollectItem()
    {
        if (ItemLoaded == null) return;
        if (Inventory.Instance.IsInventoryFull())
        {
            Debug.Log("Inventory is full!!");
            return;
        }
        Inventory.Instance.AddItem(ItemLoaded.Item, ItemLoaded.Quantity);
        ItemLoaded.PickedItem = true;
        Destroy(gameObject);
    }
}