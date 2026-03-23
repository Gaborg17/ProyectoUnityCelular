using TMPro;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.SceneManagement;
using UnityEngine.U2D.Animation;

[System.Serializable]
public class StoreItem
{
    public int itemObjectID;
    public bool isPurchased;
    public int priceToPurchase;
    public SpriteLibraryAsset spriteAsset;
}
public class StoreUI : MonoBehaviour
{
    [SerializeField]private SpriteLibrary library;

    [SerializeField] private TextMeshProUGUI coinCounter;
    [SerializeField] private TextMeshProUGUI[] prices;

    private void Start()
    {     
        library.spriteLibraryAsset = GameManager.Instance.storeItems[GameManager.Instance.spriteID].spriteAsset;
        SetPrices();
    }

    private void Update()
    {
        UpdateCoinCount();
    }

    public void SelectStoreItem(int spriteID)
    {
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
            }
        }




    }

    private void UpdateCoinCount()
    {
        coinCounter.text = GameManager.Instance.storeCoins.ToString();
    }

    public void ReturnToMenu()
    {
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
            }

        }

    }

}
