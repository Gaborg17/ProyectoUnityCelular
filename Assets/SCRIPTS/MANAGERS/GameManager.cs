using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;


    public int gemasTotales;
    public int tiempoDeLaRonda;





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
