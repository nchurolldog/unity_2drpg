using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class SkillManager : MonoBehaviour
{
    [SerializeField] InputActionReference uiActionReference;
    private bool isopen = false;
  public  CanvasGroup skillCanvas;
    public void OnEnable()
    {
        SkillSlot.OnAbilityPointSpent += HandleAbilityPointSpent;
        uiActionReference.action.performed += ToggleUI;
        uiActionReference.action.Enable();
    }

    public void OnDisable()
    {
        SkillSlot.OnAbilityPointSpent -= HandleAbilityPointSpent;
        uiActionReference.action.performed -= ToggleUI;
        uiActionReference.action.Disable();
    }

    public void ToggleUI(InputAction.CallbackContext context)
    {
        // 状态取反：开变关，关变开
        isopen = !isopen;
        UpdateUIState();

    }
    private void UpdateUIState()
    {
        // 控制 CanvasGroup 显示隐藏
        skillCanvas.alpha = isopen ? 1 : 0;
        skillCanvas.interactable = isopen;
        skillCanvas.blocksRaycasts = isopen;

       
        Time.timeScale = isopen ? 0 : 1; 
    }
    public void HandleAbilityPointSpent(SkillSlot skillSlot)
    {
        string skillName = skillSlot.skill.name;
        switch (skillName)
        {
            case "MaxHealthBoost":
            {
                StatsManager.Instance.updateMaxHealth(1);
            }
                break;
            default:
                print("unknow skill"+skillName);
                break;
        }
    }
}
