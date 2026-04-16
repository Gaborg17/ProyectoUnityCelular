using UnityEngine;
[CreateAssetMenu(menuName = "Abilities/MeleeAbility")]
public class MeleeAbilitySO : AbilitySO
{


    public override void Activate(Transform user, int direction)
    {
        PlayerAnimManager pAnim = FindAnyObjectByType<PlayerAnimManager>();
        AudioManager.Instance.Play("AtaqueEspada");
        pAnim.SwordAttack();
    }

}
