using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using NUnit.Framework;
using System.Collections.Generic;
public class ShopInfo : MonoBehaviour
{
    public CanvasGroup infoPanel;
    public TMP_Text itemName;
    public TMP_Text itemDescription;
    [Header("Stats")]
    public TMP_Text[] stats;
    private RectTransform infoPanelRect;
    public InputActionReference mousePositionAction;
    private Vector2 mousePosition;
    private void Awake()
    {
        infoPanelRect = infoPanel.GetComponent<RectTransform>(); 
    }
    private void OnEnable()
    {
        mousePositionAction.action.performed += GetMousePos;
        mousePositionAction.action.Enable();

    }
    void OnDisable()
    {
        mousePositionAction.action.performed -= GetMousePos;
        mousePositionAction.action.Disable();
    }
    private void GetMousePos(InputAction.CallbackContext context)
    {
        mousePosition = context.ReadValue<Vector2>();
    }
    public void ShowInfo(ItemSO item)
    {
        infoPanel.alpha = 1;
        infoPanel.interactable = true;
        infoPanel.blocksRaycasts = true;
        itemName.text = item.itemName;
        itemDescription.text = item.itemDescription;
        List<string> statList = new List<string>();
        if(item.currentHealth>0) statList.Add("Health: " + item.currentHealth.ToString());
        if(item.speed > 0) statList.Add("Speed: " + item.speed.ToString());
        if (item.damage > 0) statList.Add("Damage: " + item.damage.ToString());
        if(item.duration > 0) statList.Add("Duration: " + item.duration.ToString() + "s");
        if (statList.Count <= 0)
        {
            return;
        }
        for (int i = 0; i < statList.Count; i++)
        {
            stats[i].text = statList[i];
            stats[i].gameObject.SetActive(true);
        }
        for(int i = statList.Count; i < stats.Length; i++)
        {
            stats[i].gameObject.SetActive(false);
        }
    }
    public void HideInfo()
    {
        infoPanel.alpha = 0;
        infoPanel.interactable = false;
        infoPanel.blocksRaycasts = false;
        itemName.text = "";
        itemDescription.text = "";
    }
    public void FollowMouse()
    {  
        Vector3 mousePosition =new Vector3(this.mousePosition.x, this.mousePosition.y, 0);
        Vector3 offset= new Vector3(10,-10, 0);
        infoPanelRect.position = mousePosition + offset;
    }
}
