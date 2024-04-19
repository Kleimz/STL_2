using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;


public enum EnemyState
{
    Chase,
    Attack, 
    Patrol,
    Follow,
    Setup
}
[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : Agent<EnemyState>, Target
{
    public TargetTypes myType;

    [SerializeField] float health;
    [SerializeField] float attackRange;
    [SerializeField] float attackSpeed;
    float cooldown;
    GameObject[] players;
    GameObject target;
    Animator animator;

    [SerializeField] GameObject[] patrolPositions;
    int patrolIndext;
    GameObject patrolTarget;

    [SerializeField]
    EnemyState startType;

    GameObject invisWall;

    MeleeWeapon weapon;
    [SerializeField] AnimationClip attackAnimation;

    bool patroling = false;
    bool chasing = false;

    [SerializeField] float runSpeed;
    [SerializeField] float walkSpeed;

    Vector3 prevPosition;
    void Start()
    {
        nma = GetComponent<NavMeshAgent>();
        myType = SetEnum(myType);
        players = GameObject.FindGameObjectsWithTag("Player");
        animator = gameObject.GetComponent<Animator>();
        weapon = GetComponentInChildren<MeleeWeapon>();
        patrolTarget = patrolPositions[0];  
    }

    private void FixedUpdate()
    {
        FiniteStateMachine();
    }

    void Attack()
    {
        animator.SetTrigger("Attack");
        state = EnemyState.Chase;
        weapon.attacking = true;
        cooldown = 1;
        Invoke("StopAttack", attackAnimation.averageDuration);
    }

    void StopAttack()
    {
        weapon.attacking = false;
        cooldown = 0;
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

    public void ChangeState(EnemyState newState)
    {
        state = newState;
    }

    protected override void FiniteStateMachine()
    {
        switch (state)
        {
            case EnemyState.Setup:
                Setup();
                break;
            case EnemyState.Attack :
                Attack();
                break;
            case EnemyState.Chase :
                if(!chasing)
                    StartCoroutine(Chase());
                break;
            case EnemyState.Follow:
                Follow();
                break;
            case EnemyState.Patrol :
                if(!patroling)
                    StartCoroutine(Patrol());
                break;
        }
            
            
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override bool Equals(object other)
    {
        return base.Equals(other);
    }

    public override string ToString()
    {
        return base.ToString();
    }

    public IEnumerator Chase()
    {
        chasing = true;
        while(chasing)
        {
            animator.SetFloat("Speed", 1);
            animator.SetBool("Moving", true);
            nma.speed = runSpeed;
            float distance = 10000;
            foreach (GameObject player in players)
            {
                float newDist = Vector3.Distance(gameObject.transform.position, player.transform.position);
                if (newDist < distance)
                {
                    distance = newDist;
                    target = player;
                }
            }
            MoveTo(target.transform.position);
            if (Vector3.Distance(gameObject.transform.position, target.transform.position) < attackRange && cooldown <= 0)
            {
                state = EnemyState.Attack;
            }
            yield return null;
        }
        
    }

     bool AtDestination(GameObject targetPosition)
    {
        bool atDestination = false;
        
        float distance = Vector3.Distance(gameObject.transform.position, patrolTarget.transform.position);
        print(distance);
        if(distance < 0.5)
        {
            atDestination = true;   
        }
        
        return atDestination;
    }

    IEnumerator Patrol()
    {
        patroling = true;
        while(patroling)
        {
            patrolTarget = patrolPositions[patrolIndext];
            animator.SetBool("Moving", true);
            animator.SetFloat("Speed", -1);
            if (Vector3.Distance(gameObject.transform.position, patrolTarget.transform.position) > 1)
            MoveTo(patrolTarget.transform.position);
            bool arived = AtDestination(patrolTarget);
            yield return new WaitUntil(() => AtDestination(patrolTarget) == true);
            arived = false;
            animator.SetBool("Moving", false);
            yield return new WaitForSeconds(UnityEngine.Random.Range(6, 10));
            if(patrolIndext < patrolPositions.Length - 1)
            {
                patrolIndext += 1;
            }
            else
            {
                patrolIndext = 0;
            }
            yield return null;
        }
       
    }
    public void Follow()
    {
        MoveTo(invisWall.transform.position);
        if (AtDestination(invisWall))
        {
            animator.SetBool("Moving", false);
        }
        else
            animator.SetBool("Moving", true);
    }
    public void Setup()
    {
        state = startType;
    }
}
