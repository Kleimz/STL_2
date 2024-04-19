using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TargetDummy : MonoBehaviour, Target
{
    public TargetTypes myType;
    [SerializeField] float health;
    [SerializeField] float attackRange;

    void Start()
    {
        myType = SetEnum(myType);
    }

    void Attack()
    {

    }
    void Die()
    {

        Destroy(gameObject);
    }

    public void Hit(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    public TargetTypes SetEnum(TargetTypes target)
    {
        return target;
    }
}
