using UnityEngine;
[CreateAssetMenu(menuName = "Abilities/ShieldAbility")]
public class ShieldAbilitySO : AbilitySO
{
    public GameObject shieldPrefab;
    public float shieldDuration;

    public override void Activate(Transform user, int direction)
    {
        Debug.Log($"Usando {abilityName}");
        PlayerHealthHandler pH = FindAnyObjectByType<PlayerHealthHandler>();
        pH.isProtected = true;
    }



}
