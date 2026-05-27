using UnityEngine;

public class EnemyCombat : MonoBehaviour
{   public int damage = 1;
    public float attackRange = 1.2f;    
    public float attackforce = 5f;
    public float attackStunTime = 1f;
    public Transform attackPoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

   public void Attack()
    {
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, LayerMask.GetMask("Player"));
        foreach (Collider2D player in hitPlayers)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.ChangeHealth(-damage);
            }
            PlayerController playerController = player.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.Knockback(transform, attackforce, attackStunTime);
            }
        }
    }//这个方法只会在播放动画的时候调用
    public void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
