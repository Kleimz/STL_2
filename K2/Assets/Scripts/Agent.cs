using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Agent<CustomStateEnum> : MonoBehaviour
{
    public NavMeshAgent nma;
    Transform target;

    public CustomStateEnum state;

    protected abstract void FiniteStateMachine();

    public void MoveTo(Vector3 destination)
    {
        nma.SetDestination(destination);
    }
}
