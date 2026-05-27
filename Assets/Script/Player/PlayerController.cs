using UnityEngine;
using UnityEngine.InputSystem; 
using System;
using System.Collections;
public class PlayerController : MonoBehaviour
{   private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private Rigidbody2D _rigidbody2D;

    [SerializeField] private InputActionReference moveActionref;
    [SerializeField] private InputActionReference attackActionref;
    public bool isShooting;
    public Vector2 _moveinput {get; set; }

    private Vector2 _velocity;
    private InputAction _inputAction;
    private bool _isKnockback;
    private InputAction _attackInputAction;
    [SerializeField]
    private MeleeAttack _meleeAttack;
    private IAttackProvider _currentAttackProvider;
    public Action<InputAction.CallbackContext>  onPerformed;
    
    public Action<InputAction.CallbackContext>  onCanceled;

    public Action<InputAction.CallbackContext>  onAttackPerformed;

    private float Speed => StatsManager.Instance != null ? StatsManager.Instance.speed : 5f;

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _inputAction = moveActionref.action;
        _attackInputAction = attackActionref.action;
        _inputAction.Enable();
        _attackInputAction.Enable();
        _currentAttackProvider = _meleeAttack;
        onPerformed = context => _moveinput = context.ReadValue<Vector2>();
        onCanceled = context => _moveinput = Vector2.zero;
        onAttackPerformed = context => _currentAttackProvider?.Attack();
        _inputAction.performed += onPerformed;
        _inputAction.canceled += onCanceled;
        _attackInputAction.performed += onAttackPerformed;

    }

    // Update is called once per frame
    void Update()
    {
        if (_isKnockback)
            return;

        if (_moveinput != Vector2.zero)
        {
            _animator.SetFloat("vel", Math.Abs(_moveinput.magnitude));
            transform.localScale = new Vector3(_moveinput.x < 0 ? -1 : 1, 1, 1);
            _velocity = _moveinput.normalized * Speed;
            if (isShooting)
            {
                _velocity = Vector2.zero;
            }
        }
        else
        {
            _animator.SetFloat("vel", 0);
            _velocity = Vector2.zero;
        }
        if (_attackInputAction.triggered)
        {
            _currentAttackProvider?.Attack();
        }
    }
    void FixedUpdate()
    {
    
       if (!_isKnockback)
       {
           _rigidbody2D.linearVelocity = _velocity;
       }
    }
    void LateUpdate()
    {
        
    }
    void OnDestroy()
    {
        _inputAction.performed -= onPerformed;
        _inputAction.canceled -= onCanceled;
        _attackInputAction.performed -= onAttackPerformed;
    }
   

    public void SetAttackProvider(IAttackProvider provider)
    {
        _currentAttackProvider = provider;
    }

    public void Knockback(Transform enemy, float force, float stunTime)
    {
      _isKnockback = true;
        Vector2 knockbackDirection = (transform.position - enemy.position).normalized;
        _rigidbody2D.linearVelocity = knockbackDirection * force;
        StartCoroutine(KnockbackCoroutine(stunTime));
    }
    IEnumerator KnockbackCoroutine(float stunTime)
    {
        yield return new WaitForSeconds(stunTime);
        _rigidbody2D.linearVelocity = Vector2.zero;
        _isKnockback = false;
    }



}
