using System.Collections;
using UnityEngine;
using System;

public class PlayerManager : MonoBehaviour, IPlayer
{

    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private CharacterMovementController characterMovementController;


    [SerializeField] private float playerHealth = 100f;

    public float hitBackMultiply;

    public event Action<float>OnHealthChanged;

    public Animator anim;

    public void TakeDamage(float damage)
    {
        playerHealth -= damage;
        anim.SetTrigger("takeHit");

        OnHealthChanged?.Invoke(playerHealth);
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
        hitForce.y = 0;
        rb.AddForce(hitForce* hitBackMultiply, ForceMode2D.Impulse);
        characterMovementController.SetHitBack(true);
        Debug.Log(hitForce* hitBackMultiply + "HitForce");

        StartCoroutine(ResetCanMove());
    }

    IEnumerator ResetCanMove()
    {
        yield return new WaitForSeconds(0.1f);
        characterMovementController.SetHitBack(false);

        //characterMovementController.SetCanMove(true);
        rb.linearVelocity = Vector2.zero;
    }
}
