using UnityEngine;

[CreateAssetMenu(fileName = "New Item")]
public class ItemSO : ScriptableObject
{
    public Sprite icon;
    public string itemName;
   [TextArea] public string itemDescription;
   public bool isGold;
   [Header("Stats")]
   public int currentHealth;
   public int maxHealth;
   public int speed;
   public int damage;
    public int stacksize;
   [Header("For Temporary Items")] public float duration;



}
