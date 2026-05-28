using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShopKeeper : MonoBehaviour
{
    public static ShopKeeper currentKeeper;
    [SerializeField] private List<ShopItems> shopitems;
    [SerializeField] private List<ShopItems> shopweapons;
    [SerializeField] private List<ShopItems> shoparmors;
    [SerializeField] private Camera shopkeeperCamera;
    [SerializeField] Vector3 cameraOffset=new Vector3(0,0,-10);
    public Animator animator;
    public static event Action<ShopManager, bool> OnShopStateChanged;
    private bool isPlayerInRange = false;
    public CanvasGroup shopCanvasGroup;
    public InputActionReference open;
    public ShopManager shopManager;
    public bool isShopOpen = false;
    // Update is called once per frame
    private void OnEnable()
    {
        open.action.performed += ToggleShop;
        open.action.Enable();
    }
    private void OnDisable()
    {
        open.action.performed -= ToggleShop;
        open.action.Disable();
    }
    public void ToggleShop(InputAction.CallbackContext context)
    {
        if (isPlayerInRange)
        {
            isShopOpen = !isShopOpen;
           
            OnShopStateChanged?.Invoke(shopManager, isShopOpen);
            shopCanvasGroup.alpha = isShopOpen ? 1 : 0;
            shopCanvasGroup.interactable = isShopOpen;
            shopCanvasGroup.blocksRaycasts = isShopOpen;
           
            if (isShopOpen)
            {   currentKeeper = this;
                OpenItemShop();
                // Move the camera to focus on the shopkeeper
                shopkeeperCamera.transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
                shopkeeperCamera.gameObject.SetActive(true);
            }
            else
            {
                currentKeeper = null;
                // Move the camera back to its original position
                shopkeeperCamera.gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
            animator.SetBool("inRange", true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
            animator.SetBool("inRange", false);
        }
    }
    public void OpenItemShop()
    {
        shopManager.PopulateShopItems(shopitems);
    }
    public void OpenWeaponShop()
    {
        shopManager.PopulateShopItems(shopweapons);
    }
    public void OpenArmourShop()
    {
        shopManager.PopulateShopItems(shoparmors);
    }
}
