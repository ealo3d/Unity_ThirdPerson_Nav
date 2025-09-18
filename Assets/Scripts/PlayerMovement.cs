using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    InputManager inputManager;
    PlayerManager playerManager;
    Vector3 moveDirection;
    Transform cameraObject;
    Rigidbody playerRigidbody;
    AnimatorManager animatorManager;

    public float inAirTime;
    public float leapingVelocity;
    public float fallingVelocity;
    public float rayCastHeightOffset = 0.5f;
    public LayerMask groundLayer;
    public float maxDistance = 1f;
    public bool isGrounded;

    public float walkingSpeed = 2.5f;
    public float runningSpeed = 5.0f;
    public float rotationSpeed = 15f;
    public bool isRunning; 

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        playerRigidbody = GetComponent<Rigidbody>();
        playerManager = GetComponent<PlayerManager>();
        animatorManager = GetComponent<AnimatorManager>();
        cameraObject = Camera.main.transform;
                
    }
    private void HandleMovement()
    {
        moveDirection = cameraObject.forward * inputManager.verticalInput;
        moveDirection = moveDirection + cameraObject.right * inputManager.horizontalInput;
        moveDirection.y = 0;

        moveDirection.Normalize();

        if (isRunning ) 
            moveDirection = moveDirection * runningSpeed;
        else
            moveDirection = moveDirection * walkingSpeed;
       

        Vector3 movementVelocity = moveDirection;
        playerRigidbody.linearVelocity = movementVelocity;

    }

    private void HandleRotation()
    {
        Vector3 targetDirection = Vector3.zero;

        targetDirection = cameraObject.forward * inputManager.verticalInput;
        targetDirection = moveDirection + cameraObject.right * inputManager.horizontalInput;
        targetDirection.y = 0;

        targetDirection.Normalize();

        if (targetDirection == Vector3.zero)
            targetDirection = transform.forward;

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.rotation = playerRotation;


    }

    public void HandleAllMovement()
    {
        HandleFallingAndLanding(); //llamar la función

        if (playerManager.isInteracting)
            return;
        HandleMovement();
        HandleRotation();
    }

    public void HandleFallingAndLanding()
    {
        RaycastHit hit;
        Vector3 rayCastOrigin = transform.position;
        rayCastOrigin.y = rayCastOrigin.y + rayCastHeightOffset;

        if (!isGrounded)
        {
            if(!playerManager.isInteracting)
            {
                animatorManager.PlayerTargetAnimation("Falling", true);
            }
            inAirTime += Time.deltaTime;
            playerRigidbody.AddForce(transform.forward * leapingVelocity);
            playerRigidbody.AddForce(Vector3.down * fallingVelocity * inAirTime);
        }

        if(Physics.SphereCast(rayCastOrigin, 0.1f,Vector3.down, out hit, maxDistance, groundLayer))
        {
            if(!isGrounded && playerManager.isInteracting)
            {
                animatorManager.PlayerTargetAnimation("Landing", true);
            }
            inAirTime = 0;
            isGrounded = true;
            playerManager.isInteracting = false;

        }
        else
        {
            isGrounded = false;
        }


    }

   
}
