using System;
using TMPro;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{   public InventorySlot[] inventorySlots;
    int currentGold = 0;
    public TMP_Text goldText;
    
    private void OnEnable()
    {
        Loot.LootPicked += PickUpItem;
    }

    void OnDisable()
    {
        Loot.LootPicked -= PickUpItem;
    }

    public void PickUpItem(ItemSO item,int quantity)
    {
        if (item.isGold)
        {
            currentGold += quantity;
            goldText.text = currentGold.ToString();
            return;
        }
        else
        {
            foreach (var VARIABLE in inventorySlots)
            {
                if (VARIABLE.item == null)
                {   VARIABLE.item = item;
                    VARIABLE.quantity = quantity;
                    VARIABLE.UpdateUI();
                    return;
                }
                
            }
        }
    }
}
