using UnityEngine;
[CreateAssetMenu(menuName = "Abilities/MeleeAbility")]
public class MeleeAbilitySO : AbilitySO
{


    public override void Activate(Transform user, int direction)
    {
        user.gameObject.SetActive(true);
        Collider sword = user.GetChild(0).GetComponent<Collider>();
        sword.enabled = true;
        //Atacar
        Debug.Log("Usando ataque melee");
    }

}
