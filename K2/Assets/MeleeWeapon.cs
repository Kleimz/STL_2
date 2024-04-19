using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    public bool attacking = false;
    [SerializeField] float damage;


    private void OnCollisionEnter(Collision collision)
    {
        if (attacking)
        {
            PlayerHealth health = collision.gameObject.GetComponent<PlayerHealth>();

            if (health != null)
            {
                health.TakeDamage(damage);
            }
        }
    }
}
