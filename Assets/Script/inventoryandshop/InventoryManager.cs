using System;
using Script.inventoryandshop;
using TMPro;
using UnityEngine;
public class InventoryManager : MonoBehaviour
{
    public InventorySlot[] inventorySlots;

    public  int currentGold = 0;

    public TMP_Text goldText;

    public UseItem useItem;
    public GameObject lootPrefeb;

    public Transform player;

   

    private void OnEnable()

    {

        Loot.LootPicked += PickUpItem;

    }



    void OnDisable()

    {

        Loot.LootPicked -= PickUpItem;

    }

    private void Start()
    {
        goldText.text = currentGold.ToString();
    }

    public void PickUpItem(ItemSO item, int quantity)//parameter quantity means loot quantity and slot quantity means the quantity of item in inventory slot, if the item is stackable and already exists in inventory, add to that slot until it reaches stack limit, then create new slot for remaining quantity.

    {

        if (item.isGold)

        {

            currentGold += quantity;

            goldText.text = currentGold.ToString();

            return;

        }

        foreach (var slot in inventorySlots)

        {

            if (slot.item == item && slot.quantity < item.stacksize)

            {

                int availableSpace = item.stacksize - slot.quantity;

                int amountToAdd = Math.Min(availableSpace, quantity);

                slot.quantity += amountToAdd;

                quantity -= amountToAdd;
                slot.UpdateUI();

                if (quantity <= 0)
                {

                    return;
                }
            }
        }

        foreach (var slot in inventorySlots)

        {

            if (slot.item == null)

            {
                int amountToAdd = Math.Min(item.stacksize, quantity);

                slot.item = item;

                slot.quantity = quantity;

                slot.UpdateUI();

                return;

            }
        }
            /*tackle the case when slot is full but

            * there are still items be picked up

            */

            if (quantity > 0)

            {

                DropLoot(item, quantity);

            }//bug here?

        

    }

    public void DropItem(InventorySlot slot)

    {

        DropLoot(slot.item, 1);

        slot.quantity--;

        if (slot.quantity <= 0)

        {

            slot.item = null;

        }

        slot.UpdateUI();

    }

    private void DropLoot(ItemSO item, int quantity)

    {

        Loot loot = Instantiate(lootPrefeb, player.position, Quaternion.identity).GetComponent<Loot>();

        loot.Initialize(item, quantity);

    }

    public void UseItem(InventorySlot slot) //if the item is used, apply its effects and decrease quantity by 1. If quantity reaches 0, clear the slot.

    {

        if (slot.item != null && slot.quantity > 0)

        {

            useItem.ApplyEffects(slot.item);

            slot.quantity--;

            if (slot.quantity <= 0)

            {
                slot.item = null;
            }

            slot.UpdateUI();

        }

    }
}