using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gemCounter;
    [SerializeField] private TextMeshProUGUI timer;
    [SerializeField] private TextMeshProUGUI mejorTiempo;
    [SerializeField] private TextMeshProUGUI tiempoRonda;
    [SerializeField] private TextMeshProUGUI distanciaRonda;
    [SerializeField] private TextMeshProUGUI mejorDistancia;

    [SerializeField] private RectMask2D healthBar;
    [SerializeField] private float healthBarPadding;


    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject gameoverMenu;
    [SerializeField] private GameObject reviveMenu;
    [SerializeField] private GameObject settingsMenu;

    public UnityEvent restart;
    public bool inPlay;

    private PlayerHealthHandler playerHealthHandler;

    private void Start()
    {
        playerHealthHandler = FindAnyObjectByType<PlayerHealthHandler>();
        StartCoroutine(GameTimer());
    }
    private void Update()
    {
        GemCounterUpdate();
        ShowRevive();
        GameOver();
        DistanceCounter();
        HealthBarUpdate();
    }


    private void HealthBarUpdate()
    {
        if (healthBar != null)
        {
            

            float valuePerHealth = (float)playerHealthHandler.actualHealth/playerHealthHandler.maxHealth;

            healthBarPadding = 640 * (1 - valuePerHealth);


            healthBar.padding = new Vector4(0, 0, healthBarPadding, 0);
        }
    }


    private void GemCounterUpdate()
    {
        gemCounter.text = GameManager.Instance.gemasDeRonda.ToString();
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
        SceneManager.LoadScene("Game");
        GameManager.Instance.gameOver = false;
        Time.timeScale = 1f;
        GameManager.Instance.distanciaDeLaRonda = 0;
        GameManager.Instance.gemasDeRonda = 0;
        RewardsSystem.Instance.AddPlayAmount(1);
        
    }


    public void ReviveButton()
    {
        if (GameManager.Instance.gemasDeRonda >= GameManager.Instance.gemasParaRevivir)
        {
            RewardsSystem.Instance.AddGemsSpent(GameManager.Instance.gemasParaRevivir);
            reviveMenu.SetActive(false);
            playerHealthHandler.actualHealth = playerHealthHandler.maxHealth;
            PlayerAnimManager animMngr = FindAnyObjectByType<PlayerAnimManager>();
            animMngr.Death(false);
            Time.timeScale = 1f;
            GameManager.Instance.showRevive = false;
            CameraMovement camMov = FindAnyObjectByType<CameraMovement>();
            camMov.SetToPlayerPos();
        }
    }

    public void ShowRevive()
    {
        if(GameManager.Instance.showRevive == true)
        {
            reviveMenu.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void GiveUp()
    {
        GameManager.Instance.showRevive = false;
        RewardsSystem.Instance.AddDistance(GameManager.Instance.distanciaDeLaRonda);
        reviveMenu.SetActive(false);

        GameManager.Instance.gameOver = true;
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


    public void OpenSettings()
    {
        settingsMenu.SetActive(true);
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene("MenuPrincipal");
        GameManager.Instance.gameOver = false;
        GameManager.Instance.distanciaDeLaRonda = 0;
        GameManager.Instance.gemasDeRonda = 0;
        Time.timeScale = 1f;
        Debug.Log("SaliendoAlmenu");
    }
}
