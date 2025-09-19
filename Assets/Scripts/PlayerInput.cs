using UnityEngine;
using System;

public class PlayerInput : MonoBehaviour
{
    public event Action<Vector3> OnMouseClick;
    RaycastHit hitInfo = new();
    public LayerMask clickLayerMask;
        
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray.origin, ray.direction, out hitInfo, 100, clickLayerMask))
            {
                OnMouseClick?.Invoke(hitInfo.point);
                Debug.Log($"Selected position is {hitInfo.point}");
            }
        }
    }
}
