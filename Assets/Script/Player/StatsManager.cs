using UnityEngine;
using TMPro;
public class StatsManager : MonoBehaviour
{
    public static StatsManager Instance { get; private set; }
    public TMP_Text healthText;
    [Header("Combat Stats")]
    public int damage;
    public float weaponRange;
    public float knockbackForce;
    public float knockbackDuration;
    public float stuntime;
    public StatsUI statsUI;
    [Header("Movement Stats")]
    public float speed;

    [Header("Health Stats")]
    public int maxHealth;
    public int currentHealth;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    public void UpdateMaxHealth(int amount)
    {
        maxHealth+= amount;
        currentHealth += amount;
       healthText.text = "hp: "+currentHealth + "/" + maxHealth;
    }
    public void UpdateCurHealth(int amount)
    {
     
        currentHealth += amount;
        healthText.text = "hp: "+currentHealth + "/" + maxHealth;
    }
    public void UpdateSpeed(int amount)
    {
       speed += amount;
    }
}
