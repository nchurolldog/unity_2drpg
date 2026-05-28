using UnityEngine;
using System.Collections;
namespace Script.inventoryandshop
{
    public class UseItem : MonoBehaviour
    {
        public void ApplyEffects(ItemSO itemSo)
        {
            if (itemSo.currentHealth > 0)
            {
                StatsManager.Instance.UpdateCurHealth(itemSo.currentHealth);
            }
            if (itemSo.speed > 0)
            {
                StatsManager.Instance.UpdateSpeed(itemSo.speed);
            }

            if (itemSo.maxHealth > 0)
            {
                StatsManager.Instance.UpdateMaxHealth(itemSo.maxHealth);
            }
            if (itemSo.damage > 0)
            {
                StatsManager.Instance.damage += itemSo.damage;
            }
            if (itemSo.duration > 0)
            {
                StartCoroutine(Effectimer(itemSo));
            }
        }
        IEnumerator Effectimer(ItemSO itemSo)
        {
            yield return new WaitForSeconds(itemSo.duration);
            if (itemSo.currentHealth > 0)
            {
                StatsManager.Instance.UpdateCurHealth(-itemSo.currentHealth);
            }
            if (itemSo.speed > 0)
            {
                StatsManager.Instance.UpdateSpeed(-itemSo.speed);
            }

            if (itemSo.maxHealth > 0)
            {
                StatsManager.Instance.UpdateMaxHealth(-itemSo.maxHealth);
            }
            if (itemSo.damage > 0)
            {
                StatsManager.Instance.damage -= itemSo.damage;

            }
        }
    }
   
}
