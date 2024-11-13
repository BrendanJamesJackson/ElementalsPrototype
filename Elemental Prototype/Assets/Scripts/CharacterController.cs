using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{

    private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float downForce;
    [SerializeField] private float gravityAdjust;
    [SerializeField] private float rollDashForce;

    [SerializeField] private Transform groundCheckPos;

    [SerializeField] private Animator animator;
    private bool isWalking = false;
    private bool isJumping;
    private bool isFalling;
    private bool isFacingRight = true;
    public bool isJumpingDown =false;

    private bool canDash = true;
    private bool isDashing;
    [SerializeField] private float dashingPower;
    [SerializeField] private float dashingTime;
    [SerializeField] private float dashingCooldown;

    private bool isGrounded = false;

    public LayerMask groundMask;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        AdjustGravity();
        CheckGround();
        UpdateAnimations();
    }

    public void MoveInputHandler(InputAction.CallbackContext context)
    {
        Debug.Log(context.ReadValue<Vector2>().x);
        if (context.ReadValue<Vector2>().y < 0 && !isJumpingDown)
        {
            JumpDown();
            Debug.Log("jump down");
        }
        else if (context.ReadValue<Vector2>().x > 0)
        {
            Move(1);
            Debug.Log("run");
        }
        else if (context.ReadValue<Vector2>().x < 0)
        {
            Move(-1);
        }
        else
        {
            Move(0);
        }

        
    }

    public void JumpInputHandler(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("jump");
            Jump();
        }
    }

    public void DashRollInputHandler(InputAction.CallbackContext context)
    {
        if (context.performed && canDash)
        {
            StartCoroutine(RollDash());
        }
    }

    public void AttackInputHandler(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Attack();
        }
    }

    public void UpdateAnimations()
    {
        if (rb.velocity.y < 0)
        {
            isFalling = true;
            isJumping = false;
        }
        else if (rb.velocity.y >= 0)
        {
            isFalling = false;
        }

        animator.SetBool("isWalking", isWalking);
        animator.SetFloat("jumpDirection",rb.velocity.y);
        animator.SetBool("isJumping", isJumping);
        animator.SetBool("isFalling",isFalling);
        animator.SetBool("isGrounded",isGrounded);
    }

    private void CheckGround()
    {
        RaycastHit2D groundInfo = Physics2D.Raycast(groundCheckPos.position, Vector2.down, 1f,groundMask);
        //Debug.Log(groundInfo.collider.gameObject);
        Debug.DrawRay(groundCheckPos.position, Vector2.down * 1, Color.yellow);

        //Debug.Log(groundInfo.collider.gameObject);

        if (groundInfo.collider == true && groundInfo.collider.gameObject.tag == "Ground")
        {
            //isJumping = false;
            isFalling = false;
            isGrounded = true;
            //Debug.Log("ground");
            isJumpingDown = false;
            //Debug.Log(isJumping);
        }
        else
        {
            //Debug.Log("no ground");
            isGrounded = false;
        }
    }

    public void Move(int xMovement)
    {

        if (isDashing || isJumpingDown)
        {
            return;
        }

        rb.velocity = new Vector2(xMovement * speed, rb.velocity.y);
        

        if ((isFacingRight && xMovement < 0) || (!isFacingRight && xMovement > 0))
        {
            Vector3 localScale = transform.localScale;
            isFacingRight = !isFacingRight;
            localScale.x *= -1;
            transform.localScale = localScale;
        }

        if (xMovement != 0)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
        if (rb.velocity.y > 0 && rb.velocity.y < 2.5)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);

        }
        
    }

    private IEnumerator RollDash()
    {
        canDash = false;
        isDashing = true;
        rb.velocity = new Vector2(transform.localScale.normalized.x * dashingPower,rb.velocity.y);
        animator.SetTrigger("rollDash");
        yield return new WaitForSeconds(dashingTime);
        rb.velocity = new Vector2(0, rb.velocity.y); ;
        isDashing = false;
        /*yield return new WaitForSeconds(dashingCooldown);*/
        canDash = true;
    }

    public void Jump()
    {
        isJumping = true;
        isJumpingDown = false;
        UpdateAnimations();
        //Debug.Log(isJumping);
        if (rb.velocity.y < 0)
        {
            rb.velocity = new Vector2(rb.velocity.x,0);
        }

        rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        
    }

    public void Attack()
    {
        animator.SetTrigger("attack");
    }

    public void JumpDown()
    {
        if (isJumping || isFalling)
        {
            rb.velocity = new Vector2(0, 0);
            rb.AddForce(new Vector2(0, -downForce), ForceMode2D.Impulse);
            isJumpingDown = true;
        }
    }

    public void AdjustGravity()
    {
        if (rb.velocity.y < 0)
        {
            rb.gravityScale = gravityAdjust;
        }
        else
        {
            rb.gravityScale = 1;
        }
    }

}

