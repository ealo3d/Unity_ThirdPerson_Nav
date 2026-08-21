using UnityEngine;
//Necesitamos esta librería para usar la Inteligencia Artificial de Unity
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("Referencias Principales")]
    public Transform target; // El objetivo a perseguir (Nuestro Jugador)
    private NavMeshAgent agent;

    #region FASE 2: Animación de la IA
    private Animator animator;
    private int speedHash;
    #endregion

    private void Awake()
    {
        // Conectamos el componente de IA que acabamos de agregar
        agent = GetComponent<NavMeshAgent>();

        #region FASE 2: Conectar Animator
        // Buscamos el Animator en el modelo 3D hijo
        animator = GetComponentInChildren<Animator>();
        // Optimizamos el parámetro del Blend Tree
        speedHash = Animator.StringToHash("Speed");
        #endregion
    }

    private void Update()
    {
        // Medida de seguridad: Si no hay jugador asignado, no hacemos nada
        if (target == null) return;

        // === FASE 1: PERSECUCIÓN ===
        // Le damos las coordenadas del jugador al agente. 
        // El NavMeshAgent calcula la ruta automáticamente esquivando obstáculos.
        agent.SetDestination(target.position);

        #region FASE 2: Sincronizar Físicas con Animación
        // agent.velocity.magnitude nos devuelve exactamente a qué velocidad se está moviendo el agente.
        // Si choca con una pared, esto cae a cero automáticamente.
        float currentSpeed = agent.velocity.magnitude;

        // Le enviamos la velocidad real al Blend Tree para que el monstruo mueva los pies
        animator.SetFloat(speedHash, currentSpeed, 0.1f, Time.deltaTime);
        #endregion
    }
}

