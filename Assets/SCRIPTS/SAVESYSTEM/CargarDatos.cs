using UnityEngine;

using UnityEngine.ProBuilder.Shapes;

public class CargarDatos : MonoBehaviour
{
    private PerfilJugador perfil;

    private void Start()
    {
        InitializeData();
    }

    public void InitializeData()
    {
        perfil = SaveSystem.CargarPartida();
        Debug.Log("Cargando Informacion");
        if(perfil != null)
        {
            Debug.Log("Asaaaaaa");
            GameManager.Instance.storeCoins = perfil.storeCoins;
            GameManager.Instance.gemasGastadas = perfil.gemasGastadas;
            GameManager.Instance.enemigosDerrotados = perfil.enemigosDerrotados;
            GameManager.Instance.distanciaTotal = perfil.distanciaTotal;
            GameManager.Instance.heartsCollected = perfil.heartsCollected;
            GameManager.Instance.timesPlayed = perfil.timesPlayed;

            GameManager.Instance.spriteID = perfil.spriteID;
            GameManager.Instance.wandID = perfil.wandID;



            for (int i = 0; i < GameManager.Instance.storeItems.Length; i++)
            {
                GameManager.Instance.storeItems[i].isPurchased = perfil.storeItemsIsPurchased[i];
            }

            for (int i = 0; i < GameManager.Instance.wandItem.Length; i++)
            {
                GameManager.Instance.wandItem[i].isPurchased = perfil.wandItemIsPurchased[i];
            }


            for (int i = 0; i < RewardsSystem.Instance.rewardRequirements.Length; i++)
            {
                RewardsSystem.Instance.rewardRequirements[i].completed = perfil.rewardRequirementsIsCompleted[i];
            }

            AudioManager.Instance.sfxsVolume = perfil.sfxsVolume;
            AudioManager.Instance.musicVolume = perfil.musicVolume;
            AudioManager.Instance.masterVolume = perfil.masterVolume;
        }
    }
}
