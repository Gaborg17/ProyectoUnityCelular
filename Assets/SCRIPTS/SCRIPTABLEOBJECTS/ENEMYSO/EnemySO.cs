using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Enemy/EnemyData")]
public class EnemySO : ScriptableObject
{
    public float attackCooldown;
    public float enemyHealth;
    public TypeOfEnemy type;
    public float movementSpeed;

    public enum TypeOfEnemy
    {
        LandEnemy,AirEnemy
    }

}
