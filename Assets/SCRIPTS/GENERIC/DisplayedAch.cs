using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayedAch : MonoBehaviour
{
    [SerializeField] private Image achImage;
    [SerializeField] private Image completed;
    [SerializeField] private TextMeshProUGUI achName;
    [SerializeField] private TextMeshProUGUI achDesc;
    [SerializeField] private TextMeshProUGUI achProg;
    [SerializeField] private TextMeshProUGUI achReward;

    public void SetData(Sprite icon, string name, string description,
                        int current, int target, int reward)
    {
        achImage.sprite = icon;
        achName.text = name;
        achDesc.text = description;
        achProg.text = $"{current} / {target}";
        achReward.text = reward.ToString();
        completed.gameObject.SetActive(false);
    }
    public void SetDataCompleted(Sprite icon, string name, string description)
    {
        achImage.sprite = icon;
        achName.text = name;
        achDesc.text = description;
        achProg.gameObject.SetActive(false);
        achReward.gameObject.SetActive(false);
        completed.gameObject.SetActive(true);
    }
}
