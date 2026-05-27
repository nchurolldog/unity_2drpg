using UnityEngine;

public class Enemy_Movement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform player;
    public float detectionRange = 5f;
    
    public Transform detectionPoint;
    public float timer;

    public float cooldown = 2f;
    private Animator animator;
    private EnemyState enemyState;

    public float speed ;
    private Vector2 movement;

    public float attackRange = 1.2f;
    private int flipX = 1;//1 for right, -1 for left
    private bool isAttacking;
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        animator=GetComponent<Animator>();
        SetEnemyState(EnemyState.Idle);
    }

    // Update is called once per frame
    void Update()

    {
        if(enemyState != EnemyState.knockback)
        { 
            
        CheckforPlayer(); 
          if(timer>0)
        timer-=Time.deltaTime;
        Chase();}
        else
        {
            return;
        }
    }
    void Chase()
    {
        if (enemyState == EnemyState.chasing)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            movement = direction * speed;
            if ((direction.x > 0 && flipX < 0) || (direction.x < 0 && flipX > 0))
            {
                Flip();
            }
      
        }
        else
        {
            movement = Vector2.zero;
        }
    }
    
    void FixedUpdate()
    {     if(enemyState != EnemyState.knockback)
      {  rb.linearVelocity =movement;
      }
    }
    void CheckforPlayer()
    {
        if (enemyState == EnemyState.attackside && timer > 0) 
    {
        return; 
    }
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(detectionPoint.position, detectionRange, LayerMask.GetMask("Player"));
        if (hitPlayers.Length > 0)
        {
            player = hitPlayers[0].transform;

            if (Vector2.Distance(transform.position, player.position) <= attackRange && timer <= 0)
            {
                timer = cooldown;
                SetEnemyState(EnemyState.attackside);
            }
            else if (Vector2.Distance(transform.position, player.position) > attackRange)
            {
                SetEnemyState(EnemyState.chasing);
            }
        }
        else
        {
            SetEnemyState(EnemyState.Idle);
        }
    }

    public void SetEnemyState(EnemyState newState)//state machine for enemy animation
    {   switch (enemyState)
        {
            case EnemyState.Idle:
                animator.SetBool("isIdle", false);
                break;
            case EnemyState.chasing:
                animator.SetBool("isChasing", false);
                break;
            case EnemyState.die:
                animator.SetBool("isDead", false);
                break;
            case EnemyState.attackside:
                animator.SetBool("isAttackside", false);
                break;
            case EnemyState.attackdown:
                animator.SetBool("isAttackdown", false);
                break;
            case EnemyState.attackup:
                animator.SetBool("isAttackup", false);
                break;
        }
        enemyState = newState;
        switch (enemyState)
        {
            case EnemyState.Idle:
                animator.SetBool("isIdle", true);
                break;
            case EnemyState.chasing:
                animator.SetBool("isChasing", true);
                break;
            case EnemyState.die:
                animator.SetBool("isDead", true);
                break;
            case EnemyState.attackside:
                animator.SetBool("isAttackside", true);
                break;
            case EnemyState.attackdown:
                animator.SetBool("isAttackdown", true);
                break;
            case EnemyState.attackup:
                animator.SetBool("isAttackup", true);
                break;
        }
    }
    void Flip()
    {
        flipX *= -1;
        Vector3 localScale = transform.localScale;
        localScale.x = Mathf.Abs(localScale.x) * flipX;
        transform.localScale = localScale;
    }
  public  void DrawGizmosSelected()
    {
        if (detectionPoint == null)
            return;
        Gizmos.color = Color.cadetBlue;
        Gizmos.DrawWireSphere(detectionPoint.position, detectionRange);
    }
}
public enum EnemyState
{
    Idle,
    chasing,
    attackside,
    attackdown,
    attackup,
    knockback,
    die
}