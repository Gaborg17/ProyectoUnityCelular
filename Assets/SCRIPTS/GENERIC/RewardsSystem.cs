using UnityEngine;


public enum RewardType
{
    None, EnemyKill, DistanceReached, GemsSpent, HealAmount, TimesPlayed, GemsCollected
}
[System.Serializable]
public class RewardRequirements
{
    public RewardType rewardType;
    public string name;
    public string description;
    public int target;
    public int rewardProgress;
    public bool completed;
    public int rewardAmount;
    public Sprite achievementSprite;

}

public class RewardsSystem : MonoBehaviour
{
    [SerializeField] public RewardRequirements[] rewardRequirements;

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
        int currentHearts = GameManager.Instance.heartsCollected;
        int currentGemsCollected = GameManager.Instance.gemasDeRonda;
        int currentGemsSpent = GameManager.Instance.gemasGastadas;
        int currentTimesPlayed = GameManager.Instance.timesPlayed;


        CheckAndGrantRewards(RewardType.EnemyKill, currentEnemies);
        CheckAndGrantRewards(RewardType.DistanceReached, currentDistance);
        CheckAndGrantRewards(RewardType.TimesPlayed, currentTimesPlayed);
        CheckAndGrantRewards(RewardType.HealAmount, currentHearts);
        CheckAndGrantRewards(RewardType.GemsSpent, currentGemsSpent);
        CheckAndGrantRewards(RewardType.GemsCollected, currentGemsCollected);
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

    public void AddPlayAmount(int amount)
    {
        GameManager.Instance.timesPlayed += amount;
        CheckAndGrantRewards(RewardType.TimesPlayed, GameManager.Instance.timesPlayed);
    }

    public void AddHealTimes(int amount)
    {
        GameManager.Instance.heartsCollected += amount;
        CheckAndGrantRewards(RewardType.HealAmount, GameManager.Instance.heartsCollected);
    }

    public void AddGemsSpent(int amount)
    {
        GameManager.Instance.gemasGastadas += amount;
        CheckAndGrantRewards(RewardType.GemsSpent, GameManager.Instance.gemasGastadas);
    }

    public void AddGemsCollected(int amount)
    {
        GameManager.Instance.gemasDeRonda += amount;
        CheckAndGrantRewards(RewardType.GemsCollected, GameManager.Instance.gemasDeRonda);
    }



    private void CheckAndGrantRewards(RewardType type, int currentValue)
    {
        foreach (var reward in rewardRequirements)
        {
            if (reward.rewardType != type) continue;
            reward.rewardProgress = currentValue;

            while (currentValue >= reward.target && reward.completed == false)
            {

                GrantReward(reward);

            }
        }
    }


    private void GrantReward(RewardRequirements reward)
    {
        // Formatear la descripción con el objetivo recién completado

        string description = string.Format(reward.description, reward.target);
        reward.completed = true;

        Debug.Log($"¡Misión completada! {description} - Recompensa: {reward.rewardAmount}");

        GameManager.Instance.storeCoins += reward.rewardAmount;
        AudioManager.Instance.Play("Logro");
    }
}
