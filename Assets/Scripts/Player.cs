using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] PlayerInput input;
    [SerializeField] AgentMover movement;
    [SerializeField] AgentAnimation agentAnimation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        input.OnMouseClick += movement.SetDestination;
        movement.OnSpeedChange += agentAnimation.SetSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
