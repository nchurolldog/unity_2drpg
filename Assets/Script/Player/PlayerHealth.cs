using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public TMP_Text healthText;
    public Animator animator;

    private StatsManager Stats => StatsManager.Instance;

    void Start()
    {
        if (Stats == null)
            return;

        Stats.currentHealth = Stats.maxHealth;
        UpdateHealthUI();
    }

    public void ChangeHealth(int amount)
    {
        if (Stats == null)
            return;

        Stats.currentHealth += amount;
        Stats.currentHealth = Mathf.Clamp(Stats.currentHealth, 0, Stats.maxHealth);
        animator.Play("HealthUpdate");
        UpdateHealthUI();

        if (Stats.currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void UpdateHealthUI()
    {
        healthText.text = "Hp: " + Stats.currentHealth.ToString() + "/" + Stats.maxHealth.ToString();
    }
}