using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerControls playerControls;
    AnimatorManager animatorManager;
    PlayerMovement playerMovement;

    public Vector2 movementInput;

    public float verticalInput;
    public float horizontalInput;
    public float moveAmount;

    public bool shiftInput;

    private void Awake()
    {
        animatorManager = GetComponent<AnimatorManager>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerControls();
            playerControls.PlayerMovement.HorizontalMovement.performed += i => movementInput = i.ReadValue<Vector2>();

            playerControls.PlayerActions.Shift.performed += i => shiftInput = true;
            playerControls.PlayerActions.Shift.canceled += i => shiftInput = false;
        }
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;

        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));

        animatorManager.UpdateAnimatorValue(0, moveAmount, playerMovement.isRunning);
    }

    public void HandleAllInputs ()
    {
        HandleMovementInput();
        HandleRunningInput();
    }

    private void HandleRunningInput()
    {
        if (shiftInput && moveAmount > 0.5f)
            playerMovement.isRunning = true;
        else
            playerMovement.isRunning = false;
    }
}
