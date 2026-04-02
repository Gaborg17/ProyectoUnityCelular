using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
        RewardsSystem.Instance.AddPlayAmount(1);
    }

    public void Store()
    {
        SceneManager.LoadScene("Store");
    }


    public void Settings()
    {
        SceneManager.LoadScene("Settings");
    }

    public void Achievements()
    {
        SceneManager.LoadScene("Achievements");
    }

    public void Exit()
    {
        Application.Quit();
    }

}
