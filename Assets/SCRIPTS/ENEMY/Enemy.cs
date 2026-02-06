using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private EnemySO enemySO;
    private float health;
    private float attackCooldown;

    [SerializeField] private Transform player;
    [SerializeField] private Rigidbody rbE;

    public Transform targetPosition;

    public bool frozen;
    public bool mindControl;

    private ReturnToPool poolReturn;
    private CheckVisibility checkVisibility;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rbE = GetComponent<Rigidbody>();
        health = enemySO.enemyHealth;
        attackCooldown = enemySO.attackCooldown;
        targetPosition = player;
        checkVisibility = GetComponent<CheckVisibility>();
        poolReturn = GetComponent<ReturnToPool>();
    }

    private void OnEnable()
    {
        rbE = GetComponent<Rigidbody>();
        rbE.linearVelocity = Vector3.zero;
        health = enemySO.enemyHealth;
        attackCooldown = enemySO.attackCooldown;
        targetPosition = player;
    }


    private void Update()
    {
        OutOfRangeCheck();
        if (!frozen)
        {
            MoveToTarget();

            if (mindControl)
            {
                UnderMindControl();

            }

        }

    }
    private void MoveToTarget()
    {
        if(targetPosition != null && targetPosition.position.y >= (transform.position.y - 2))
        {
            Vector3 newPosition = newPosition = Vector3.MoveTowards(transform.position, new Vector3(targetPosition.position.x,0,0), enemySO.movementSpeed * Time.deltaTime);
            rbE.MovePosition(newPosition);
        }

    }


    private void UnderMindControl()
    {
        GameObject[] enemies;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 pos = transform.position;

        foreach (GameObject enemy in enemies)
        {
            if (enemy == this.gameObject) continue;

            if (!enemy.activeInHierarchy) continue;

            Vector3 diff = enemy.transform.position - pos;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = enemy;
                distance = curDistance;
            }
        }



        targetPosition = closest?.transform;
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
        poolReturn.Return();
        ObjectPooling oP = FindAnyObjectByType<ObjectPooling>();
        oP.SpawnFromPool("Gema", this.transform.position);
    }

    private void OutOfRangeCheck()
    {
        if (!checkVisibility.IsVisible())
        {
            poolReturn.Return();
            ObjectPooling oP = FindAnyObjectByType<ObjectPooling>();
            oP.SpawnFromPool("Gema", this.transform.position);
        }

    }

}
