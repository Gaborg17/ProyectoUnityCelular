using UnityEngine;

public class EnemyAnimationHandler : MonoBehaviour
{
    [SerializeField] private Animator e_Anim;

    public void MoveToTarget(bool isMoving)
    {
        e_Anim.SetBool("IsWalking", isMoving);
    }

    public void Death(bool isDeath)
    {
        e_Anim.SetBool("IsDeath", isDeath);
    }

    public void Freeze(bool isFrozen)
    {
        e_Anim.SetBool("IsFrozen", isFrozen);
    }

    public void Attacking()
    {
        e_Anim.SetTrigger("IsAttacking");
    }



}
