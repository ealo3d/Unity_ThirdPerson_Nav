using UnityEngine;


public class PlayerManager : MonoBehaviour
{
    // Variable para almacenar una referencia al script InputManager (que lee los botones).
    InputManager inputManager;
    // Variable para almacenar una referencia al script PlayerMovement (que mueve al jugador).
    PlayerMovement playerMovement;
    
    #region FASE 3
    /*
    // Referencia al componente Animator que controla la máquina de estados de animación del modelo 3D.
    Animator animator;
    // Indica si el jugador está realizando una acción bloqueante (como atacar, esquivar o una caída fuerte).
    public bool isInteracting;
    */
    #endregion

    // Método que se ejecuta justo cuando el objeto cobra vida.
    private void Awake()
    {
        // Obtiene el script InputManager adjunto a este mismo GameObject.
        inputManager = GetComponent<InputManager>();
        // Obtiene el script PlayerMovement adjunto a este mismo GameObject.
        playerMovement = GetComponent<PlayerMovement>();
                
        #region FASE 3
        // Obtiene el componente Animator que se encuentra en uno de los GameObjects "hijos" (el modelo 3D).
        /* animator = GetComponentInChildren<Animator>(); */
        #endregion
    }
    
    private void Update() 
    { 
        inputManager.HandleAllInputs(); 
    }
    
    // FixedUpdate corre en sincronía con el motor de físicas. 
    private void FixedUpdate() 
    { 
        playerMovement.HandleAllMovement(); 
    }

    
    #region FASE 3
    /*
    // LateUpdate se ejecuta al final del frame, después de Update y FixedUpdate. Es el momento perfecto para leer estados de la animación.
    private void LateUpdate() 
    { 
        // Lee el parámetro "isInteracting" directamente desde el Animator y lo guarda en nuestra variable.
        isInteracting = animator.GetBool("isInteracting"); 
    }
    */
    #endregion
}