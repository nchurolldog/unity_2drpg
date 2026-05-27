using UnityEngine;

public class Enemy_Health : MonoBehaviour
{
    public int expReward = 3;

    public delegate void MonsterDefeated(int exp);
    public static event MonsterDefeated OnMonsterDefeated;
    public int maxHealth = 10;
    public int currentHealth;
    
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void ChangeHealth(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        if(currentHealth <= 0)
        {
            OnMonsterDefeated?.Invoke(expReward);
            Destroy(gameObject);
        }
    }

}
