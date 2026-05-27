using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class SkillSlot : MonoBehaviour
{
 public SkillSO skill;
 public Image icon;
    public int currentlevel;
    public bool isactive;
    public TMP_Text skillText;
    public Button skillbutton;
    public List<SkillSlot> prerequisites;
    public static event Action<SkillSlot> OnAbilityPointSpent;
    public static event Action<SkillSlot> OnSkillMaxed;
 private void OnValidate()
 {

     if (skill != null&&skillText != null)
     {
         UpdateUI();
     }
 }
 private void UpdateUI()
 {
     if (skill == null)
     {
         print("skill is null");
     }
     if (icon == null)
     {
         print("No icon selected");
     }else if (icon.sprite == null)
     {
         print("icon sprite selected");
     }else if (skill.icon == null)
     {
         print("skillso icon selected");
     }
    icon.sprite = skill.icon;
    if (isactive)
    { skillbutton.interactable = true; 
      skillText.text = currentlevel.ToString()+"/"+skill.maxlevel.ToString();
      icon.color = Color.white;
    }
    else
    {   skillbutton.interactable = false;
        skillText.text = "Locked";
        icon.color = Color.grey;
    }
    
 }

 public void TryUpgradeSkill()
 {
     if (isactive && currentlevel < skill.maxlevel)
     {
         currentlevel++;
         OnAbilityPointSpent?.Invoke(this);
         if (currentlevel >= skill.maxlevel)
         {
             OnSkillMaxed?.Invoke(this);
         }
         UpdateUI();
         
     }
 }

 public  void Unlock()
 {
     isactive = true;
     UpdateUI();
 }
 public bool CanbeUnlocked()
 {
     foreach (SkillSlot slot in prerequisites)
     {
         if (!slot.isactive||slot.currentlevel < slot.skill.maxlevel)
         {
             return false;
         }
     }
     return true;
 }
}

