using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    public EnemySO enemySO;
    private float health;
    private float attackCooldown;

    [SerializeField] private Transform player;
    [SerializeField] private Rigidbody rbE;
    [SerializeField] private bool death;

    public Transform targetPosition;

    public bool frozen;
    [SerializeField] private GameObject freezeEffect;
    public bool mindControl;
    public bool followingTarget = false;

    private ReturnToPool poolReturn;
    private CheckVisibility checkVisibility;
    private EnemyAnimationHandler eAnim;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rbE = GetComponent<Rigidbody>();
        health = enemySO.enemyHealth;
        attackCooldown = enemySO.attackCooldown;
        targetPosition = player;
        eAnim = GetComponent<EnemyAnimationHandler>();
        checkVisibility = GetComponent<CheckVisibility>();
        poolReturn = GetComponent<ReturnToPool>();
    }

    private void OnEnable()
    {
        
        rbE = GetComponent<Rigidbody>();
        rbE.isKinematic = false;
        rbE.WakeUp();
        rbE.linearVelocity = Vector3.zero;
        health = enemySO.enemyHealth;
        attackCooldown = enemySO.attackCooldown;
        targetPosition = player;
        frozen = false;
        mindControl = false;
        death = false;
        freezeEffect.SetActive(false);
        followingTarget = false;
        targetPosition = player;
    }

    private void OnDisable()
    {
        freezeEffect.SetActive(false);
        targetPosition = null;
        transform.position = Vector3.zero;
    }

    private void FixedUpdate()
    {

        OutOfRangeCheck();
        if (!frozen)
        {
            
            rbE.isKinematic = false;
            Flip();
            MoveToTarget();
            eAnim.MoveToTarget(followingTarget);
            if (mindControl)
            {
                UnderMindControl();

            }

        }

        else if (frozen)
        {
            eAnim.Freeze(frozen);
            rbE.isKinematic = true;
            freezeEffect.SetActive(true);
        }

    }
    private void MoveToTarget()
    {
        if(targetPosition != null && targetPosition.position.y >= (transform.position.y - enemySO.detectionHeight))
        {
            followingTarget = true;
            //Vector3 newPosition = newPosition = Vector3.MoveTowards(transform.position, new Vector3(targetPosition.position.x,0,0), enemySO.movementSpeed * Time.deltaTime);
            //rbE.MovePosition(newPosition);

            Vector3 direction = new Vector3(targetPosition.position.x - transform.position.x, 0, 0).normalized;
            rbE.linearVelocity = new Vector3(direction.x * enemySO.movementSpeed, rbE.linearVelocity.y, 0);

        }
        else
        {
            followingTarget = false;
            rbE.linearVelocity = new Vector3(0, rbE.linearVelocity.y, 0);
        }

    }

    private void Flip()
    {
        if(targetPosition == null)
        {
            return;
        }

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


    private void Animation()
    {
        


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
        death = true;
        eAnim.Death(death);
        ObjectPooling oP = FindAnyObjectByType<ObjectPooling>();
        oP.SpawnFromPool("Gema", this.transform.position);
        StartCoroutine(DeathDelay());
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

    private IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(.6f);
        poolReturn.Return();

    }

}
