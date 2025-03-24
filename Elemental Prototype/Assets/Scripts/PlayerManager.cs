using System.Collections;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IPlayer
{

    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private CharacterMovementController characterMovementController;


    [SerializeField] private float playerHealth = 100f;


    public Animator anim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(transform.position);
    }

    public void TakeDamage(float damage)
    {
        playerHealth -= damage;
        //anim.SetTrigger("takeHit");
    }

    public void HitBack(float hitbackForce, Transform attackOriginPosition)
    {
        //characterMovementController.SetCanMove(false);
        Vector3 hitForceTemp = (transform.position - attackOriginPosition.position).normalized;
        Vector3 hitForce = hitForceTemp;
        if (hitForceTemp.x > 0.1)
        {
            hitForce.x = 1;
        }
        else if (hitForceTemp.x < 0.1)
        {
            hitForce.x = -1;
        }
        if (hitForceTemp.y > 0.6)
        {
            hitForce.y = -1;
        }
        else if (hitForceTemp.y < -0.6)
        {
            hitForce.y = 1;
        }
        Debug.Log(hitForce);

        hitForce *= hitbackForce;
        rb.AddForce(hitForce, ForceMode2D.Impulse);
        Debug.Log(hitForce);

        StartCoroutine(ResetCanMove());
    }

    IEnumerator ResetCanMove()
    {
        yield return new WaitForSeconds(0.5f);
        //characterMovementController.SetCanMove(true);
        rb.linearVelocity = Vector2.zero;
    }
}
