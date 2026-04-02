using UnityEngine;

public class Warning : MonoBehaviour
{
    private CheckVisibility visibility;

    [SerializeField] private GameObject warningObject;

    private void Start()
    {
        visibility = GetComponent<CheckVisibility>();
    }

    private void Update()
    {
        if (!visibility.IsVisible())
        { 
            warningObject.SetActive(true);
        }

        else
        {
            warningObject.SetActive(false);
        }
    }

}
