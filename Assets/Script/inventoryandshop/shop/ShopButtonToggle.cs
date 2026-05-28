using UnityEngine;

public class ShopButtonToggle : MonoBehaviour
{

    public void OpenItemShop()
    {   if(ShopKeeper.currentKeeper != null)
        ShopKeeper.currentKeeper.OpenItemShop();
    }
    public void OpenWeaponShop()
    {
        if(ShopKeeper.currentKeeper != null)
        ShopKeeper.currentKeeper.OpenWeaponShop();
    }
    public void OpenArmorShop()
    { if(ShopKeeper.currentKeeper != null)
        ShopKeeper.currentKeeper.OpenArmourShop();
    }

}
 