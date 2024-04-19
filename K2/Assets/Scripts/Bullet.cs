using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float damage = 10;
    [SerializeField] float destroyTimer = 2;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("SelfDestruct", destroyTimer);
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject targetObject = collision.collider.gameObject; 
        if (targetObject.CompareTag("Enemy"))
        {
            targetObject.GetComponent<Target>().Hit(damage);
        }
        Destroy(gameObject);
    }

    void SelfDestruct()
    {
        Destroy(gameObject);
    }
}
