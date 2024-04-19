using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    float maxHp;
    float hp;
    Animator animator;
    private void Start()
    {
        hp = maxHp;
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(float damageCount)
    {
        animator.SetTrigger("TakeDamage");
        hp -= damageCount;
        if(hp <= 0 )
        {
            Die();
        }
    }

    void Die()
    {

    }
}
