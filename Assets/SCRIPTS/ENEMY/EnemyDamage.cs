using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public string tagToCollide;
    public int damage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagToCollide))
        {
            if(other.GetComponent<IDamageable>() != null)
            {
                other.GetComponent<IDamageable>().GetDamaged(damage);
                Debug.Log($"Dañando a {other.name}");
                Destroy(this.gameObject);
            }
        }

        else if (other.CompareTag("Ground"))
        {
            Destroy(this.gameObject);
        }

    }

}
