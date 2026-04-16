using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D.Animation;
using UnityEngine.UI;

[System.Serializable]
public class StoreItem
{
    public int itemObjectID;
    public bool isPurchased;
    public int priceToPurchase;
    public SpriteLibraryAsset spriteAsset;
}

[System.Serializable]
public class Wands
{
    public int itemObjectID;
    public bool isPurchased;
    public int priceToPurchase;
    public Sprite spriteAsset;
}
public class StoreUI : MonoBehaviour
{
    [SerializeField]private SpriteLibrary library;
    [SerializeField]private SpriteRenderer sprite;

    [SerializeField] private TextMeshProUGUI coinCounter;
    [SerializeField] private TextMeshProUGUI[] prices;
    [SerializeField] private TextMeshProUGUI[] wandPrices;


    

    private void Start()
    {     
        library.spriteLibraryAsset = GameManager.Instance.storeItems[GameManager.Instance.spriteID].spriteAsset;
        SetPrices();
        SetWandPrices();
    }

    private void Update()
    {
        UpdateCoinCount();
    }

    public void SelectStoreItem(int spriteID)
    {
        AudioManager.Instance.Play("Tap");
        if (GameManager.Instance.storeItems[spriteID].isPurchased)
        {        
            GameManager.Instance.spriteID = spriteID;
            library.spriteLibraryAsset = GameManager.Instance.storeItems[spriteID].spriteAsset;
        }

        else if (!GameManager.Instance.storeItems[spriteID].isPurchased)
        {
            if (GameManager.Instance.storeCoins >= GameManager.Instance.storeItems[spriteID].priceToPurchase)
            {
                library.spriteLibraryAsset = GameManager.Instance.storeItems[spriteID].spriteAsset;
                GameManager.Instance.spriteID = spriteID;
                GameManager.Instance.storeCoins -= GameManager.Instance.storeItems[spriteID].priceToPurchase;
                GameManager.Instance.storeItems[spriteID].isPurchased = true;
                prices[spriteID].gameObject.SetActive(false);
                Image storeImg = prices[spriteID].gameObject.transform.parent.GetComponent<Image>();
                storeImg.color = Color.white;
            }
        }




    }


    public void SelectWand(int wandID)
    {
        AudioManager.Instance.Play("Tap");
        if (GameManager.Instance.wandItem[wandID].isPurchased)
        {
            GameManager.Instance.wandID = wandID;
            sprite.sprite = GameManager.Instance.wandItem[wandID].spriteAsset;
        }

        else if (!GameManager.Instance.wandItem[wandID].isPurchased)
        {
            if (GameManager.Instance.storeCoins >= GameManager.Instance.wandItem[wandID].priceToPurchase)
            {
                sprite.sprite = GameManager.Instance.wandItem[wandID].spriteAsset;
                GameManager.Instance.spriteID = wandID;
                GameManager.Instance.storeCoins -= GameManager.Instance.wandItem[wandID].priceToPurchase;
                GameManager.Instance.wandItem[wandID].isPurchased = true;
                wandPrices[wandID].gameObject.SetActive(false);
                Image storeImg = wandPrices[wandID].gameObject.transform.parent.GetComponent<Image>();
                storeImg.color = Color.white;
            }
        }
    }

    private void UpdateCoinCount()
    {
        coinCounter.text = GameManager.Instance.storeCoins.ToString();
    }

    public void ReturnToMenu()
    {
        AudioManager.Instance.Play("Tap");
        SceneManager.LoadScene("MenuPrincipal");
    }


    private void SetPrices()
    {
        for(int i = 0; i < GameManager.Instance.storeItems.Length; i++)
        {
            prices[i].text = GameManager.Instance.storeItems[i].priceToPurchase.ToString();
            if(GameManager.Instance.storeItems[i].isPurchased)
            {
                prices[i].gameObject.SetActive(false);
                Image storeImg = prices[i].gameObject.transform.parent.GetComponent<Image>();
                storeImg.color = Color.white;

            }

        }

    }


    private void SetWandPrices()
    {
        for (int i = 0; i < GameManager.Instance.wandItem.Length; i++)
        {
            wandPrices[i].text = GameManager.Instance.wandItem[i].priceToPurchase.ToString();
            if (GameManager.Instance.storeItems[i].isPurchased)
            {
                wandPrices[i].gameObject.SetActive(false);
                Image storeImg = wandPrices[i].gameObject.transform.parent.GetComponent<Image>();
                storeImg.color = Color.white;
            }

        }

    }

}
