using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerControls playercontrols;
    public Vector2 movementInput;

    public float verticalInput;
    public float horizontalInput;

    private void OnEnable()
    {
        if (playercontrols == null)
        {
            playercontrols = new PlayerControls();
            playercontrols.PlayerMovement.HorizontalMovement.performed += i => movementInput = i.ReadValue<Vector2>();

        }
        playercontrols.Enable();
    }
    private void OnDisable()
    {
        playercontrols.Disable();
    }

    private void HandleMovementInput()

    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;

        
    }
    private void HandleAllInputs()
    {
        HandleMovementInput();
        //JumpingInput();
        //any other function we need!
    }






    
}
