using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerChangEquipment : MonoBehaviour
{
   public InputActionReference changeAction;
   public Player_Bow bow;
   public MeleeAttack meleeAttack;
   public PlayerController playerController;
   private float timer;
   public float changeCoolDown;

   private void OnEnable()
   {
      changeAction.action.performed += Toggle;
      changeAction.action.Enable();
   }

   private void OnDisable()
   {
      changeAction.action.performed -= Toggle;
      changeAction.action.Disable();
   }

   private void Update()
   {
      timer -= Time.deltaTime;
   }

   private void Toggle(InputAction.CallbackContext context)
   {
      if (timer <= 0)
      {
         bow.enabled = !bow.enabled;
         meleeAttack.enabled = !meleeAttack.enabled;
         playerController.SetAttackProvider(bow.enabled ? bow : meleeAttack);
         timer = changeCoolDown;
      }
   }
}
