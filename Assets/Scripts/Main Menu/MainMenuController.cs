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
    public GameObject loadingScreen;
    public float loadingScreenDuration = 3f; // The duration in seconds
    public int maxTouchCount = 4; // The maximum touch count before hiding the howToPlayImage

    private int currentHowToPlaySpriteIndex;
    private int currentCreditsSpriteIndex;
    private bool canChangeSprite = true;
    private int touchCounter = 0;

    private void Start()
    {
        currentHowToPlaySpriteIndex = 0;
        currentCreditsSpriteIndex = 0;
        howToPlayImage.SetActive(false);
    }

    private void Update()
    {
        if (Input.touchCount > 0 && canChangeSprite)
        {
            // Increment the sprite index and display the next sprite
            if (howToPlaySprites.Length > 0)
            {
                currentHowToPlaySpriteIndex = (currentHowToPlaySpriteIndex + 1) % howToPlaySprites.Length;
                howToPlayImage.GetComponent<Image>().sprite = howToPlaySprites[currentHowToPlaySpriteIndex];
                touchCounter++;
                canChangeSprite = false;

                // Start a coroutine to delay the next sprite change
                StartCoroutine(EnableSpriteChange());
            }
        }

        if (touchCounter >= maxTouchCount)
        {
            howToPlayImage.SetActive(false);
            touchCounter = 0;
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
            canChangeSprite = true;
            touchCounter = 0;
        }
    }

    public void ShowCredits()
    {
        if (creditsSprites.Length > 0)
        {
            currentCreditsSpriteIndex = 0;
            howToPlayImage.GetComponent<Image>().sprite = creditsSprites[currentCreditsSpriteIndex];
            howToPlayImage.SetActive(true);
            canChangeSprite = false;
            touchCounter = 0;
        }
    }

    private IEnumerator DisableLoadingScreen()
    {
        yield return new WaitForSeconds(loadingScreenDuration);
        loadingScreen.SetActive(false);
    }

    private IEnumerator EnableSpriteChange()
    {
        yield return new WaitForSeconds(0.5f); // Delay between sprite changes
        canChangeSprite = true;
    }
}
