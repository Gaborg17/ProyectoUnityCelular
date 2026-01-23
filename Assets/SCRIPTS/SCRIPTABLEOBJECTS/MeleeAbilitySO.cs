using UnityEngine;
[CreateAssetMenu(menuName = "Abilities/MeleeAbility")]
public class MeleeAbilitySO : AbilitySO
{


    public override void Activate(GameObject user)
    {
        //ActivarArma y atacar
        Debug.Log("Usando ataque melee");
    }

}
