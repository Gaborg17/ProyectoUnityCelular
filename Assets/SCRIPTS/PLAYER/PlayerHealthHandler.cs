using UnityEngine;

public class PlayerHealthHandler : MonoBehaviour, IDamageable
{
    [SerializeField] private int actualHealth = 5;
    [SerializeField] private int maxHealth;

    public bool isProtected = false;

    private PlayerAnimManager animManager;
    private void Start()
    {
        actualHealth = maxHealth;
        animManager = GetComponent<PlayerAnimManager>();
    }

    public void GetDamaged(int damage)
    {
        if (isProtected == true)
        {
            return;
        }

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
