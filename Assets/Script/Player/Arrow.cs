using System;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Rigidbody2D _rb;
    public Animator _animator;
    public Vector2 _derection=Vector2.right;
    public float _lifespan=5;
    public float _speed;
    public LayerMask enemyLayerMask;
    public LayerMask obstacleLayerMask;
    public int damage;
    public float knockbackForce;
    public float knockbacktime;
    public float stunTime;
    //-----------------------------------------------
    public SpriteRenderer sr;
    public Sprite buriedSpritr;
    void Start()
    {   
        _rb.linearVelocity=_derection*_speed;
        RotateArrow();
        Destroy(gameObject,_lifespan);
    }

    private void RotateArrow()
    {
        float angle=Mathf.Atan2(_derection.y,_derection.x)*Mathf.Rad2Deg;
        transform.rotation=Quaternion.Euler(new Vector3(0,0,angle));
    }

    private void OnCollisionEnter2D(Collision2D other)
    {  
        if ((enemyLayerMask.value & (1 << other.gameObject.layer)) >0)
        {
            other.gameObject.GetComponent<Enemy_Health>().ChangeHealth(-damage);
            other.gameObject.GetComponent<EnemyKnockBack>().KnockBack(transform, knockbackForce, stunTime);
            AttachToTarget(other.gameObject.transform);
           
        }else if((obstacleLayerMask.value & (1 << other.gameObject.layer))>0)
        {
            AttachToTarget(other.gameObject.transform);
        }
    }

    private void AttachToTarget(Transform target)
    {
        sr.sprite = buriedSpritr;
        _rb.linearVelocity=Vector2.zero;
        _rb.bodyType = RigidbodyType2D.Kinematic;
        transform.SetParent(target);
        this.gameObject.GetComponent<BoxCollider2D>().isTrigger=true;
    }
}
