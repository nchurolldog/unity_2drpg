using System;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class SkillTree : MonoBehaviour
{
    public SkillSlot[]  skillSlots;
    public TMP_Text points;
    public int availablePoints;
    
    private void Start()
    {
        foreach (SkillSlot slot in skillSlots)
        {
            slot.skillbutton.onClick.AddListener(()=>CheckAvailable(slot));
        }
        UpdateSkillPoints(0);
        
    }
    private void CheckAvailable(SkillSlot slot)
    {
        if (availablePoints > 0)
        {
          slot.TryUpgradeSkill();
        }
    }
    private void HandleAbilityPointSpent(SkillSlot slot)
    {
        if (availablePoints > 0)
        {
            UpdateSkillPoints(-1);
            
        }
    }

    private void HandleSkillMaxed(SkillSlot skillSlot)
    {
        foreach (SkillSlot slot in skillSlots)
        {
            if(!slot.isactive&&slot.CanbeUnlocked())
            slot.Unlock();
        }
    }

    private void OnEnable()
    {
        SkillSlot.OnAbilityPointSpent += HandleAbilityPointSpent;
        SkillSlot.OnSkillMaxed += HandleSkillMaxed;
        ExpManager.OnLevelUp += UpdateSkillPoints;
    }

    private void OnDisable()
    {
        SkillSlot.OnAbilityPointSpent -= HandleAbilityPointSpent;
        SkillSlot.OnSkillMaxed -= HandleSkillMaxed;
        ExpManager.OnLevelUp -= UpdateSkillPoints;
    
    }

    public void UpdateSkillPoints(int amount)
    {
        availablePoints += amount;
        points.text ="Points: " + availablePoints.ToString();
    }
}
