using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Requerido para usar el nuevo sistema
using UnityEngine.InputSystem; 

public class InputManager : MonoBehaviour
{
    // --- REFERENCIAS A OTROS SCRIPTS ---
    [SerializeField] Movement movement;
    
    #region 3. Referencia MouseLook (Descomentar en la Fase 3)
    /*
    [SerializeField] MouseLook mouseLook;
    */
    #endregion

    // --- VARIABLES DEL SISTEMA DE INPUT ---
    PlayerControls controls; 
    PlayerControls.GroundMovementActions groundMovement; 

    // --- ALMACENAMIENTO DE DATOS ---
    Vector2 horizontalInput; 

    #region 3. Input del Ratón (Descomentar en la Fase 3)
    /*
    Vector2 mouseInput;
    */
    #endregion

    private void Awake()
    {
        controls = new PlayerControls(); 
        groundMovement = controls.GroundMovement; 

        // 1. Fase Base: Movimiento Horizontal
        // Captura el valor WASD y lo guarda en la variable
        groundMovement.HorizontalMovement.performed += ctx => horizontalInput = ctx.ReadValue<Vector2>();

        #region 2. Evento de Salto (Descomentar en la Fase 2)
        /*
        // Escucha la barra espaciadora y avisa al script Movement
        groundMovement.Jump.performed += x => movement.OnJumpPressed();
        */
        #endregion

        #region 3. Eventos del Ratón (Descomentar en la Fase 3)
        /*
        // Captura el movimiento del ratón en X e Y
        groundMovement.MouseX.performed += ctx => mouseInput.x = ctx.ReadValue<float>();
        groundMovement.MouseY.performed += ctx => mouseInput.y = ctx.ReadValue<float>();
        */
        #endregion
    }

    private void Update()
    {
        // Pasa constantemente el valor WASD al script de movimiento
        movement.ReceiveInput(horizontalInput);

        #region 3. Actualizar Ratón (Descomentar en la Fase 3)
        /*
        // Pasa los valores del ratón al script MouseLook
        mouseLook.ReceiveInput(mouseInput);
        */
        #endregion
    }

    // Activar y desactivar los controles es obligatorio en el nuevo sistema
    private void OnEnable() { controls.Enable(); }
    private void OnDestroy() { controls.Disable(); }
}