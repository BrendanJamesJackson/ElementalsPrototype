using UnityEngine;

public class FireKnightSwordManager : MonoBehaviour
{
    public FireKnightAttackStates State;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Transform current = collision.transform;
        while (current != null)
        {
            IPlayer p = current.GetComponent<IPlayer>();
            if (p != null)
            {
                //collision.gameObject.GetComponent<PlayerManager>().HitBack(2f, transform.root);
                switch (State.GetAttackState())
                {
                    case AttackType.Light:
                        { 
                            break; 
                        }
                }

                break;
            }
            current = current.parent;
        }
    }
}



