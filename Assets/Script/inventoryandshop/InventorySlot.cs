using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class InventorySlot : MonoBehaviour
{
    public ItemSO item;
    public int quantity;
    public Image image;
    public TMP_Text text;
    public void UpdateUI()
    {   
        if (item != null)
        {
            image.sprite = item.icon;
            image.gameObject.SetActive(true);
            text.text =quantity.ToString();
        }
        else
        {
            image.gameObject.SetActive(false);
            text.text = "";
        }
    }
}
