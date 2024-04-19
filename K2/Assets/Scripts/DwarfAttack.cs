using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DwarfAttack : PlayerAttack
{
    public GameObject hitCenter;
    [SerializeField] float sphereRadius;
    [SerializeField] float maxDistance;
    [SerializeField] LayerMask targetLayer;
    public override void Attack()
    {
        if(cooldown <= 0)
        {
            print("Attack!");
            RaycastHit[] hits = Physics.SphereCastAll(hitCenter.transform.position, sphereRadius, hitCenter.transform.forward, maxDistance, targetLayer, QueryTriggerInteraction.UseGlobal);
            foreach (RaycastHit hit in hits)
            {
                Collider collider = hit.collider;
                if(collider.GetComponent<Target>() != null)
                {
                    collider.GetComponent<Target>().Hit(damage);
                }
            }
            cooldown = attackSpeed;
        }
       
    }
    private void Update()
    {
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
    }
}
