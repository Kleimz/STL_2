using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class InvisWall : MonoBehaviour
{
    [SerializeField]
    GameObject targetDestination;
    GameObject[] players;
    [SerializeField]
    float maxDistance;

    NavMeshAgent agent;

    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        agent.Move(targetDestination.transform.position);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Move())
        {
            agent.isStopped = false;
        }
        else
        {
            agent.isStopped = true;
        }
    }

    bool Move()
    {
        float distance = 10000;
        foreach (GameObject player in players)
        {
            float newDist = Vector3.Distance(gameObject.transform.position, player.transform.position);
            if (newDist < distance)
            {
                distance = newDist;
            }
        }
        if (distance > maxDistance)
        {
            return true;
        }
        return false;
    }
}
