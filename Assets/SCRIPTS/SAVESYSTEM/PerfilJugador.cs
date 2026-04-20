using UnityEngine;

[System.Serializable]
public class PerfilJugador
{
    public int storeCoins;

    [Header("Estadisticas Logros")]
    public int gemasGastadas;
    public int enemigosDerrotados;
    public int distanciaTotal;
    public int heartsCollected;
    public int timesPlayed;

    public int spriteID;
    public int wandID;

    
    public bool[] storeItemsIsPurchased;
    
    public bool[] wandItemIsPurchased;

    
    public bool[] rewardRequirementsIsCompleted;

    [Range(0.0001f, 1f)]
    public float sfxsVolume;
    [Range(0.0001f, 1f)]
    public float musicVolume;
    [Range(0.0001f, 1f)]
    public float masterVolume;


    public PerfilJugador()
    {
        if (GameManager.Instance != null && AudioManager.Instance != null && RewardsSystem.Instance != null)
        {
            storeCoins = GameManager.Instance.storeCoins;
            gemasGastadas = GameManager.Instance.gemasGastadas;
            enemigosDerrotados = GameManager.Instance.enemigosDerrotados;
            distanciaTotal = GameManager.Instance.distanciaTotal;
            heartsCollected = GameManager.Instance.heartsCollected;
            timesPlayed = GameManager.Instance.timesPlayed;

            spriteID = GameManager.Instance.spriteID;
            wandID = GameManager.Instance.wandID;

            storeItemsIsPurchased = new bool[GameManager.Instance.storeItems.Length];
            for (int i = 0; i < GameManager.Instance.storeItems.Length; i++)
            {
                storeItemsIsPurchased[i] = GameManager.Instance.storeItems[i].isPurchased;
            }
            wandItemIsPurchased = new bool[GameManager.Instance.wandItem.Length];
            for (int i = 0; i < GameManager.Instance.wandItem.Length; i++)
            {
                wandItemIsPurchased[i] = GameManager.Instance.wandItem[i].isPurchased;
            }

            rewardRequirementsIsCompleted = new bool[RewardsSystem.Instance.rewardRequirements.Length];
            for (int i = 0; i < RewardsSystem.Instance.rewardRequirements.Length; i++)
            {
                rewardRequirementsIsCompleted[i] = RewardsSystem.Instance.rewardRequirements[i].completed;
            }

            sfxsVolume = AudioManager.Instance.sfxsVolume;
            musicVolume = AudioManager.Instance.musicVolume;
            masterVolume = AudioManager.Instance.masterVolume;


        }
    }
}
