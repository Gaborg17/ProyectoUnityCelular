using UnityEngine;

public class PlayerAbilityDamage : MonoBehaviour
{

    
    [SerializeField] private AbilitySO abilitySO;
    [SerializeField] private LayerMask maskToInteract;
    [SerializeField] private float range;


    private void Start()
    {
        switch (abilitySO.abilityName)
        {
            case "Fireball":
                AudioManager.Instance.Play("AtaqueFuego");
                break;
            case "Congelar":
                AudioManager.Instance.Play("AtaqueHielo");
                break;
            case "Control Mental":
                AudioManager.Instance.Play("Hipnosis");
                break;
            case "Teletransportacion":
                AudioManager.Instance.Play("TeletransportacionLanzar");
                break;

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Walls") || other.CompareTag("Ground"))
        {
            switch (abilitySO.abilityName)
            {
                case "Protect":
                    break;
                case "Sword":
                    break;
                default:
                    Destroy(this.gameObject);
                    break;
            }
        }

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
                    AudioManager.Instance.Play("ImpactoFuego");
                    foreach (Collider collider in Physics.OverlapSphere(transform.position, range, maskToInteract))
                    {
                        if(collider.gameObject.CompareTag("Enemy"))
                        collider.GetComponent<IDamageable>().GetDamaged(abilitySO.damage);
                    }
                    Destroy(this.gameObject);
                    break;
                case "Congelar":
                    Destroy(this.gameObject);
                    AudioManager.Instance.Play("ImpactoHielo");
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
            GameObject particle = Instantiate(abilitySO.collisionParticle, this.transform.position, Quaternion.Euler(-90,0,0));

            Destroy(particle.gameObject, 1f);
            Debug.Log("Collision");
            if(abilitySO.abilityName == "Teletransportacion")
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                Rigidbody rb = player.GetComponent<Rigidbody>();
                Debug.Log("moviendo");
                AudioManager.Instance.Play("Teletransportacion");
                rb.position =collision.GetContact(0).point + Vector3.up * 0.5f;
                Destroy(this.gameObject);

            }
        }
    }

    private void OnEnable()
    {
        if (abilitySO.abilityName == "Protect")
        {
            AudioManager.Instance.Play("EscudoOn");
        }
    }

    private void OnDisable()
    {
        if (abilitySO.abilityName == "Protect")
        {
            AudioManager.Instance.Play("EscudoOff");
        }
    }

}
