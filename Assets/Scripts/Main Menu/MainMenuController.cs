using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public string gameSceneName;
    public Sprite[] howToPlaySprites;
    public Sprite creditsSprite;
    public Image howToPlayImage;

    public GameObject loadingScreen;

    public void StartGame()
    {
        //// Activate the loading screen
        //loadingScreen.SetActive(true);

        // Load the game scene
        SceneManager.LoadScene(gameSceneName);
    }

    public void ShowHowToPlay()
    {
        // Display a random sprite from the howToPlaySprites array
        int randomIndex = Random.Range(0, howToPlaySprites.Length);
        howToPlayImage.sprite = howToPlaySprites[randomIndex];
    }

    public void ShowCredits()
    {
        howToPlayImage.sprite = creditsSprite;
    }
}
