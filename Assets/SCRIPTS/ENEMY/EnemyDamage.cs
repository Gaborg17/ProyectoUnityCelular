using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public string tagToCollide;
    public int damage;

    [SerializeField]private AttackType typeAttack;

    private CheckVisibility checkVisibility;

    private void Start()
    {
        checkVisibility = GetComponent<CheckVisibility>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagToCollide))
        {
            if(other.GetComponent<IDamageable>() != null)
            {
                other.GetComponent<IDamageable>().GetDamaged(damage);
                Debug.Log($"Dañando a {other.name}");

                if (typeAttack == AttackType.Air)
                {
                    Destroy(this.gameObject);
                }

            }
        }

        else if (other.CompareTag("Ground"))
        {
            if (typeAttack == AttackType.Air)
            {
                Destroy(this.gameObject);
            }
        }

    }

    private void Update()
    {
        if(checkVisibility != null)
        {
            if (!checkVisibility.IsVisible())
            {
                Destroy(this.gameObject);
            }
        }
    }

    private enum AttackType
    {
        Melee, Air
    }

}
