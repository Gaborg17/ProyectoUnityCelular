using UnityEngine;

public class PlayerHealthHandler : MonoBehaviour, IDamageable
{
    [SerializeField] private int actualHealth = 5;
    [SerializeField] private int maxHealth;

    private PlayerAnimManager animManager;
    private void Start()
    {
        actualHealth = maxHealth;
        animManager = GetComponent<PlayerAnimManager>();
    }

    public void GetDamaged(int damage)
    {
        actualHealth -= damage;

        if(actualHealth <= 0)
        {
            //animation
            OnDeath();
        }
    }

    public void OnDeath()
    {
        GameManager.Instance.gameOver = true;
    }
}
