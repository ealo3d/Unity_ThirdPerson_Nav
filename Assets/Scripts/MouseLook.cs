using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] float sensitivityX = 8f; 
    [SerializeField] float sensitivityY = 0.5f; 
    float mouseX, mouseY; 

    #region 2. Variables Mirada Vertical (Descomentar en Fase 2)
    
    [SerializeField] Transform playerCamera;
    [SerializeField] float xClamp = 85f;
    float xRotation = 0f;
    
    #endregion

    private void Update()
    {
        // 1. Mirada Horizontal
        // Rota todo el cuerpo del jugador (cápsula/Demy) en el eje Y
        transform.Rotate(Vector3.up, mouseX * Time.deltaTime); 

        #region 2. Lógica Mirada Vertical (Descomentar en Fase 2)
        
        // Calculamos la rotación (se resta para no invertir el control)
        xRotation -= mouseY; 
        
        // Clamp limita el ángulo para no dar la vuelta de campana (e.g. -85 a 85 grados)
        xRotation = Mathf.Clamp(xRotation, -xClamp, xClamp);
        
        // Aplicamos la rotación calculada exclusivamente a la cámara, preservando Y y Z
        Vector3 targetRotation = transform.eulerAngles;
        targetRotation.x = xRotation; 
        playerCamera.eulerAngles = targetRotation; 
        
        #endregion
    }

    public void ReceiveInput(Vector2 mouseInput) 
    {
        // Multiplicamos el input puro por la sensibilidad deseada
        mouseX = mouseInput.x * sensitivityX; 
        mouseY = mouseInput.y * sensitivityY; 
    }
}