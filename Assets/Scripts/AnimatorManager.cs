using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    // Variable para almacenar la referencia al componente Animator del GameObject
    Animator animator;
    
    // Variable entera para almacenar el ID (hash) del parámetro "Horizontal" del Animator (optimización)
    int horizontal;
    
    // Variable entera para almacenar el ID (hash) del parámetro "Vertical" del Animator (optimización)
    int vertical;

    private void Awake()
    {
        // Obtenemos y guardamos la referencia al componente Animator adjunto a este mismo GameObject
        animator = GetComponent<Animator>();
        
        // Convertimos el string "Horizontal" y "vertical" a un ID numérico (hash) para modificar el parámetro más rápido
        horizontal = Animator.StringToHash("Horizontal");
        vertical = Animator.StringToHash("Vertical");
    }

    // Método público que otros scripts llamarán para actualizar los parámetros del Animator
    public void UpdateAnimatorValues(float horizontalMovement, float verticalMovement, bool isRunning)
    {
        // Si el personaje está corriendo (isRunning es true), sobrescribimos el movimiento vertical
        if (isRunning) { verticalMovement = 2f; }

        // Actualizamos el parámetro horizontal en el Animator, con un tiempo de suavizado de 0.1 segundos (dampTime)
        animator.SetFloat(horizontal, horizontalMovement, 0.1f, Time.deltaTime);
        
        // Actualizamos el parámetro vertical en el Animator, con el mismo tiempo de suavizado para evitar cambios bruscos
        animator.SetFloat(vertical, verticalMovement, 0.1f, Time.deltaTime);
    }
    
    #region FASE 3
    /*
    // Método para forzar la reproducción de una animación específica y marcar al jugador como interactuando
    public void PlayerTargetAnimation(string targetAnimation, bool isInteracting)
    {
        // Modificamos el parámetro booleano "isInteracting" del Animator
        animator.SetBool("isInteracting", isInteracting);
        
        // Hacemos una transición suave hacia la nueva animación (targetAnimation) en 0.2 segundos
        animator.CrossFade(targetAnimation, 0.2f);
    }
    */
    #endregion
}