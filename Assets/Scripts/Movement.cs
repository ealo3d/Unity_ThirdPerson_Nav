using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] CharacterController controller; //Para referenciar el Character Controller
    [SerializeField] float speed = 11f; //Velocidad del personaje
    Vector2 horizontalInput; //Guardar el input horizontal

    #region 2. Variables de Gravedad (Descomentar en la Fase 2)
    /*
    [SerializeField] float gravity = -30f; //este valor debe ajustarse dependiendo de la escala de la escena
    Vector3 verticalVelocity = Vector3.zero; // velocidad vertical inicializada en cero
    [SerializeField] LayerMask groundMask; 
    bool isGrounded;
    */
    #endregion

    #region 3. Variables de Salto (Descomentar en la Fase 3)
    /*
    [SerializeField] float jumpHeight = 3.5f;
    bool jump;
    */
    #endregion

    #region 4. Variables de Animación (Descomentar en la Fase 4)
    /*
    [SerializeField] Animator anim;
    private int velocityHash;

    private void Start()
    {
        // Optimizamos el string del parámetro para mayor rendimiento
        velocityHash = Animator.StringToHash("PlayerVelocity");
    }
    */
    #endregion

    private void Update()
    {
        #region 2. Lógica de Gravedad - Suelo (Descomentar en la Fase 2)
        /*
        // Crea una esfera invisible para detectar si tocamos la capa "Ground"
        isGrounded = Physics.CheckSphere(transform.position, 0.1f, groundMask);

        // Si tocamos el suelo, detenemos la acumulación de gravedad
        if (isGrounded) 
        {
            verticalVelocity.y = 0f; 
        }
        */
        #endregion

        // 1. Movimiento Base (Horizontal)
        // Calcula la dirección basándose en hacia dónde mira el jugador
        Vector3 horizontalVelocity = (transform.right * horizontalInput.x + transform.forward * horizontalInput.y) * speed;

        //Pasar al metodo Move de CharacterController el valor de horizontalVelocity * deltatime (independiente del framerate)
        controller.Move(horizontalVelocity * Time.deltaTime);

        #region 4. Lógica de Animación (Descomentar en la Fase 4)
        /*
        // Tomamos la velocidad física real (ignorando caídas)
        Vector3 playerRealSpeed = new Vector3(controller.velocity.x, 0f, controller.velocity.z);
        // La convertimos a un valor de 0 a 1 para el Blend Tree
        float animationSpeed = playerRealSpeed.magnitude / speed;
        // Transición suave de 0.1 segundos
        anim.SetFloat(velocityHash, animationSpeed, 0.1f, Time.deltaTime);
        */
        #endregion

        #region 3. Lógica de Salto (Descomentar en la Fase 3)
        /*
        // El salto se calcula ANTES de aplicar la gravedad
        if (jump) 
        {
            if(isGrounded) 
            {
                 // Fórmula física real para la altura del salto
                 verticalVelocity.y = Mathf.Sqrt(-2f * jumpHeight * gravity); 
            }
            jump = false; 
        }
        */
        #endregion
        
        #region 2. Lógica de Gravedad - Caída (Descomentar en la Fase 2)
        /*
        // Aplica la gravedad en el tiempo y mueve el controlador hacia abajo
        verticalVelocity.y += gravity * Time.deltaTime; 
        controller.Move(verticalVelocity * Time.deltaTime); 
        */
        #endregion
    }

    //Metodo para recibir el horizontal input desde el script InputManager
    public void ReceiveInput(Vector2 _horizontalInput) 
    {
        horizontalInput = _horizontalInput; //Guardar el valor que viene desde el InputManager 
    }

    #region 3. Función del Salto (Descomentar en la Fase 3)
    /*
    public void OnJumpPressed() 
    {
        jump = true; 
    }
    */
    #endregion
}