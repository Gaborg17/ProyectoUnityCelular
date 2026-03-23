using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;


    public int gemasDeRonda;
    public int gemasParaRevivir;


    public int storeCoins;

    [Header("Estadisticas Logros")]
    public int gemasTotales;
    public int enemigosDerrotados;
    public int distanciaTotal;


    public int tiempoDeLaRonda;
    public int mejorTiempo;

    public int distanciaDeLaRonda;
    public int mejorDistancia;

    public bool gameOver = false;
    public bool showRevive = false;

    public int spriteID;

    public StoreItem[] storeItems;

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
}
