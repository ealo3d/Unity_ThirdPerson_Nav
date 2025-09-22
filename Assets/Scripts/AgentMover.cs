using System;
using UnityEngine;
using UnityEngine.AI;

public class AgentMover : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    public event Action<float> OnSpeedChange;
    
    // Update is called once per frame
    void Update()
    {
        OnSpeedChange?.Invoke(Mathf.Clamp01(agent.velocity.magnitude / agent.speed));
    }

    public void SetDestination(Vector3 destination)
    {
        agent.destination = destination;
    }
}
