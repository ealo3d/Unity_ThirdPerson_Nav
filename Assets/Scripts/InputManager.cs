using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerControls playerControls; //Para almacenar una instancia de la clase generada por el nuevo Input System.
    public Vector2 movementInput; //Para almacenar la entrada de movimiento en dos ejes (X y Y, como un joystick).
    public float verticalInput; //Para almacenar el valor individual del eje vertical (hacia adelante/atrás).
    public float horizontalInput; //Para almacenar el valor individual del eje horizontal (izquierda/derecha).

    #region FASE 2
    /*
    AnimatorManager animatorManager; // Variable para controlar las animaciones del jugador.
    PlayerMovement playerMovement; // Referencia al script de movimiento del jugador.
    public float moveAmount; // Variable para almacenar la magnitud total del movimiento (usada típicamente para mezclar animaciones).
    public bool shiftInput; // Bandera para saber si el botón de correr (Shift) está presionado.
    */
    #endregion

    private void Awake()
    {
        #region FASE 2
        /*
        // Busca el componente AnimatorManager en los objetos hijos de este GameObject.
        animatorManager = GetComponentInChildren<AnimatorManager>();
        // Obtiene el componente PlayerMovement asociado a este mismo GameObject.
        playerMovement = GetComponent<PlayerMovement>();
        */
        #endregion
    }

    private void OnEnable() // Método llamado cuando el objeto o componente se activa en la escena.
    {
        if (playerControls == null) // Comprueba si la instancia de playerControls aún no ha sido creada.
        {
            playerControls = new PlayerControls(); // Crea una nueva instancia de los controles generados.

            // Se suscribe al evento 'performed' de la acción Movement. Cuando ocurre, lee el valor como un Vector2 y lo guarda en movementInput.
            playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();

            #region FASE 2
            /*
            // Cuando se presiona la acción Shift, shiftInput se vuelve true.
            playerControls.PlayerActions.Shift.performed += i => shiftInput = true; 

            // Cuando se suelta la acción Shift, shiftInput vuelve a ser false.
            playerControls.PlayerActions.Shift.canceled += i => shiftInput = false; 
            */
            #endregion
        }
        playerControls.Enable(); // Habilita los controles para que comiencen a escuchar y procesar las entradas del jugador.
    }

    // Método llamado al desactivar el objeto. Aquí se deshabilitan los controles para ahorrar recursos.
    private void OnDisable() { playerControls.Disable(); }

    public void HandleAllInputs() // Método público que agrupa todas las funciones de entrada.
    {
        HandleMovementInput(); // Llama a la función que procesa la lógica de la entrada de movimiento.
        #region FASE 2 
        /* HandleRunningInput(); */
        #endregion
    }

    private void HandleMovementInput() // Función que se encarga de desglosar y procesar la entrada de movimiento recibida.
    {
        verticalInput = movementInput.y; // Extrae y asigna el valor del eje Y del vector a la variable verticalInput.
        horizontalInput = movementInput.x; // Extrae y asigna el valor del eje X del vector a la variable horizontalInput.

        #region FASE 2
        /*
        // Calcula la cantidad total de movimiento (moveAmount) limitando el valor entre 0 y 1, sumando los valores absolutos de los ejes.
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        // Llama al método para actualizar los parámetros del Animator pasándole la información de movimiento y si el jugador está corriendo.
        animatorManager.UpdateAnimatorValues(0, moveAmount, playerMovement.isRunning);
        */
        #endregion
    }

    #region FASE 2
    /*
    private void HandleRunningInput()
    {
        // Si el botón Shift está presionado y el jugador se está moviendo lo suficiente (moveAmount > 0.5f), entonces activa el estado de correr.
        if (shiftInput && moveAmount > 0.5f) { playerMovement.isRunning = true; }
        else { playerMovement.isRunning = false; } // De lo contrario, desactiva el estado de correr.
    }
    */
    #endregion
}