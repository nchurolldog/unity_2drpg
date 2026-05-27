using System;
using UnityEngine;

public class Loot : MonoBehaviour
{
  public ItemSO itemSO;
  public SpriteRenderer sr;
  public Animator animator;
  public int quantity;
    public static event Action<ItemSO,int> LootPicked;
  private void OnValidate()
  {
    if (itemSO == null) return;
    sr.sprite = itemSO.icon;
    this.name = itemSO.itemName;
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("Player"))
    {
        animator.Play("LootPickUP");
        LootPicked?.Invoke(itemSO, quantity);
        Destroy(gameObject,0.5f);
    }
  }
}
