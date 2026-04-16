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
                reward.achievementSprite,        
                reward.name,        
                reward.description
            );
            }
            else
            {
                disp.SetData(
                reward.achievementSprite,
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
        AudioManager.Instance.Play("Tap");
        SceneManager.LoadScene("MenuPrincipal");
    }

}
