using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public abstract class PlayerAttack : MonoBehaviour
{

    [SerializeField] public float damage = 10;
    [SerializeField] public float attackSpeed = 0.5f;
    [SerializeField] public float cooldown = 0;
    PlayerMovement controller;
    public void OnAttack(InputAction.CallbackContext context)
    {
        controller = GetComponent<PlayerMovement>();
        if(cooldown <= 0 && context.started && controller.groundedPlayer)
        {
            GetComponent<Animator>().SetTrigger("Attack");
            Attack();
            cooldown = attackSpeed;
        }
       
    }
    public abstract void Attack();
}
