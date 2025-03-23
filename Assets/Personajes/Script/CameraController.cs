using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class CameraController : MonoBehaviour
{
    [Header("Configuración de Cámara")]
    public Transform player; 
    public float distance = 5f; 
    public float height = 2f; 
    public float sensitivity = 2f; 
    public float rotationSmoothTime = 0.1f;
 
    private Vector3 currentRotation;
    private Vector3 rotationSmoothVelocity;
    private float yaw; 
    private float pitch;
 
    [Header("Límites de Ángulo Vertical")]
    public float minPitch = -20f;
    public float maxPitch = 60f;
 
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
 
    void LateUpdate()
    {
        HandleCameraRotation();
        UpdateCameraPosition();
    }
 
    void HandleCameraRotation()
    {
        yaw += Input.GetAxis("Mouse X") * sensitivity;
        pitch -= Input.GetAxis("Mouse Y") * sensitivity;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
 
        Vector3 targetRotation = new Vector3(pitch, yaw);
        currentRotation = Vector3.SmoothDamp(currentRotation, targetRotation, ref rotationSmoothVelocity, rotationSmoothTime);
    }
 
    void UpdateCameraPosition()
    {
        Quaternion rotation = Quaternion.Euler(currentRotation.x, currentRotation.y, 0);
        Vector3 targetPosition = player.position - (rotation * Vector3.forward * distance) + (Vector3.up * height);
 
        transform.position = targetPosition;
        transform.LookAt(player.position + Vector3.up * height);
    }
}