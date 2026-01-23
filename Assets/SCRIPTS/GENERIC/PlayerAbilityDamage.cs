using UnityEngine;

public class PlayerAbilityDamage : MonoBehaviour
{

    
    [SerializeField] private AbilitySO abilitySO;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            

            switch (abilitySO.abilityName)
            {
                case "Fireball":
                    other.GetComponent<IDamageable>().GetDamaged(abilitySO.damage);
                    Destroy(this.gameObject);
                    break;
                case "Congelar":
                    Destroy(this.gameObject);
                    break;
                case "Control Mental":
                    Destroy(this.gameObject);
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

}
