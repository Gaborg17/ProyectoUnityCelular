using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
        RewardsSystem.Instance.AddPlayAmount(1);
        AudioManager.Instance.Play("Tap");
    }

    public void Store()
    {
        AudioManager.Instance.Play("Tap");
        SceneManager.LoadScene("Store");
    }

    public void Creditos()
    {
        AudioManager.Instance.Play("Tap");
        SceneManager.LoadScene("Creditos");
    }

    public void Settings()
    {
        AudioManager.Instance.Play("Tap");
        SceneManager.LoadScene("Settings");
    }

    public void Achievements()
    {
        AudioManager.Instance.Play("Tap");
        SceneManager.LoadScene("Achievements");
    }

    public void Exit()
    {
        SaveSystem.GuardarPartida();
        AudioManager.Instance.Play("Tap");
        Application.Quit();
    }

}
