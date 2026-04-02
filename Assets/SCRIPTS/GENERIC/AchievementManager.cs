using UnityEngine;
using UnityEngine.SceneManagement;

public class AchievementManager : MonoBehaviour
{
    [SerializeField] private GameObject achievementDisplay;
    [SerializeField] private Transform content;


    void Start()
    {
        foreach(var reward in RewardsSystem.Instance.rewardRequirements)
        {
            GameObject achDisplayed = Instantiate(achievementDisplay, content);
            DisplayedAch disp = achDisplayed.GetComponent<DisplayedAch>();
            if (reward.completed)
            {
                disp.SetDataCompleted(
                null,        
                reward.name,        
                reward.description
            );
            }
            else
            {
                disp.SetData(
                null,
                reward.name,
                reward.description,
                reward.rewardProgress,
                reward.target,
                reward.rewardAmount
            );
            }
        }
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }

}
