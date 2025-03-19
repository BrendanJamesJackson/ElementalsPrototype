using UnityEngine;
using UnityEngine.InputSystem;

public class FireKnightCombatController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public PlayerInput _playerInput;

    private bool isBlocking;

    public float comboTimeAllowance;
    private float timeSincePrimary;
    private bool PrimaryActivated = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAnimations();
    }

    public void PrimaryAttackInputHandler(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            BasicAttack();
        }
    }

    public void SecondaryAttackInputHandler(InputAction.CallbackContext context)
    {

    }

    public void BlockInputHandler(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Block(true);
            animator.SetTrigger("block");
        }
        else if (context.canceled)
        {
            Block(false);
        }
    }

    public void UpdateAnimations()
    {
        animator.SetBool("isBlocking", isBlocking);
    }

    public void BasicAttack()
    {
        animator.SetTrigger("attack");
        //Debug.Log("attack");
    }

    public void SecondAttack()
    {

    }

    public void Block(bool state)
    {
        isBlocking = state;
    }
    public void TakeHit()
    {
        animator.SetTrigger("TakeHit");
    }

}
