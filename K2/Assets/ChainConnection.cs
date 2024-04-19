using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class ChainConnection : MonoBehaviour
{
    public Transform dwarf;
    public Transform elf;
    public float maxDistance;
    // Start is called before the first frame update

    public float speed;
    private LineRenderer chain;
    public bool activateCollider;

    public CharacterController characterControllerElf;
    public CharacterController characterControllerDwarf;

    //for trying to pull
    public float pullforce = 10f;
    Rigidbody elfRb;
    void Start()
    {
        chain = GetComponent<LineRenderer>();
        elfRb = GetComponent<Rigidbody>();  

    }

    // Update is called once per frame
    void Update()
    {

        DistanceControl();
        //DrawChain();
        GenerateCollider();
        CreateSegments();
    }

    void DrawChain()
    {
        Vector3[] positions =
        {
            dwarf.position, elf.position + new Vector3(0,1,0),
        };
        chain.SetPositions(positions);

    }

    void DistanceControl()
    {
        float distance = Vector3.Distance(dwarf.position, elf.position);

        if (distance > maxDistance)
        {
            Vector3 direction = (dwarf.position - elf.position).normalized;

            Vector3 newPos = dwarf.position - direction * maxDistance;

            characterControllerElf.enabled = false;
            elf.position = newPos;
            characterControllerElf.enabled = true;
        }


        Debug.Log(((int)distance));
    }

    [SerializeField] int numberOfsegs = 10;
    void CreateSegments()
    {
        // Calculate the positions of the segments
        Vector3[] positions = new Vector3[numberOfsegs + 1];
        for (int i = 0; i <= numberOfsegs; i++)
        {
            float t = (float)i / numberOfsegs;
            positions[i] = Vector3.Lerp(dwarf.position, elf.position + new Vector3(0,1,0), t);
        }

        // Update the line renderer
        chain.positionCount = positions.Length;
        chain.SetPositions(positions);
    }


  void PullTowards()
    {
        if (elf != null)
        {
            // Calculate the direction from this player to the target player
            Vector3 directionToTarget = elf.position - transform.position;

            // Calculate the distance between the two players
            float distanceToTarget = directionToTarget.magnitude;

            // Check if players are too far apart
            if (distanceToTarget > 5f) // Adjust this value as needed
            {
                // Apply force to pull the player towards the target
                Vector3 pullForceVector = directionToTarget.normalized * pullforce;
                elfRb.AddForce(pullForceVector, ForceMode.Force);
            }
        }
    }

    void GenerateCollider()
    {
        if(activateCollider == true)
        {

            MeshCollider collider = GetComponent<MeshCollider>();

            if(collider == null)
            {
                collider = gameObject.AddComponent<MeshCollider>();
            }

            Mesh mesh = new Mesh();
            chain.BakeMesh(mesh, true);
            collider.sharedMesh = mesh;
            collider.convex = true;
        }
    }
}
