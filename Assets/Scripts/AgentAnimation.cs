using UnityEngine;

public class AgentAnimation : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] string movementSpeed = "MovementSpeed";

    public void SetSpeed(float speed)
    {
        animator.SetFloat(movementSpeed, speed);
    }
}
