using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private InputSystem_Actions inputActions;
    private CharacterController characterController;

    public Vector2 moveInput;
    public Vector2 aimInput;

    private float verticalVelocity;
    const float GRAVITY = 9.81f;
    const float GROUND_STICK = -.5f;

    [Header("Movement")]
    public Vector3 moveDirection;
    [SerializeField] private float moveSpeed = 5f;

    [Header("Aiming")]
    [SerializeField]private LayerMask aimLayerMask;
    [SerializeField]private Transform aimPoint;
    private Vector3 lookingDirection;

    private void Awake()
    {
        inputActions = new InputSystem_Actions();

        inputActions.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += ctx => moveInput = Vector2.zero;

        inputActions.Player.Aim.performed += ctx => aimInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Aim.canceled += ctx => aimInput = Vector2.zero;
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
    }
    private void OnDisable()
    {
        inputActions.Player.Disable();
    }
    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        ApplyMovement();
        AimTowardsMouse();
    }

    private void ApplyGravity()
    {
        if(!characterController.isGrounded)
        {
            verticalVelocity -= GRAVITY * Time.deltaTime;
            moveDirection.y = verticalVelocity;
        }
        else
        {
            verticalVelocity = GROUND_STICK;
        }
    }

    private void ApplyMovement()
    {
        moveDirection = new Vector3(moveInput.x, 0, moveInput.y);
        ApplyGravity();
        if (moveDirection.magnitude > 0)
        {
            characterController.Move(moveDirection * Time.deltaTime * moveSpeed);
        }
    }
    private void AimTowardsMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(aimInput);
        if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, aimLayerMask))
        {
            lookingDirection = hitInfo.point - transform.position;
            lookingDirection.y = 0f;
            lookingDirection.Normalize();
            
            transform.forward = lookingDirection;

            aimPoint.position = new Vector3(hitInfo.point.x, transform.position.y, hitInfo.point.z);
        }
    }
}
