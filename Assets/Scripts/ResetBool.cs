using UnityEngine;

// Declaramos una clase pública llamada ResetBool que hereda de StateMachineBehaviour.
// Esto permite que el script se adjunte a un estado dentro de la máquina de estados del Animator (Animator Controller).
public class ResetBool : StateMachineBehaviour
{
    // Variable pública tipo string para almacenar el nombre del booleano del Animator que queremos modificar.
    public string isInteractingBool; 
    
    // Variable pública bool para el estado del booleano.
    public bool isInteractingStatus; 

    // Sobrescribimos el método OnStateEnter, llamado automáticamente cuando la transición entra en el estado del Animator.
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Usamos el componente Animator (que se pasa como parámetro) para establecer el valor del parámetro booleano.
        // Se le pasa el nombre del parámetro (isInteractingBool) y el valor que le queremos asignar (isInteractingStatus).
        animator.SetBool(isInteractingBool, isInteractingStatus);
    }
}