using UnityEngine;

public class Gemas : MonoBehaviour
{
    private ReturnToPool returner;
    private CheckVisibility visibility;
    private PlayerHealthHandler healthHandler;

    [SerializeField] private GameObject[] model;

    [SerializeField] private int gemValue;


    [SerializeField, Range(0,100)] private int probabilityToHeal;

    private bool heals = false;

    private void Start()
    {
        healthHandler = FindAnyObjectByType<PlayerHealthHandler>();
        visibility = GetComponent<CheckVisibility>();
        returner = GetComponent<ReturnToPool>();
    }

    private void OnEnable()
    {
        int probability = ProbabilityToHeal();

        if (probability > probabilityToHeal)
        {
            heals = false;
            model[1].gameObject.SetActive(false);
            model[0].gameObject.SetActive(true);

        }

        else
        {
            heals = true;
            model[0].gameObject.SetActive(false);
            model[1].gameObject.SetActive(true);
        }
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

        if (heals == false)
        {
            
            RewardsSystem.Instance.AddGemsCollected(gemValue);
            AudioManager.Instance.Play("Gema");

        }

        else
        {
            healthHandler.actualHealth = healthHandler.maxHealth;
            AudioManager.Instance.Play("Gema");
            RewardsSystem.Instance.AddHealTimes(1);
        }
    }

    private int ProbabilityToHeal()
    {
        int randomNum = Random.Range(0, 100);
        return randomNum;
    }
}
