using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Variable pública para asignar a qué objeto (Transform) debe seguir la cámara (ej. el jugador)
    public Transform target;
    
    // Define la distancia (desplazamiento) que mantendrá la cámara respecto al objetivo
    public Vector3 offset = new Vector3(0f, 3f, -6f);
    
    // Tiempo en segundos que tarda la cámara en alcanzar al objetivo (suavidad del movimiento)
    public float smoothTime = 0.2f;
    
    // Variable interna requerida por SmoothDamp para almacenar la velocidad actual del movimiento
    private Vector3 currentVelocity = Vector3.zero;

    private void LateUpdate()
    {
        // Si no hay ningún objetivo asignado, interrumpe la función para evitar errores
        if (target == null) return;
        
        // Calcula la posición deseada sumando la posición exacta del objetivo más el desplazamiento (offset)
        Vector3 targetPosition = target.position + offset;
        
        // Mueve la cámara suavemente desde su posición actual hacia la posición objetivo calculada
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);
    }
}