using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AbilityManager : MonoBehaviour
{
    [SerializeField] private AbilitySO currentAbility;

    public AbilitySO[] availableAbilities;

    [SerializeField] private Image buttonSprite;

    [SerializeField] private float abilityChangeTimer;

    private void OnValidate()
    {
        buttonSprite.color = currentAbility.tempColor;
    }

    private void Start()
    {
        StartCoroutine(ChangeAbility());
    }

    private void Update()
    {

    }
    public void UseActiveAbility()
    {
        if (currentAbility != null && !AbilitySO.isOnCooldown)
        {
            currentAbility.Activate(gameObject);
            StartCoroutine(currentAbility.Cooldown());
        }
        else
        {
            Debug.Log("Ability in Cooldown:" + currentAbility.name);
        }
    }

    
    public IEnumerator ChangeAbility()
    {
        yield return new WaitForSeconds(abilityChangeTimer);
        currentAbility = availableAbilities[RandomNumber()];
        buttonSprite.color = currentAbility.tempColor;
        StartCoroutine(ChangeAbility());
    }

    private int RandomNumber()
    {
        int randomNum = Random.Range(0, availableAbilities.Length);
        return randomNum;
    }

}
