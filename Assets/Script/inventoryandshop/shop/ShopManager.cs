using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System;
public class ShopManager : MonoBehaviour
{
   
    [SerializeField] private ShopSlot[] slots;
    [SerializeField] private InventoryManager inventorymanager;


    public void PopulateShopItems(List<ShopItems> shopitems)
    {
        for (int i = 0; i < shopitems.Count; i++)
        {
            ShopItems shopItem=shopitems[i];
            slots[i].Initialize(shopItem.item, shopItem.price);
            slots[i].gameObject.SetActive(true);
        }
        for(int i=shopitems.Count; i < slots.Length; i++)
        {
            slots[i].gameObject.SetActive(false);
        }
    }

    public void TryBuyItem(ItemSO itemSO, int price)
    {
        if(inventorymanager .currentGold< price)
        {   print("Not enough gold to buy this item.");
            return;
        }
        if (itemSO != null && inventorymanager.currentGold >= price)
        {
            if (HasSpaceForItem(itemSO))
            {
                inventorymanager.currentGold -= price;
                inventorymanager.goldText.text = inventorymanager.currentGold.ToString();
                inventorymanager.PickUpItem(itemSO,1);
            }
        }
    }
        private bool HasSpaceForItem(ItemSO itemSO)
    {
        foreach (var slot in inventorymanager.inventorySlots)
        {
            if(slot.item == itemSO&&slot.quantity<itemSO.stacksize)
            {
                return true;
            }else if (slot.item == null)
            {
                return true;
            }
        }
        
        return false;
    }
    public void SellItem(ItemSO itemSO)
    {
      if(itemSO == null)
        {
            return;
        }
      foreach (var slot in slots) 
            if(slot.itemSO == itemSO)
            {
                inventorymanager.currentGold += slot.price;
                inventorymanager.goldText.text = inventorymanager.currentGold.ToString();
                return;
            }

    }
}
[System.Serializable]
public class ShopItems
{
    public ItemSO item;
    public int price;
}