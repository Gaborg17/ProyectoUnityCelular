using UnityEngine;
using UnityEngine.UI;

public class ShowComplete : MonoBehaviour
{
    private Image img;
    private void Start()
    {
        img = GetComponent<Image>();
    }
    private void Update()
    {
        if(GameManager.Instance.showCompleted == true)
        {
            img.enabled = true;
            this.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            img.enabled = false;
            this.transform.GetChild(0).gameObject.SetActive(false);
        }
        
    }
}
