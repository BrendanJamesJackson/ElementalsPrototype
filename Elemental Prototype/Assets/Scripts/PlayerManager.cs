using System.Collections;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IPlayer
{

    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private CharacterMovementController characterMovementController;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HitBack(float hitbackForce, Transform opponentPosition)
    {
        //characterMovementController.SetCanMove(false);
        Vector3 hitForce = transform.position - opponentPosition.position;
        hitForce *= hitbackForce;
        rb.AddForce(hitForce, ForceMode2D.Impulse);
        StartCoroutine(ResetCanMove());
    }

    IEnumerator ResetCanMove()
    {
        yield return new WaitForSeconds(0.5f);
        //characterMovementController.SetCanMove(true);
    }
}
