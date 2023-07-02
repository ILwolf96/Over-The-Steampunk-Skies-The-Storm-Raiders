using System.Collections; // Add this line
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public string gameSceneName;
    public Sprite[] howToPlaySprites;
    public Sprite[] creditsSprites;
    public Image howToPlayImage;

    public GameObject loadingScreen;
    public float loadingScreenDuration = 3f; // The duration in seconds

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

        // Start the coroutine to disable the loading screen after the specified duration
        StartCoroutine(DisableLoadingScreen());

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
            howToPlayImage.sprite = creditsSprites[currentCreditsSpriteIndex];
            currentCreditsSpriteIndex = (currentCreditsSpriteIndex + 1) % creditsSprites.Length;
        }
    }

    private IEnumerator DisableLoadingScreen()
    {
        yield return new WaitForSeconds(loadingScreenDuration);
        loadingScreen.SetActive(false);
    }
}
