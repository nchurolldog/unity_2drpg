using System;

using UnityEngine;



public class Loot : MonoBehaviour

{

    public ItemSO itemSO;

    public SpriteRenderer sr;

    public Animator animator;

    public int quantity;

    public static event Action<ItemSO, int> LootPicked;



    public bool isPickedUp = true;

    private void OnValidate()

    {

        if (itemSO == null) return;

        UpdateAppearance();

    }



    private void OnTriggerEnter2D(Collider2D other)

    {

        if (other.CompareTag("Player") && isPickedUp)

        {

            animator.Play("LootPickUP");

            LootPicked?.Invoke(itemSO, quantity);

            Destroy(gameObject, 0.5f);

        }

    }

    private void OnTriggerExit2D(Collider2D collision)

    {

        if (collision.CompareTag("Player"))

        {

            isPickedUp = true;

        }

    }

    public void Initialize(ItemSO item, int quantity)

    {

        this.itemSO = item;

        this.quantity = quantity;

        isPickedUp = false;

        UpdateAppearance();

    }

    void UpdateAppearance()

    {

        sr.sprite = itemSO.icon;

        this.name = itemSO.itemName;

    }

}