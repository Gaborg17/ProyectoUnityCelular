using UnityEngine;

public class PlayerHealthHandler : MonoBehaviour, IDamageable
{
    public int actualHealth = 5;
    public int maxHealth;

    public bool isProtected = false;

    private PlayerAnimManager animManager;
    [SerializeField] private GameObject shieldAbility;

    private void Start()
    {
        actualHealth = maxHealth;
        animManager = GetComponent<PlayerAnimManager>();
    }
    private void Update()
    {
        shieldAbility.SetActive(isProtected);

    }

    public void GetDamaged(int damage)
    {
        if (isProtected == true)
        {
            return;
        }

        animManager.Damaged();
        actualHealth -= damage;
        AudioManager.Instance.Play("RecibirDaño");

        if (actualHealth <= 0)
        {
            animManager.Death(true);
            OnDeath();
        }
    }

    public void OnDeath()
    {

        GameManager.Instance.showRevive = true;
    }
}
