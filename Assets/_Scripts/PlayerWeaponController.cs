using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    private Animator animator;
    private PlayerInput playerInput;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        animator = GetComponentInChildren<Animator>();

        playerInput.inputActions.Player.Attack.performed += ctx => Shoot();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Shoot()
    {
        animator.SetTrigger("Fire");
    }
}
