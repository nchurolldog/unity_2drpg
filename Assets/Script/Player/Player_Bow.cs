using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class Player_Bow : MonoBehaviour, IAttackProvider
{
    [Header("References")]
    public Transform launchPoint;
    public GameObject arrowPrefab;
    public PlayerController player;
    Vector2 aimDirection=Vector2.right;
    [Header("Bow Settings")]
    [SerializeField] private float arrowSpeed = 15f;
    public Animator animator;
    
    public float shootCoolDown = 0.5f;
    private float timer;

    private void OnEnable()
    {
        animator.SetLayerWeight(0, 0);
        animator.SetLayerWeight(1, 1);
    }

    private void OnDisable()
    {
        animator.SetLayerWeight(0, 1);
        animator.SetLayerWeight(1, 0);
    }

    public void Attack()
    {
        animator.SetBool("isShooting", true);
        player.isShooting=true;
    }


    private void Update()
    {   timer-= Time.deltaTime;
        HandleAiming();
    }

    private void HandleAiming()
    { 
        Vector2 moveinput = player._moveinput;
        if (moveinput.x != 0 || moveinput.y != 0)
        {
            aimDirection = new Vector2(moveinput.x, moveinput.y).normalized;
            animator.SetFloat("aimx", aimDirection.x);
            animator.SetFloat("amiy", aimDirection.y);
        }
    }
    void Shoot()
    { if(timer<=0){
            Arrow arrow = Instantiate(arrowPrefab, launchPoint.position, Quaternion.identity).GetComponent<Arrow>();
            arrow._derection=aimDirection;
            timer = shootCoolDown;
        }
       animator.SetBool("isShooting",false);
       player.isShooting=false;
    }

}