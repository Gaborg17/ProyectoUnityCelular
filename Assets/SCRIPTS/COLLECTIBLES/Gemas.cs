using UnityEngine;

public class Gemas : MonoBehaviour
{
    private ReturnToPool returner;
    private CheckVisibility visibility;
    private PlayerHealthHandler healthHandler;

    [SerializeField] private int gemValue;


    [SerializeField, Range(0,100)] private int probabilityToHeal;

    private void Start()
    {
        healthHandler = FindAnyObjectByType<PlayerHealthHandler>();
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
            OnCollected();
        }
    }


    private void OnCollected()
    {
        int probability = ProbabilityToHeal();

        if (probability > probabilityToHeal)
        {
            GameManager.Instance.gemasTotales += gemValue;
        }

        else
        {
            healthHandler.actualHealth += healthHandler.maxHealth;
        }
    }

    private int ProbabilityToHeal()
    {
        int randomNum = Random.Range(0, 100);
        return randomNum;
    }
}
