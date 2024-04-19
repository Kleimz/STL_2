using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakRock : MonoBehaviour
{
    public int RockHp = 100;
    [SerializeField] private GameObject brokenRockPre;
    [SerializeField] private float exploForce = 1000f;
    [SerializeField] private float exploRadius = 1000f;
    [SerializeField] private float fadeSpeed = 0.25f;
    [SerializeField] private float fadeDelay = 5f;
    [SerializeField] private float fadeSleepCheclDelay = 0.5f;
    [SerializeField] private Rigidbody rb;

    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Explode();
    }

    public void Explode()
    {
        if (rb != null)
        {
            Destroy(rb);
        }
        
        if(TryGetComponent<Collider>(out Collider collider))
        {
            collider.enabled = false;
        }
        if(TryGetComponent<Renderer>(out Renderer renderer))
        {
            renderer.enabled = false;
        }

        GameObject brokenInstance = Instantiate(brokenRockPre, transform.position, transform.rotation);

        Rigidbody[] rigidbodies = brokenInstance.GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody rbb in rigidbodies)
        {
            if(rb != null)
            {
                rbb.velocity = rb.velocity.normalized;
            }
            rbb.AddExplosionForce(exploForce, transform.position, exploRadius);
        }

        StartCoroutine(FadeOutPieces(rigidbodies));

    }

    private IEnumerator FadeOutPieces(Rigidbody[] rigidbodies)
    {
        WaitForSeconds wait = new WaitForSeconds(fadeSleepCheclDelay);
        int activePieces = rigidbodies.Length;
        
        while(activePieces > 0)
        {
            yield return wait;

            foreach (Rigidbody rbb in rigidbodies)
            {
                if (rbb.IsSleeping())
                {
                    activePieces--;
                }
            }
        }

        yield return new WaitForSeconds(fadeDelay);
        float time = 0;
        Renderer[] renderers = Array.ConvertAll(rigidbodies, GHetRendererFromRigdigbody);

        foreach(Rigidbody rbb in rigidbodies)
        {
            Destroy(rbb.GetComponent<Collider>());
            Destroy(rbb);
        }

        while (time < 1)
        {
            float step = Time.deltaTime * fadeSpeed;
            foreach (Renderer renderer1 in renderers)
            {
                renderer1.transform.Translate(Vector3.down * (step / renderer1.bounds.size.y), Space.World);
            }
            time += step;
            yield return null;
        }

        foreach(Renderer renderer in renderers)
        {
            Destroy(renderer.gameObject);
        }
        Destroy(gameObject);
   
    }

    private Renderer GHetRendererFromRigdigbody(Rigidbody rigidbody)
    {
        return rigidbody.GetComponent<Renderer>();

    }



    // Update is called once per frame
    void Update()
    {

    }


    /*

    public void SetBroken()
    {
        Instantiate(brokenRockPre, this.gameObject.transform.position, this.gameObject.transform.rotation);
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Untagged"))
        {
            RockHp =- 25;
            
            SetBroken();
        }
        
        Debug.Log("Hit");
    }
    */
}
