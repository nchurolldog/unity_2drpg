using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
public class InventorySlot : MonoBehaviour, IPointerClickHandler
{

    public ItemSO item;

    public int quantity;

    public Image image;

    public TMP_Text text;//reference to the text of the slot, used to display quantity

    public InventoryManager inventoryManager;

    public static ShopManager currentShop;
    private void OnEnable()
    {
        ShopKeeper.OnShopStateChanged+= HandleShopStateChanged;
    }
    private void OnDisable()
    {
        ShopKeeper.OnShopStateChanged -= HandleShopStateChanged;
    }
    private void HandleShopStateChanged(ShopManager manager, bool isactive)
    {
             currentShop= isactive ? manager : null;
    }

    private void Start()

    {

        inventoryManager = GetComponentInParent<InventoryManager>();

    }



    public void UpdateUI()

    {
        if (quantity <= 0)
        {
            item= null;
        }

        if (item != null)

        {

            image.sprite = item.icon;

            image.gameObject.SetActive(true);

            text.text = quantity.ToString();

        }

        else

        {

            image.gameObject.SetActive(false);

            text.text = "";

        }

    }



    public void OnPointerClick(PointerEventData eventData)

    {

        if (quantity > 0)

        {

            if (eventData.button == PointerEventData.InputButton.Left)

            {
                if(currentShop!= null)
                {
                    currentShop.SellItem(item);
                    quantity--; 
                    UpdateUI();
                }

                else
                { 
                    inventoryManager.UseItem(this); }

            }
            else if (eventData.button == PointerEventData.InputButton.Right)

            {

                inventoryManager.DropItem(this);

            }

        }

    }

}