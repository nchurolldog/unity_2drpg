using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExpManager : MonoBehaviour
{
    public Slider expSlider;
    public TMP_Text expText;
    public int level = 1; // 建议从1级开始
    public int currentExp;
    public int expToNextLevel = 10;
    public float expGrowthRate = 1.2f;
    public static event Action<int> OnLevelUp;
   private void OnEnable()
   {
       Enemy_Health.OnMonsterDefeated +=GainExp;
   }

   private void OnDisable()
   {
       Enemy_Health.OnMonsterDefeated -=GainExp;
   }

   private void Awake()
    {
        UpdateUI();
    }



    public void GainExp(int amount)
    {
        currentExp += amount;
        // 使用 while 处理连升多级
        while (currentExp >= expToNextLevel)
        {
            LevelUp();
        } 
        UpdateUI();
    }

    public void LevelUp()
    {
        level++;
        OnLevelUp?.Invoke(1);
        currentExp -= expToNextLevel;
       
        expToNextLevel = Mathf.RoundToInt(expToNextLevel * expGrowthRate);
    }

    public void UpdateUI()
    {
       
            expSlider.maxValue = expToNextLevel;
            expSlider.value = currentExp;
            // 这里改为显示 level 变量
            expText.text = "Level: " + level.ToString();
        
    }
}