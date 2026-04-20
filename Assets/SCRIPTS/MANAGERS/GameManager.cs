using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;


    public int gemasDeRonda;
    public int gemasParaRevivir;


    public int storeCoins;

    [Header("Estadisticas Logros")]
    public int gemasGastadas;
    public int enemigosDerrotados;
    public int distanciaTotal;
    public int heartsCollected;
    public int timesPlayed;



    public int tiempoDeLaRonda;
    public int mejorTiempo;

    public int distanciaDeLaRonda;
    public int mejorDistancia;

    public bool gameOver = false;
    public bool showRevive = false;

    public int spriteID;
    public int wandID;

    public StoreItem[] storeItems;
    public Wands[] wandItem;


    public bool showCompleted;
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


    public IEnumerator RewardCompleted()
    {
        showCompleted = true;
        yield return new WaitForSeconds(1);
        showCompleted = false;
    }

}
