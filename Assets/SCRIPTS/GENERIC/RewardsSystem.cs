using UnityEngine;


public enum RewardType
{
    None, EnemyKill, DistanceReached, TimeAlive
}
[System.Serializable]
public class RewardRequirements
{
    public RewardType rewardType;
    public string description;
    public int initialTarget;
    public int increment;
    public int rewardProgress;

    public int rewardAmount;

    [SerializeField] public int currentTarget;
    [SerializeField] public int completions;


}

public class RewardsSystem : MonoBehaviour
{
    [SerializeField] private RewardRequirements[] rewardRequirements;

    public static RewardsSystem Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        int currentEnemies = GameManager.Instance.enemigosDerrotados;
        int currentDistance = GameManager.Instance.distanciaTotal;

        foreach (var reward in rewardRequirements)
        {
            reward.currentTarget = reward.initialTarget;
        }

        CheckAndGrantRewards(RewardType.EnemyKill, currentEnemies);
        CheckAndGrantRewards(RewardType.DistanceReached, currentDistance);
    }

    public void AddEnemyKill(int amount)
    {
        GameManager.Instance.enemigosDerrotados += amount;
        CheckAndGrantRewards(RewardType.EnemyKill, GameManager.Instance.enemigosDerrotados);
    }

    public void AddDistance(int amount)
    {
        GameManager.Instance.distanciaTotal += amount;
        CheckAndGrantRewards(RewardType.DistanceReached, GameManager.Instance.distanciaTotal);
    }

    //public void AddTime(int roundTime)
    //{
    //    totalTimeAccumulated += roundTime;
    //    CheckAndGrantRewards(RewardType.TimeAlive, totalTimeAccumulated);
    //}


    private void CheckAndGrantRewards(RewardType type, int currentValue)
    {
        foreach (var reward in rewardRequirements)
        {
            if (reward.rewardType != type) continue;


            while (currentValue >= reward.currentTarget)
            {
                
                GrantReward(reward);

                
                reward.currentTarget += reward.increment;
                reward.completions++;
            }
        }
    }


    private void GrantReward(RewardRequirements reward)
    {
        // Formatear la descripción con el objetivo recién completado
        int completedTarget = reward.currentTarget - reward.increment;
        string description = string.Format(reward.description, completedTarget);

        Debug.Log($"¡Misión completada! {description} - Recompensa: {reward.rewardAmount}");

        // Aquí aplicas la recompensa (ej. monedas, experiencia, etc.)
        GameManager.Instance.storeCoins += reward.rewardAmount;
    }
}
