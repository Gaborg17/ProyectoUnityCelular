using UnityEngine;

public class Gemas : MonoBehaviour
{
    private ReturnToPool returner;
    private CheckVisibility visibility;

    [SerializeField] private int gemValue;

    private void Start()
    {
        visibility = GetComponent<CheckVisibility>();
        returner = GetComponent<ReturnToPool>();
    }


    private void Update()
    {
        if (visibility.IsVisible())
        {
            return;
        }
        else
        {
            returner.Return();
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            returner.Return();
            GameManager.Instance.gemasTotales += gemValue;
        }
    }
}
