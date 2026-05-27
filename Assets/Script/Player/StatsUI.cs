using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class State : MonoBehaviour
{
    public GameObject[] statsSlots;
    public CanvasGroup statsCanvas;
    [SerializeField] InputActionReference uiActionReference;

    private bool isopen = false;

    private void Start()
    {
        // 初始化 UI 状态
        UpdateAllStats();
        UpdateUIState();
    }

    private void OnEnable()
    {
        // 订阅 Performed 事件（按键按下的那一刻）
        uiActionReference.action.performed += ToggleUI;
        uiActionReference.action.Enable();
    }

    private void OnDisable()
    {
        // 取消订阅，防止残留
        uiActionReference.action.performed -= ToggleUI;
        uiActionReference.action.Disable();
    }

    private void ToggleUI(InputAction.CallbackContext context)
    {
        // 状态取反：开变关，关变开
        isopen = !isopen;
        UpdateUIState();

        if (isopen)
        {
            UpdateAllStats();
        }
    }

    private void UpdateUIState()
    {
        // 控制 CanvasGroup 显示隐藏
        statsCanvas.alpha = isopen ? 1 : 0;
        statsCanvas.interactable = isopen;
        statsCanvas.blocksRaycasts = isopen;

       
        Time.timeScale = isopen ? 0 : 1; 
    }

    public void UpdateDamage()
    {
        statsSlots[0].GetComponentInChildren<TMP_Text>().text = "Damage: " + StatsManager.Instance.damage;
    }

    public void UpdateSpeed()
    {
        statsSlots[1].GetComponentInChildren<TMP_Text>().text = "Speed: " + StatsManager.Instance.speed;
    }

    public void UpdateAllStats()
    {
        UpdateDamage();
        UpdateSpeed();
    }
}