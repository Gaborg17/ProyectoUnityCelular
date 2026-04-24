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

    [SerializeField] private Transform abilitySpawn;



    [SerializeField] private TextMeshProUGUI priceCA;
    [SerializeField] private TextMeshProUGUI priceEA;

    private GameObject player;
    private PlayerMovement pM;
    private PlayerHealthHandler pH;

    public float timer;
    public float durationMultiplier;

    public int gemsToChange;
    public int gemsToExtend;


    [SerializeField] private GameObject wand;
    [SerializeField] private GameObject sword;
    [SerializeField] private GameObject[] slashes;
    private void OnValidate()
    {
        buttonSprite.color = currentAbility.tempColor;
    }

    private void Start()
    {
        StopCooldown();
        pH = FindAnyObjectByType<PlayerHealthHandler>();
        player = GameObject.FindWithTag("Player");
        pM = player.GetComponent<PlayerMovement>();
        StartCoroutine(ChangeAbility());
        priceCA.text = gemsToChange.ToString();
        priceEA.text = gemsToExtend.ToString();
    }

    private void Update()
    {
        if (currentAbility.name == "ShieldA")
        {

            if (pH != null)
            {
                pH.isProtected = true;
            }
        }

        if (currentAbility.name != "ShieldA")
        {

            if (pH != null)
            {
                pH.isProtected = false;
            }

            if (currentAbility.name == "SwordA")
            {
                sword.SetActive(true);
                wand.SetActive(false);
            }

            if (currentAbility.name != "SwordA")
            {
                sword.SetActive(false);
                wand.SetActive(true);
            }
        }

        if (!AbilitySO.isOnCooldown)
        {
            slashes[0].gameObject.SetActive(false);
            slashes[1].gameObject.SetActive(false);
        }

    }
    public void UseActiveAbility()
    {
        if (currentAbility != null && !AbilitySO.isOnCooldown)
        {
            currentAbility.Activate(abilitySpawn, pM.direction);
            StartCoroutine(currentAbility.Cooldown());
            pM.actualSpeed = pM.walkSpeed * currentAbility.playerSpeedMultiplier;
            if (currentAbility.name == "SwordA")
            {
                if (pM.lookingLeft == true)
                {
                    slashes[0].gameObject.SetActive(true);
                }
                if (pM.lookingLeft == false)
                {
                    slashes[1].gameObject.SetActive(true);
                }
            }
        }
        else
        {
            Debug.Log("Ability in Cooldown:" + currentAbility.name);
            slashes[0].gameObject.SetActive(false);
            slashes[1].gameObject.SetActive(false);

        }
    }


    public IEnumerator ChangeAbility()
    {
        AbilitySO.isOnCooldown = false;
        currentAbility = nextAbility;
        int abiltyNum;
        do
        {
            abiltyNum = RandomNumber();
        }
        while (availableAbilities[abiltyNum] == currentAbility);

        nextAbility = availableAbilities[abiltyNum];
        timer = abilityChangeTimer;

        buttonSprite.color = currentAbility.tempColor;
        buttonSpriteImg.sprite = currentAbility.icon;

        nButtonSprite.color = nextAbility.tempColor;
        nButtonSpriteImg.sprite = nextAbility.icon;



        if (!AbilitySO.isOnCooldown)
        {
            pM.actualSpeed = pM.walkSpeed;
        }



        while (timer > 0)
        {
            timer -= Time.deltaTime;
            timerImg.fillAmount = timer / 10;
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
        if (GameManager.Instance.gemasDeRonda >= gemsToChange)
        {
            AudioManager.Instance.Play("CambiarPoder");
            RewardsSystem.Instance.AddGemsSpent(gemsToChange);
            timer = 0;
            GameManager.Instance.gemasDeRonda -= gemsToChange;
            gemsToChange *= 2;
            priceCA.text = gemsToChange.ToString();
        }

    }



    public void PayToExtend()
    {
        if (GameManager.Instance.gemasDeRonda >= gemsToExtend)
        {
            AudioManager.Instance.Play("ReiniciarTiempo");
            RewardsSystem.Instance.AddGemsSpent(gemsToExtend);
            timer = abilityChangeTimer * durationMultiplier;
            GameManager.Instance.gemasDeRonda -= gemsToExtend;
            gemsToExtend *= 2;
            priceEA.text = gemsToExtend.ToString();
        }
    }

}
