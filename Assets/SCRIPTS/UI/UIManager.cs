using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gemCounter;
    [SerializeField] private TextMeshProUGUI timer;
    [SerializeField] private TextMeshProUGUI mejorTiempo;
    [SerializeField] private TextMeshProUGUI tiempoRonda;
    [SerializeField] private TextMeshProUGUI distanciaRonda;
    [SerializeField] private TextMeshProUGUI mejorDistancia;




    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject gameoverMenu;

    public UnityEvent restart;
    public bool inPlay;

    private void Start()
    {
        StartCoroutine(GameTimer());
    }
    private void Update()
    {
        GemCounterUpdate();
        GameOver();
        DistanceCounter();
    }


    private void GemCounterUpdate()
    {
        gemCounter.text = GameManager.Instance.gemasTotales.ToString();
    }

    private IEnumerator GameTimer()
    {
        float time = 0;
        while (inPlay)
        {
            
            time += Time.deltaTime;
            GameManager.Instance.tiempoDeLaRonda = ((int)time);
            yield return null;
        }
    }

    private void DistanceCounter()
    {
        if (inPlay)
        {
            timer.text = $"{GameManager.Instance.distanciaDeLaRonda} m";
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
    }

    public void Continue()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }

    public void RestartGame()
    {
        
        restart?.Invoke();
        SceneManager.LoadScene(0);
        GameManager.Instance.gameOver = false;
        Time.timeScale = 1f;
        GameManager.Instance.distanciaDeLaRonda = 0;
    }

    public void GameOver()
    {
        if(GameManager.Instance.gameOver == true)
        {
            if (GameManager.Instance.tiempoDeLaRonda > GameManager.Instance.mejorTiempo)
            {
                GameManager.Instance.mejorTiempo = GameManager.Instance.tiempoDeLaRonda;
            }

            if (GameManager.Instance.distanciaDeLaRonda > GameManager.Instance.mejorDistancia)
            {
                GameManager.Instance.mejorDistancia = GameManager.Instance.distanciaDeLaRonda;
            }
            gameoverMenu.SetActive(true);
            Time.timeScale = 0f;
            tiempoRonda.text = $"Tiempo: {GameManager.Instance.tiempoDeLaRonda}";
            mejorTiempo.text = $"Mejor Tiempo: {GameManager.Instance.mejorTiempo}";
            distanciaRonda.text = $"Altura: {GameManager.Instance.distanciaDeLaRonda}m";
            mejorDistancia.text = $"Mejor Altura: {GameManager.Instance.mejorDistancia}m";


        }
    }

    public void ExitToMenu()
    {
        GameManager.Instance.distanciaDeLaRonda = 0;
        Time.timeScale = 1f;
        Debug.Log("SaliendoAlmenu");
    }
}
