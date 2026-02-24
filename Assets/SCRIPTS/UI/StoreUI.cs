using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.SceneManagement;
using UnityEngine.U2D.Animation;

public class StoreUI : MonoBehaviour
{
    [SerializeField]private SpriteLibrary library;
    [SerializeField]private SpriteLibraryAsset[] spriteLibraryAssets;

    private void Start()
    {
        library.spriteLibraryAsset = spriteLibraryAssets[GameManager.Instance.spriteID];
    }

    public void SelectSprite(int spriteID)
    {
        library.spriteLibraryAsset = spriteLibraryAssets[spriteID];
        GameManager.Instance.spriteID = spriteID;
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }

}
