using UnityEngine;

public class PlayerAnimManager : MonoBehaviour
{
    [SerializeField] private Animator pAnim;

    public void Run(float vector)
    {
        if (pAnim != null)
        {
            pAnim.SetFloat("isMoving", vector);
            
        }
    }

    public void Jump()
    {
        if (pAnim != null)
        {
            pAnim.SetTrigger("isJumping");
        }
    }

    public void Fall(bool isGrounded)
    {
        if (pAnim != null)
        {
            pAnim.SetBool("isGrounded", isGrounded);
        }
    }


    public void Damaged()
    {
        if (pAnim != null)
        {

        }
    }

    public void Death()
    {
        if (pAnim != null)
        {

        }
    }



}
