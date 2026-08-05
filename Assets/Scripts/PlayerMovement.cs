using UnityEngine; 

public class PlayerMovement : MonoBehaviour 
{
    InputManager inputManager; // Referencia al script InputManager para leer las entradas del jugador.
    Vector3 moveDirection; // Almacena la dirección en la que el jugador se moverá en el espacio 3D.
    Transform cameraObject; // Referencia al Transform de la cámara principal para calcular el movimiento relativo a ella.
    Rigidbody playerRigidbody; // Referencia al componente Rigidbody del jugador para aplicar movimiento físico.
    public float movementSpeed = 7; // Velocidad base a la que se mueve el personaje.
    public float rotationSpeed = 15; // Velocidad a la que el personaje gira sobre sí mismo para encarar la dirección de movimiento.

    #region FASE 2
    /*
    public float walkingSpeed = 2.5f; // Velocidad para caminar.
    public float runningSpeed = 7f; // Velocidad para correr.
    public bool isRunning; //Para indicar si el personaje está corriendo.
    */
    #endregion

    #region FASE 3
    /*
    PlayerManager playerManager; // Referencia al script principal del jugador para revisar su estado.
    AnimatorManager animatorManager; // Referencia al script que controla las animaciones del jugador.
    
    [Header("Gravedad Avanzada")] // Título que se muestra en el Inspector de Unity para organizar estas variables.
    public float inAirTimer; // Temporizador para rastrear cuánto tiempo lleva el jugador cayendo.
    public float leapingVelocity = 3f; // Fuerza de impulso hacia adelante que se aplica levemente mientras cae.
    public float fallingVelocity = 100f; // Multiplicador de gravedad para forzar una caída más rápida.
    public float rayCastHeightOffset = 0.5f; // Altura desde donde inicia el rayo que detecta el suelo.
    public float maxDistance = 0.5f; // Distancia máxima que recorre el rayo hacia abajo para buscar el suelo.
    public LayerMask groundLayer; // Identifica qué capas del entorno son consideradas "suelo".
    public bool isGrounded; //Para saber si el personaje está tocando el suelo.
    */
    #endregion

    private void Awake() 
    {
        inputManager = GetComponent<InputManager>(); 
        playerRigidbody = GetComponent<Rigidbody>(); 
        cameraObject = Camera.main.transform; // Busca la cámara principal de la escena y asigna su transform a la variable cameraObject.

        #region FASE 3
        /*
        playerManager = GetComponent<PlayerManager>(); // Obtiene el PlayerManager.
        animatorManager = GetComponentInChildren<AnimatorManager>(); // Obtiene el AnimatorManager que está en un objeto hijo.
        isGrounded = true; // Por defecto el jugador se considera en el suelo al iniciar.
        */
        #endregion
    }

    public void HandleAllMovement() // Función principal que agrupa todo el manejo de movimiento (translación y rotación).
    {
        #region FASE 3
        /*
        HandleFallingAndLanding(); // Llama a la función que calcula si el jugador cae o aterriza.
        if (playerManager.isInteracting) return; // Si el jugador está bloqueado en una animación importante, interrumpe el movimiento.
        */
        #endregion

        HandleMovement(); // Llama a la función que se encarga del desplazamiento (translación).
        HandleRotation(); // Llama a la función que se encarga de que el personaje rote hacia donde camina.
    }

    private void HandleMovement() // Calcula y aplica la velocidad para mover al personaje.
    {
        // Calcula el movimiento adelante/atrás en base hacia donde mira la cámara.
        moveDirection = cameraObject.forward * inputManager.verticalInput; 

        // Le suma a esa dirección el movimiento lateral (izquierda/derecha) de la cámara.
        moveDirection += cameraObject.right * inputManager.horizontalInput; 

        // Fuerza el eje Y a 0 para que la dirección de movimiento no lo eleve o lo hunda en el suelo.
        moveDirection.y = 0; 

        // Normaliza el vector (magnitud de 1) para que caminar en diagonal no sea más rápido.
        moveDirection.Normalize(); 

        #region FASE 2
        /*
        // Si está corriendo, multiplica la dirección por la velocidad de correr.
        if (isRunning) { moveDirection *= runningSpeed; } 
        
        // Si no corre, la multiplica por la velocidad de caminar.
        else { moveDirection *= walkingSpeed; } 
        */
        #endregion

        // (NOTA CLASE: Borra esta línea cuando pases a la Fase 2 visualmente)
        moveDirection *= movementSpeed; // FASE 1: Aplica siempre la velocidad constante base a la dirección.

        // Guarda la dirección calculada (ya con velocidad) en un vector de velocidad final.
        Vector3 movementVelocity = moveDirection;

        // Sobrescribe la velocidad del Rigidbody con nuestra velocidad calculada para moverlo físicamente.
        playerRigidbody.linearVelocity = movementVelocity; 
    }

