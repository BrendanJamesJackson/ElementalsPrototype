using UnityEngine;
using UnityEngine.InputSystem;

public class FireKnightCombatController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public PlayerInput _playerInput;

    private bool isBlocking;

    public float comboTimeAllowance;
    private float timeSincePrimary;
    private float timeSinceSecondary;
    private bool PrimaryActivated = false;
    private bool SecondaryActivated = false;

    public BoxCollider2D swordCollider;

    public bool BasicAttackReady = true;

    public void SetBasicAttackReady(bool ready)
    {
        BasicAttackReady = ready;
    }

    public bool GetBasicAttackReady()
    {
        return BasicAttackReady;
    }

    void Update()
    {
        UpdateAnimations();

        if (PrimaryActivated)
        {
            timeSincePrimary += Time.deltaTime;
        }
        if (timeSincePrimary > comboTimeAllowance)
        {
            PrimaryActivated = false;
        }

        if (SecondaryActivated)
        {
            timeSinceSecondary += Time.deltaTime;
        }
        if (timeSinceSecondary > comboTimeAllowance)
        {
            SecondaryActivated = false;
        }
    }

    public void PrimaryAttackInputHandler(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (SecondaryActivated && timeSinceSecondary > 0.05f)
            {
                ThirdAttack();
            }
            else
            {
                BasicAttack();
                timeSincePrimary = 0;
                PrimaryActivated = true;
            }

        }
    }

    public void SecondaryAttackInputHandler(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (PrimaryActivated && timeSincePrimary > 0.05f)
            {
                SecondAttack();
                timeSinceSecondary = 0;
                SecondaryActivated = true;
            }
        }
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
        animator.SetTrigger("basicAttack");
        BasicAttackReady = true;
        //Debug.Log("Basic attack");
    }

    public void SecondAttack()
    {
        animator.SetTrigger("secondAttack");
        //Debug.Log("Second Attack");
    }

    public void ThirdAttack()
    {
        animator.SetTrigger("thirdAttack");
        //Debug.Log("Third Attack");
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
