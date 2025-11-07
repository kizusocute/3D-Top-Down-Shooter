using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private InputSystem_Actions inputActions;
    private CharacterController characterController;
    private Animator animator;

    public Vector2 moveInput;
    public Vector2 aimInput;

    private float verticalVelocity;
    const float GRAVITY = 9.81f;
    const float GROUND_STICK = -.5f;
    private float dampTime =  .1f;
    [Header("Movement")]
    public Vector3 moveDirection;
    bool isRunning = false;
    private float speed;
    [SerializeField] private float walkSpeed = 1.5f;
    [SerializeField] private float runSpeed = 3f;

    [Header("Aiming")]
    [SerializeField]private LayerMask aimLayerMask;
    [SerializeField]private Transform aimPoint;
    private Vector3 lookingDirection;

    private void Awake()
    {
        AssignInputActionSystem();
    }

    private void AssignInputActionSystem()
    {
        inputActions = new InputSystem_Actions();

        inputActions.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += ctx => moveInput = Vector2.zero;

        inputActions.Player.Aim.performed += ctx => aimInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Aim.canceled += ctx => aimInput = Vector2.zero;

        inputActions.Player.Run.performed += ctx =>
        {
                isRunning = true;
                speed = runSpeed;
        };
        inputActions.Player.Run.canceled += ctx =>
        {
            isRunning = false;
            speed = walkSpeed;
        };
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
        animator = GetComponentInChildren<Animator>();

        speed = walkSpeed;
    }

    private void Update()
    {
        ApplyMovement();
        AimTowardsMouse();
        AnimatorController();
    }

    private void AnimatorController()
    {
        float xVelocity = Vector3.Dot(moveDirection.normalized, transform.right);//so sanh huong di chuyen voi huong ngang cua player
        float zVelocity = Vector3.Dot(moveDirection.normalized, transform.forward);

        animator.SetFloat("xVelocity", xVelocity, dampTime, Time.deltaTime);//Damp for smoothing
        animator.SetFloat("zVelocity", zVelocity, dampTime, Time.deltaTime);

        bool playRunAnimation = moveDirection.magnitude > 0 && isRunning;
        animator.SetBool("isRunning", playRunAnimation);
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
            characterController.Move(moveDirection * Time.deltaTime * walkSpeed);
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
