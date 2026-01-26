using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(menuName = "Abilities/ShieldAbility")]
public class ShieldAbilitySO : AbilitySO
{
    public GameObject shieldPrefab;
    public float shieldDuration;

    public override void Activate(Transform user, int direction)
    {
            GameObject shield = Instantiate(shieldPrefab, user.parent.position, user.rotation);
            shield.transform.parent = user.parent.transform;
            Debug.Log($"Usando {abilityName}");

            Destroy(shield.gameObject, shieldDuration);     

    }


    
}
