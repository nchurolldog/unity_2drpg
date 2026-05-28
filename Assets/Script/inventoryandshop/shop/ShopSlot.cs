using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler,IPointerMoveHandler
{
    public ItemSO itemSO;
    public TMP_Text itemNameText;
    public TMP_Text priceText;
    public Image itemImage;
    public int price;
    [SerializeField] private ShopManager shopManager;
    [SerializeField]private  ShopInfo shopInfo;
    public void Initialize(ItemSO item,int price)
    {
        itemSO = item;
        itemNameText.text = itemSO.itemName;
        itemImage.sprite = itemSO.icon;
        this.price = price;
        priceText.text = price.ToString();
    }

    public void OnBuyButtonClicked()
    {
        if (shopManager != null)
        {
            shopManager.TryBuyItem(itemSO, price);
        }
    }



    public void OnPointerEnter(PointerEventData eventData)
    {      if(itemSO != null) 
        shopInfo.ShowInfo(itemSO);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (itemSO != null)
            shopInfo.HideInfo();
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        if (itemSO != null)
            shopInfo.FollowMouse();
    }
}
