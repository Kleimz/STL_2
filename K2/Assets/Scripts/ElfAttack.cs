using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElfAttack : PlayerAttack
{
    [SerializeField] GameObject projectile;
    [SerializeField] float projectileSpeed;
    public override void Attack()
    {
        if(cooldown <= 0)
        {
            GameObject spawnedProjectile = Instantiate(projectile, transform.position, transform.rotation);
            spawnedProjectile.GetComponent<Rigidbody>().AddForce(transform.forward * projectileSpeed); 
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
