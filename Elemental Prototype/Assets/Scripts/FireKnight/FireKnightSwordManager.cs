using Unity.VisualScripting;
using UnityEngine;

public class FireKnightSwordManager : MonoBehaviour
{
    public FireKnightAttackStates State;
    public FireKnightCombatController CombatController;

    [Header("Basic Attack Values")]
    public float BasicMin;
    public float BasicMax;
    public float BasicHitback;

    [Header("Medium Attack Values")]
    public float MediumMin;
    public float MediumMax;
    public float MediumHitback;

    public Transform attackOriginPosition;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Transform current = collision.transform;
        while (current != null)
        {
            IPlayer p = current.GetComponent<IPlayer>();
            if (p != null && (current.GameObject() != this.transform.parent.gameObject))
            {
                //collision.gameObject.GetComponent<PlayerManager>().HitBack(2f, transform.root);
                switch (State.GetAttackState())
                {
                    case AttackType.Light:
                        {
                            if (CombatController.GetBasicAttackReady())
                            {
                                BasicAttack(current.gameObject.GetComponent<PlayerManager>());
                            }
                            break; 
                        }
                    case AttackType.Medium:
                        {
                            if (CombatController.GetMediumAttackReady())
                            {
                                MediumAttack(current.gameObject.GetComponent<PlayerManager>());
                            }
                            break;
                        }
                }
                
                break;
            }
            current = current.parent;
        }
    }

    void BasicAttack(PlayerManager enemy)
    {
        enemy.TakeDamage(Random.Range(BasicMin, BasicMax));
        enemy.HitBack(BasicHitback, attackOriginPosition);
        //Debug.Log("Basic Attack");
        CombatController.SetBasicAttackReady(false);
    }

    void MediumAttack(PlayerManager enemy)
    {
        enemy.TakeDamage(Random.Range(MediumMin, MediumMax));
        enemy.HitBack(MediumHitback, attackOriginPosition);
        Debug.Log("Medium Attack");
        CombatController.SetMediumAttackReady(false);
    }
}



