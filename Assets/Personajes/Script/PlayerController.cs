using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class PlayerController : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    public float walkSpeed = 5f;
    public float sprintSpeed = 10f;
    private float currentSpeed;
 
    [Header("Animación")]
    public Animator animator;
    public float speedThreshold = 0.1f;
 
    private Rigidbody rb;
    private Vector3 movement;
 
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentSpeed = walkSpeed;
    }
 
    void Update()
    {
        HandleMovementInput();
        UpdateAnimator();
    }
 
    void FixedUpdate()
    {
        MoveCharacter(movement);
    }
 
    void HandleMovementInput()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
 
        movement = transform.TransformDirection(new Vector3(horizontal, 0, vertical));
        currentSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;
    }
 
    void MoveCharacter(Vector3 direction)
    {
        if (direction.magnitude > 1f)
        {
            direction.Normalize();
        }
 
        Vector3 targetVelocity = direction * currentSpeed;
        targetVelocity.y = rb.velocity.y;
        rb.velocity = targetVelocity;
    }
 
    void UpdateAnimator()
    {
        float horizontalSpeed = new Vector3(rb.velocity.x, 0, rb.velocity.z).magnitude;
        bool isWalking = horizontalSpeed > speedThreshold;
        animator.SetBool("isWalking", isWalking);
 
        bool isRunning = (currentSpeed == sprintSpeed) && isWalking;
        animator.SetBool("isRunning", isRunning);
    }
}