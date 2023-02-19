using UnityEngine;

public class CallAttackFromModel : MonoBehaviour
{
    public void Punch()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>().MeleeAttack();
    }

    public void Throw()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>().RangedAttack();
    }
}
