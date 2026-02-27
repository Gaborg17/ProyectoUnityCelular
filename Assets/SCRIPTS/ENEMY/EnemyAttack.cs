using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private int attackDamage;
    [SerializeField] private float cooldown;

    [SerializeField] private float attackDistance;
    [SerializeField] private float attackForce;

    [SerializeField] private GameObject attackPrefab;

    private Enemy enemy;
    private EnemySO enemySO;

    private EnemyDamage eDamage;

    private Coroutine attackCoroutine;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
        enemySO = enemy.enemySO;
        eDamage = attackPrefab.GetComponent<EnemyDamage>();
        
    }

    private void Update()
    {
        if (enemy.followingTarget && !enemy.frozen)
        {
            ChangeTarget();
            DoAttack();
        }
    }


    private void AttackType()
    {
        switch (enemySO.type)
        {
            case EnemySO.TypeOfEnemy.AirEnemy:
                if (attackCoroutine == null)
                    attackCoroutine = StartCoroutine(AirAttack());

                break;
            case EnemySO.TypeOfEnemy.LandEnemy:
                if (attackCoroutine == null)
                    attackCoroutine = StartCoroutine(MeleeAttack());
                break;
        }
    }


    private void DoAttack()
    {
        float distance = Vector3.Distance(transform.position, enemy.targetPosition.position);

        if (distance <= attackDistance)
        {
            if (attackCoroutine == null)
                AttackType();
        }

        else if (distance > attackDistance)
        {
            if (attackCoroutine != null)
            {
                StopCoroutine(attackCoroutine);
                attackCoroutine = null;
            }
        }
    }

    private void ChangeTarget()
    {
        if (enemy.mindControl)
        {
            eDamage.tagToCollide = "Enemy";
        }

        else
        {
            eDamage.tagToCollide = "Player";
        }
    }


    private IEnumerator MeleeAttack()
    {
        attackPrefab.SetActive(true);
        yield return new WaitForSeconds(cooldown);
        attackCoroutine = null;
    }

    private IEnumerator AirAttack()
    {
        GameObject projectile = Instantiate(attackPrefab, this.transform.position, this.transform.rotation);
        eDamage = projectile.GetComponent<EnemyDamage>();
        Rigidbody rbP = projectile.GetComponent<Rigidbody>();
        Vector3 direction = (enemy.targetPosition.position - transform.position);
        rbP.AddForce(Vector3.right * direction.x * attackForce, ForceMode.Acceleration);

        yield return new WaitForSeconds(cooldown);
        attackCoroutine = null;
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

}
