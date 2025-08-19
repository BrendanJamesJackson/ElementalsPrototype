using UnityEngine;
using UnityEngine.InputSystem;

public class FireKnightCombatController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public PlayerInput _playerInput;

    public CharacterMovementController _characterMovementController;

    private bool isBlocking;

    public float comboTimeAllowance;
    private float timeSincePrimary;
    private float timeSinceSecondary;
    private bool PrimaryActivated = false;
    private bool SecondaryActivated = false;
    public bool HoldAttackActivated = false;

    public BoxCollider2D swordCollider;

    public bool BasicAttackReady = true;
    public bool MediumAttackReady = true;

    public AudioSource swordAudioController;
    public AudioClip swordSwing1;
    public AudioClip swordSwing2;
    public AudioClip swordSwing3;
    public AudioClip swordSwingAir;
    public AudioClip swordCharge;
    public AudioClip swordRelease;
    public bool charging = false;

    public void StopAudio()
    {
        swordAudioController.Stop();
    }

    public void PlayAudio(int attackNum)
    {
        Debug.Log("Playing Sound");
        swordAudioController.Stop();

        switch (attackNum)
        {
            case 1:
                swordAudioController.resource = swordSwing1;
                swordAudioController.pitch = Random.Range(0.9f, 1.2f);
                break;
            case 2:
                swordAudioController.resource = swordSwing2;
                swordAudioController.pitch = Random.Range(0.6f, 1.3f);
                break;
            case 3:
                swordAudioController.resource = swordSwing3;
                swordAudioController.pitch = Random.Range(0.6f, 1.1f);
                break;
            case 4:
                swordAudioController.resource = swordSwingAir;
                swordAudioController.pitch = Random.Range(0.7f, 1.3f);
                break;
            case 5:
                if (charging)
                {
                    return;
                }
                else
                {
                    swordAudioController.resource = swordCharge;
                    swordAudioController.loop = true;
                    charging = true;
                }
                break;
            case 6:
                swordAudioController.loop = false;
                swordAudioController.resource = swordRelease;
                charging = false;
                break;
        }

        swordAudioController.Play();
        
    }

    public void SetBasicAttackReady(bool ready)
    {
        BasicAttackReady = ready;
    }

    public bool GetBasicAttackReady()
    {
        return BasicAttackReady;
    }

    public void SetMediumAttackReady(bool ready)
    {
        MediumAttackReady = ready;
    }

    public bool GetMediumAttackReady()
    {
        return MediumAttackReady;
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
            else
            {
                HoldAttackActivated = true;
            }
        }
    }

    public void HoldAttackInputHandler(InputAction.CallbackContext context)
    {
        if (context.started && !HoldAttackActivated)
        {
            Debug.Log("hold start");
            HoldAttackStart();
            HoldAttackActivated = true;
        }
        else if (context.canceled)
        {
            HoldAttackReleased();
            HoldAttackActivated = false;
        }
    }   
    
    public void HoldAttackStart()
    {
        animator.SetTrigger("specialAttackStart");
        animator.SetBool("specialAttack", true);
        _characterMovementController.SetCanMove(false);
    }

    public void HoldAttackReleased()
    {
            animator.SetBool("specialAttack", false);
        _characterMovementController.SetCanMove(true);
    }

    public void BlockInputHandler(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Block(true);
            animator.SetTrigger("block");
            _characterMovementController.SetCanMove(false);
        }
        else if (context.canceled)
        {
            Block(false);
            _characterMovementController.SetCanMove(true);
        }
    }

    public void UpdateAnimations()
    {
        animator.SetBool("isBlocking", isBlocking);
    }

    public void BasicAttack()
    {
        BasicAttackReady = true;

        _characterMovementController.SetCanMove(false);
        animator.SetTrigger("basicAttack");
        
        
        //Debug.Log("Basic attack");
    }

    public void SecondAttack()
    {
        MediumAttackReady = true;
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
