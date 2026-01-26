using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/PrejectileAbility")]
public class ProjectileAbilitySO : AbilitySO
{
    public GameObject projectilePrefab;
    public float speed;
    public float shootAngle;


    public override void Activate(Transform user, int direction)
    {
            GameObject projectile = Instantiate(projectilePrefab, user.position, user.rotation);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();

            rb.AddForce(direction * Vector3.right * speed, ForceMode.Impulse);
            rb.AddForce(Vector3.up * shootAngle, ForceMode.Impulse);

        Destroy(projectile.gameObject,4f);

            Debug.Log($"Disparando {abilityName}");
            //LogicaDisparo

    }

    


}