    private void HandleRotation() // Calcula y aplica la rotación para que el modelo mire hacia donde se dirige.
    {
        // Dirección objetivo frontal relativa a la cámara.
        Vector3 targetDirection = cameraObject.forward * inputManager.verticalInput;

        // Le suma la dirección objetivo lateral relativa a la cámara.
        targetDirection += cameraObject.right * inputManager.horizontalInput;

        // Evita que el modelo se incline hacia arriba o hacia abajo.
        targetDirection.y = 0;

        // Normaliza la dirección objetivo.
        targetDirection.Normalize(); 

        // Si no hay inputs, la dirección objetivo sigue siendo su dirección actual frontal (para no resetear su rotación).
        if (targetDirection == Vector3.zero) targetDirection = transform.forward; 

        // Calcula la rotación (Quaternion) necesaria para mirar hacia la 'targetDirection'.
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection); 
        
        // Interpola suavemente entre la rotación actual y la objetivo según la velocidad de rotación y el tiempo del frame.
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime); 

        // Aplica la rotación interpolada y suavizada al objeto del jugador.
        transform.rotation = playerRotation; 
    }

    #region FASE 3
    /*
    // Detecta si hay suelo y aplica animaciones y fuerzas de gravedad.
    private void HandleFallingAndLanding()
    {
        // Variable donde se guardará la información si el rayo (SphereCast) choca con algo.
        RaycastHit hit;
        // Establece el origen del rayo en la base del jugador.
        Vector3 rayCastOrigin = transform.position; 
        // Eleva el origen del rayo ligeramente para que empiece desde arriba del suelo y no falle al chocar consigo mismo.
        rayCastOrigin.y += rayCastHeightOffset; 

        // Si el jugador no está tocando el suelo.
        if (!isGrounded) 
        {
            // Y no está ocupado realizando una animación prioritaria.
            if (!playerManager.isInteracting) 
            {
                // Dispara la animación de caer.
                animatorManager.PlayerTargetAnimation("Falling", true); 
            }

            // Va sumando el tiempo que pasa en el aire frame a frame.
            inAirTimer += Time.deltaTime;
            
            // Aplica un pequeño empuje hacia adelante mientras está en el aire.
            playerRigidbody.AddForce(transform.forward * leapingVelocity); 
            
            // Aplica una fuerza artificial hacia abajo que es más fuerte cuanto más tiempo pase en el aire.
            playerRigidbody.AddForce(Vector3.down * fallingVelocity * inAirTimer); 
        }

        // Lanza una esfera invisible (SphereCast) hacia abajo. Retorna 'true' si golpea algún objeto de la capa 'groundLayer' dentro de la 'maxDistance'.
        if (Physics.SphereCast(rayCastOrigin, 0.1f, Vector3.down, out hit, maxDistance, groundLayer))
        {
            // Si no estaba en el suelo en el frame anterior, y está interactuando (es decir, reproduciendo la animación de caída).
            if (!isGrounded && playerManager.isInteracting) 
            {
                // Dispara la animación de aterrizaje al tocar suelo.
                animatorManager.PlayerTargetAnimation("Landing", true); 
            }
            
            // Como tocó suelo, resetea a 0 el tiempo de caída.
            inAirTimer = 0; 
            // Marca que ya está tocando el suelo seguro.
            isGrounded = true; 
            // Termina cualquier bloqueo temporal por caer.
            playerManager.isInteracting = false; 
        }
        else // Si el SphereCast no golpeó nada en absoluto (no hay suelo).
        {
            // Indica que ya no hay suelo debajo, por lo tanto empezará a caer en el próximo frame.
            isGrounded = false; 
        }
    }
    */
    #endregion
}