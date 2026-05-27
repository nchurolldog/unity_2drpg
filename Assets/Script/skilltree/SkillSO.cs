using System;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(menuName =  "SkillTree/Skill", fileName = "NewSkill")]
public class SkillSO : ScriptableObject
{
   public string skillName;
   public int maxlevel;
   public Sprite icon;
}
