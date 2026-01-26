using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AbilityManager : MonoBehaviour
{
    [SerializeField] private AbilitySO currentAbility;

    public AbilitySO[] availableAbilities;

    [SerializeField] private Image buttonSprite;

    [SerializeField] private float abilityChangeTimer;

    [SerializeField]private Transform abilitySpawn;

    private GameObject player;
    private PlayerMovement pM;

    private void OnValidate()
    {
        buttonSprite.color = currentAbility.tempColor;
    }

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        pM = player.GetComponent<PlayerMovement>();
        StartCoroutine(ChangeAbility());
    }

    private void Update()
    {

    }
    public void UseActiveAbility()
    {
        if (currentAbility != null && !AbilitySO.isOnCooldown)
        {
            currentAbility.Activate(abilitySpawn, pM.direction);
            StartCoroutine(currentAbility.Cooldown());
            pM.actualSpeed = pM.walkSpeed * currentAbility.playerSpeedMultiplier;
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
        abilitySpawn.gameObject.SetActive(false);
        if (!AbilitySO.isOnCooldown)
        {
            pM.actualSpeed = pM.walkSpeed;
        }
        StartCoroutine(ChangeAbility());
    }

    private int RandomNumber()
    {
        int randomNum = Random.Range(0, availableAbilities.Length);
        return randomNum;
    }

}
