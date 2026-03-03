using UnityEngine;

public class PlayerAbilityDamage : MonoBehaviour
{

    
    [SerializeField] private AbilitySO abilitySO;
    [SerializeField] private LayerMask maskToInteract;
    [SerializeField] private float range;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (abilitySO.collisionParticle != null)
            {
                GameObject particle = Instantiate(abilitySO.collisionParticle, this.transform.position, this.transform.rotation);

                Destroy(particle.gameObject, 1f);
            }

            switch (abilitySO.abilityName)
            {
                case "Fireball":

                    foreach (Collider collider in Physics.OverlapSphere(transform.position, range, maskToInteract))
                    {
                        if(collider.gameObject.CompareTag("Enemy"))
                        collider.GetComponent<IDamageable>().GetDamaged(abilitySO.damage);
                    }
                    Destroy(this.gameObject);
                    break;
                case "Congelar":
                    Destroy(this.gameObject);
                    foreach (Collider collider in Physics.OverlapSphere(transform.position, range, maskToInteract))
                    {
                        if (collider.gameObject.CompareTag("Enemy"))
                            collider.GetComponent<Enemy>().frozen = true;
                    }
                    
                    break;
                case "Control Mental":
                    other.GetComponent<Enemy>().mindControl = true;
                    Destroy(this.gameObject);
                    break;
                case "Protect":
                    other.GetComponent<IDamageable>().GetDamaged(abilitySO.damage);

                    
                    break;
                case "Sword":
                    other.GetComponent<IDamageable>().GetDamaged(abilitySO.damage);
                    Collider sword = GetComponent<Collider>();
                    sword.enabled = false;
                    break;

            }



        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground") || collision.collider.CompareTag("Walls"))
        {
            Debug.Log("Collision");
            if(abilitySO.abilityName == "Teletransportacion")
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                Rigidbody rb = player.GetComponent<Rigidbody>();
                Debug.Log("moviendo");
                rb.position =collision.GetContact(0).point + Vector3.up * 0.5f;
                Destroy(this.gameObject);

            }
        }
    }

    private void OnDestroy()
    {
        if(abilitySO.abilityName == "Protect")
        {
            PlayerHealthHandler pH = FindAnyObjectByType<PlayerHealthHandler>();
            pH.isProtected = false;
        }
    }

}
