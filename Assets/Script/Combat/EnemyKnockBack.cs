using UnityEngine;

public class EnemyKnockBack : MonoBehaviour
{
    private Rigidbody2D rb;
    private Enemy_Movement enemyMovement;
    private Coroutine _stunCoroutine;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyMovement = GetComponent<Enemy_Movement>();
    }

    public void KnockBack(Transform attacker, float force, float stunTime)
    {
        Vector2 knockbackDirection = (transform.position - attacker.position).normalized;
        enemyMovement.SetEnemyState(EnemyState.knockback);
        rb.linearVelocity = knockbackDirection * force;

        if (_stunCoroutine != null)
            StopCoroutine(_stunCoroutine);
        _stunCoroutine = StartCoroutine(Stun(stunTime));
    }

    private System.Collections.IEnumerator Stun(float stunTime)
    {
        yield return new WaitForSeconds(stunTime);
        enemyMovement.SetEnemyState(EnemyState.Idle);
        _stunCoroutine = null;
    }
}
