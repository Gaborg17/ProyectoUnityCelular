using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private EnemySO enemySO;
    private float health;
    private float attackCooldown;


    private void Start()
    {
        health = enemySO.enemyHealth;
        attackCooldown = enemySO.attackCooldown;
    }


    public void GetDamaged(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            OnDeath();
        }
    }

    public void OnDeath()
    {
        Destroy(this.gameObject);
    }
}
