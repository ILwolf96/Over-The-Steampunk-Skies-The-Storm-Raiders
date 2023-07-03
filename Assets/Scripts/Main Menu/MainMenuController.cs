using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public string gameSceneName;
    public Sprite[] howToPlaySprites;
    public Sprite[] creditsSprites;
    public GameObject howToPlayImage;
    public GameObject creditsImage;
    public GameObject loadingScreen;
    public float loadingScreenDuration = 3f; // The duration in seconds
    public int maxHowToPlayTouchCount = 4; // The maximum touch count for How To Play button
    public int maxCreditsTouchCount = 4; // The maximum touch count for Credits button

    private int currentHowToPlaySpriteIndex;
    private int currentCreditsSpriteIndex;
    private bool canChangeHowToPlaySprite = true;
    private bool canChangeCreditsSprite = true;
    private int howToPlayTouchCounter = 0;
    private int creditsTouchCounter = 0;

    private void Start()
    {
        currentHowToPlaySpriteIndex = 0;
        currentCreditsSpriteIndex = 0;
        howToPlayImage.SetActive(false);
        creditsImage.SetActive(false);
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            if (canChangeHowToPlaySprite)
            {
                // Increment the sprite index and display the next sprite for How To Play
                if (howToPlaySprites.Length > 0)
                {
                    howToPlayImage.GetComponent<Image>().sprite = howToPlaySprites[currentHowToPlaySpriteIndex];
                    howToPlayTouchCounter++;
                    canChangeHowToPlaySprite = false;
                    currentHowToPlaySpriteIndex = (currentHowToPlaySpriteIndex + 1) % howToPlaySprites.Length;

                    // Start a coroutine to delay the next sprite change for How To Play
                    StartCoroutine(EnableHowToPlaySpriteChange());
                }
            }
            else if (canChangeCreditsSprite)
            {
                // Increment the sprite index and display the next sprite for Credits
                if (creditsSprites.Length > 0)
                {
                    creditsImage.GetComponent<Image>().sprite = creditsSprites[currentCreditsSpriteIndex];
                    creditsTouchCounter++;
                    canChangeCreditsSprite = false;
                    currentCreditsSpriteIndex = (currentCreditsSpriteIndex + 1) % creditsSprites.Length;

                    // Start a coroutine to delay the next sprite change for Credits
                    StartCoroutine(EnableCreditsSpriteChange());
                }
            }
        }

        if (howToPlayTouchCounter >= maxHowToPlayTouchCount && howToPlayImage.activeSelf)
        {
            howToPlayImage.SetActive(false);
            howToPlayTouchCounter = 0;
        }

        if (creditsTouchCounter >= maxCreditsTouchCount && creditsImage.activeSelf)
        {
            creditsImage.SetActive(false);
            creditsTouchCounter = 0;
        }
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
            currentHowToPlaySpriteIndex = 0;
            howToPlayImage.GetComponent<Image>().sprite = howToPlaySprites[currentHowToPlaySpriteIndex];
            howToPlayImage.SetActive(true);
            canChangeHowToPlaySprite = true;
            howToPlayTouchCounter = 0;
        }
    }

    public void ShowCredits()
    {
        if (creditsSprites.Length > 0)
        {
            currentCreditsSpriteIndex = 0;
            creditsImage.GetComponent<Image>().sprite = creditsSprites[currentCreditsSpriteIndex];
            creditsImage.SetActive(true);
            canChangeCreditsSprite = true;
            creditsTouchCounter = 0;
        }
    }

    private IEnumerator DisableLoadingScreen()
    {
        yield return new WaitForSeconds(loadingScreenDuration);
        loadingScreen.SetActive(false);
    }

    private IEnumerator EnableHowToPlaySpriteChange()
    {
        yield return new WaitForSeconds(0.5f); // Delay between sprite changes for How To Play
        canChangeHowToPlaySprite = true;
    }

    private IEnumerator EnableCreditsSpriteChange()
    {
        yield return new WaitForSeconds(0.5f); // Delay between sprite changes for Credits
        canChangeCreditsSprite = true;
    }
}
