using UnityEngine;
[CreateAssetMenu(menuName = "Abilities/ShieldAbility")]
public class ShieldAbilitySO : AbilitySO
{
    public float radius;

    public override void Activate(GameObject user)
    {

            //Activar escudo
            Debug.Log($"Usando {abilityName}");
        
    }
}
