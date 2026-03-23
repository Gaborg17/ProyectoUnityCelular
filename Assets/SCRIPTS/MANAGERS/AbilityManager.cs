using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityManager : MonoBehaviour
{
    [SerializeField] private AbilitySO currentAbility;
    [SerializeField] private AbilitySO nextAbility;

    public AbilitySO[] availableAbilities;

    [SerializeField] private Image buttonSprite;
    [SerializeField] private Image buttonSpriteImg;
    
    [SerializeField] private Image nButtonSprite;
    [SerializeField] private Image nButtonSpriteImg;


    [SerializeField] private Image timerImg;

    [SerializeField] private float abilityChangeTimer;

    [SerializeField]private Transform abilitySpawn;


    [SerializeField] private TextMeshProUGUI priceCA;
    [SerializeField] private TextMeshProUGUI priceEA;

    private GameObject player;
    private PlayerMovement pM;

    public float timer;
    public float durationMultiplier;

    public int gemsToChange;
    public int gemsToExtend;
    private void OnValidate()
    {
        buttonSprite.color = currentAbility.tempColor;
    }

    private void Start()
    {
        StopCooldown();
        player = GameObject.FindWithTag("Player");
        pM = player.GetComponent<PlayerMovement>();
        StartCoroutine(ChangeAbility());
        priceCA.text = gemsToChange.ToString();
        priceEA.text = gemsToExtend.ToString();
    }

    private void Update()
    {
        if (currentAbility.name == "ShieldA" && !AbilitySO.isOnCooldown)
        {
            UseActiveAbility();
        }

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
        currentAbility = nextAbility;

        nextAbility = availableAbilities[RandomNumber()];
        timer = abilityChangeTimer;
        
        buttonSprite.color = currentAbility.tempColor;
        buttonSpriteImg.sprite = currentAbility.icon;
        
        nButtonSprite.color = nextAbility.tempColor;
        nButtonSpriteImg.sprite = nextAbility.icon;
        abilitySpawn.gameObject.SetActive(false);
        if (!AbilitySO.isOnCooldown)
        {
            pM.actualSpeed = pM.walkSpeed;
        }



        while (timer > 0)
        {


            timer -= Time.deltaTime;
            timerImg.fillAmount = timer/10;
            yield return null;
        }
        
        StartCoroutine(ChangeAbility());
    }

    
    private int RandomNumber()
    {
        int randomNum = Random.Range(0, availableAbilities.Length);
        return randomNum;
    }


    public void StopCooldown()
    {
        StopCoroutine(currentAbility.Cooldown());
        AbilitySO.isOnCooldown = false;
        AbilitySO.counter = 0;
    }

    public void PayToChange()
    {
        if(GameManager.Instance.gemasDeRonda >= gemsToChange)
        {
            timer = 0;
            GameManager.Instance.gemasDeRonda -= gemsToChange;
            priceCA.text = gemsToChange.ToString();
        }
        
    }



    public void PayToExtend()
    {
        if (GameManager.Instance.gemasDeRonda >= gemsToExtend)
        {
            timer = abilityChangeTimer * durationMultiplier;
            GameManager.Instance.gemasDeRonda -= gemsToExtend;
            gemsToExtend *= 2;
            priceEA.text = gemsToExtend.ToString();
        }
    }

}
