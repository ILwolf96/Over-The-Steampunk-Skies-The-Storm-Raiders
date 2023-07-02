using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public string gameSceneName;
    public Sprite[] howToPlaySprites;
    public Sprite[] creditsSprites;
    public Image howToPlayImage;
    public Image creditsImage;

    public GameObject loadingScreen;

    private int currentHowToPlaySpriteIndex;
    private int currentCreditsSpriteIndex;

    private void Start()
    {
        currentHowToPlaySpriteIndex = 0;
        currentCreditsSpriteIndex = 0;
    }

    public void StartGame()
    {
        // Activate the loading screen
        loadingScreen.SetActive(true);

        // Load the game scene
        SceneManager.LoadScene(gameSceneName);
    }

    public void ShowHowToPlay()
    {
        if (howToPlaySprites.Length > 0)
        {
            howToPlayImage.sprite = howToPlaySprites[currentHowToPlaySpriteIndex];
            currentHowToPlaySpriteIndex = (currentHowToPlaySpriteIndex + 1) % howToPlaySprites.Length;
        }
    }

    public void ShowCredits()
    {
        if (creditsSprites.Length > 0)
        {
            creditsImage.sprite = creditsSprites[currentCreditsSpriteIndex];
            currentCreditsSpriteIndex = (currentCreditsSpriteIndex + 1) % creditsSprites.Length;
        }
    }
}
