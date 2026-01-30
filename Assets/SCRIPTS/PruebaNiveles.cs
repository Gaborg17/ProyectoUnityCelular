using System.Collections;
using UnityEngine;

public class PruebaNiveles : MonoBehaviour
{
    private CheckVisibility visibility;

    private void Start()
    {
        visibility = GetComponent<CheckVisibility>();
    }

    private void Update()
    {
        if (visibility.IsVisible())
        {
            return;
        }

        else if (!visibility.IsVisible())
        {
            ObjectPooling oP = FindAnyObjectByType<ObjectPooling>();
            oP.ReturnToQueue(this.gameObject);
        }
    }
    
}
