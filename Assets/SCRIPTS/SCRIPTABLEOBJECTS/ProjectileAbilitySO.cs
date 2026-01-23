using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/PrejectileAbility")]
public class ProjectileAbilitySO : AbilitySO
{
    public GameObject projectilePrefab;
    public float speed;
    public float shootAngle;


    public override void Activate(GameObject user)
    {
            GameObject projectile = Instantiate(projectilePrefab, user.transform.position, user.transform.rotation);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();

            rb.AddForce(Vector3.right * speed, ForceMode.Impulse);
            rb.AddForce(Vector3.up * shootAngle, ForceMode.Impulse);

            Debug.Log($"Disparando {abilityName}");
            //LogicaDisparo

    }

    


}
