using UnityEngine;

public class FireKnightAttackStates : MonoBehaviour
{
    public AttackType currentAttack;

    public void SetAttackState(string attack)
    {
        switch (attack)
        {
            case "Light":
                currentAttack = AttackType.Light;
                break;
        }
    }

    public AttackType GetAttackState()
    {
        return currentAttack;
    }

}


public enum AttackType
{
    Light,
    Medium,
    Heavy
}