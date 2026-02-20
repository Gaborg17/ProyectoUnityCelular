using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;


    public int gemasTotales;
    public int tiempoDeLaRonda;
    public int mejorTiempo;

    public int distanciaDeLaRonda;
    public int mejorDistancia;

    public bool gameOver = false;





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
