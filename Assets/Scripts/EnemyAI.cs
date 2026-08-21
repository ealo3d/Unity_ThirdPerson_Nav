using UnityEngine;
using UnityEngine.AI; // ¡VITAL! Para acceder a los componentes de navegación de Unity

public class EnemyAI : MonoBehaviour
{
    // === FASE 1: VARIABLES BASE ===
    [Header("Referencias Principales")]
    public Transform target; // El jugador a perseguir
    private NavMeshAgent agent; // El componente "cerebro motor" de la IA

    #region FASE 2: Animación
    /*
    private Animator animator; // Referencia al Animator en el modelo 3D (hijo)
    private int speedHash; // ID optimizado para el parámetro del Blend Tree
    */
    #endregion

    #region FASE 3: Inteligencia y Detección
    /*
    [Header("Inteligencia")]
    public float detectionRadius = 10f; // Distancia a la que el enemigo "despierta"
    */
    #endregion

    private void Awake()
    {
        // === FASE 1: INICIALIZACIÓN ===
        agent = GetComponent<NavMeshAgent>();

        #region FASE 2: Inicialización del Animator
        /*
        animator = GetComponentInChildren<Animator>(); // Buscamos en el hijo (la malla visual)
        speedHash = Animator.StringToHash("Speed");
        */
        #endregion
    }

    private void Update()
    {
        // Medida de seguridad: Si no hay jugador asignado, evitamos errores y no hacemos nada
        if (target == null) return;

        #region FASE 3: Lógica de Detección (Reemplaza la línea de la Fase 1)
        /*
        // Calculamos la distancia exacta entre el enemigo y el jugador usando matemáticas de vectores
        float distanceToPlayer = Vector3.Distance(transform.position, target.position);

        // Si la distancia es menor o igual al radio de detección...
        if (distanceToPlayer <= detectionRadius)
        {
            agent.SetDestination(target.position); // ¡Calcula la ruta y persíguelo!
        }
        else
        {
            // Si el jugador está lejos, el enemigo establece su destino en su propia posición actual (frena)
            agent.SetDestination(transform.position); 
        }
        */
        #endregion

        // === LÍNEA DE FASE 1 (Persecución infinita) ===
        // NOTA PARA CLASE: Bórrala o coméntala cuando actives la Fase 3, de lo contrario sobrescribirá la lógica del radio.
        agent.SetDestination(target.position);

        #region FASE 2: Sincronizar Físicas con Animación
        /*
        // agent.velocity.magnitude nos dice la velocidad real de desplazamiento (si choca una pared, es 0)
        float currentSpeed = agent.velocity.magnitude;

        // Le enviamos esa velocidad al Blend Tree para que cambie suavemente entre Idle, Walk o Run
        animator.SetFloat(speedHash, currentSpeed, 0.1f, Time.deltaTime);
        */
        #endregion
    }

    #region FASE 3: Dibujar la Zona Visualmente
    /*
    // OnDrawGizmosSelected dibuja formas en la ventana de Escena (no se ven en el juego final).
    // Nos permite visualizar matemáticamente el tamaño real del radio de detección.
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        // Dibuja una esfera de alambre usando la posición del enemigo como centro y nuestro radio como tamaño
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
    */
    #endregion
}