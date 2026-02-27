using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    public EnemySO enemySO;
    private float health;
    private float attackCooldown;

    [SerializeField] private Transform player;
    [SerializeField] private Rigidbody rbE;

    public Transform targetPosition;

    public bool frozen;
    public bool mindControl;
    public bool followingTarget = false;

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
        rbE.isKinematic = false;
        rbE.linearVelocity = Vector3.zero;
        health = enemySO.enemyHealth;
        attackCooldown = enemySO.attackCooldown;
        targetPosition = player;
        frozen = false;
        mindControl = false;
    }


    private void FixedUpdate()
    {

        OutOfRangeCheck();
        if (!frozen)
        {
            rbE.isKinematic = false;
            Flip();
            MoveToTarget();

            if (mindControl)
            {
                UnderMindControl();

            }

        }

        else if (frozen)
        {
            rbE.isKinematic = true;
        }

    }
    private void MoveToTarget()
    {
        if(targetPosition != null && targetPosition.position.y >= (transform.position.y - 2))
        {
            followingTarget = true;
            Vector3 newPosition = newPosition = Vector3.MoveTowards(transform.position, new Vector3(targetPosition.position.x,0,0), enemySO.movementSpeed * Time.deltaTime);
            rbE.MovePosition(newPosition);


        }
        else
        {
            followingTarget = false;
        }

    }

    private void Flip()
    {
        if (targetPosition.position.x >= transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (targetPosition.position.x < transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
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






    public void GetDamaged(int damage)
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
