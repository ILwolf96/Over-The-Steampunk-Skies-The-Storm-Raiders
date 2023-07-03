using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseWindow;
    [SerializeField]
    private Button pauseButton;
    [SerializeField]
    private Button resumeButton;
    [SerializeField]
    private Button returnButton;
    [SerializeField]
    private List<GameObject> gameManagers;

    private bool worldManagersEnabled = true;
    private bool canChangeHowToPlaySprite = true;
    private bool canChangeCreditsSprite = true;
    private int currentHowToPlaySpriteIndex;
    private int currentCreditsSpriteIndex;
    private int howToPlayTouchCounter = 0;
    private int creditsTouchCounter = 0;

    private void Start()
    {
        pauseButton.onClick.AddListener(PauseButtonPress);
        resumeButton.onClick.AddListener(ResumeButtonPress);
        returnButton.onClick.AddListener(() => ReturnToMainMenu("MainMenu"));
    }

    public void PauseButtonPress()
    {
        worldManagersEnabled = !worldManagersEnabled;
        pauseWindow.SetActive(worldManagersEnabled);

        foreach (GameObject gameManager in gameManagers)
        {
            DisableScripts(gameManager);
        }

        if (worldManagersEnabled)
        {
            HideHowToPlay();
            HideCredits();
        }
        else
        {
            ShowHowToPlay();
        }
    }

    public void ResumeButtonPress()
    {
        worldManagersEnabled = true;
        pauseWindow.SetActive(false);

        foreach (GameObject gameManager in gameManagers)
        {
            EnableScripts(gameManager);
        }

        HideHowToPlay();
        HideCredits();
    }

    public void ReturnToMainMenu(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void OnEnableWorldManagers()
    {
        worldManagersEnabled = true;
        pauseWindow.SetActive(false);
    }

    private void DisableScripts(GameObject gameObject)
    {
        MonoBehaviour[] scripts = gameObject.GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour script in scripts)
        {
            script.enabled = false;
        }
    }

    private void EnableScripts(GameObject gameObject)
    {
        MonoBehaviour[] scripts = gameObject.GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour script in scripts)
        {
            script.enabled = true;
        }
    }

    private void ShowHowToPlay()
    {
        currentHowToPlaySpriteIndex = 0;
        canChangeHowToPlaySprite = true;
        howToPlayTouchCounter = 0;
    }

    private void HideHowToPlay()
    {
        canChangeHowToPlaySprite = false;
        howToPlayTouchCounter = 0;
    }

    private void ShowCredits()
    {
        currentCreditsSpriteIndex = 0;
        canChangeCreditsSprite = true;
        creditsTouchCounter = 0;
    }

    private void HideCredits()
    {
        canChangeCreditsSprite = false;
        creditsTouchCounter = 0;
    }

    private void Update()
    {
        if (worldManagersEnabled)
        {
            return;
        }

        if (Input.touchCount > 0)
        {
            if (canChangeHowToPlaySprite)
            {
                IncrementHowToPlaySprite();
            }
            else if (canChangeCreditsSprite)
            {
                IncrementCreditsSprite();
            }
        }

        if (howToPlayTouchCounter >= 4)
        {
            HideHowToPlay();
        }

        if (creditsTouchCounter >= 4)
        {
            HideCredits();
        }
    }

    private void IncrementHowToPlaySprite()
    {
        if (howToPlayTouchCounter < 4)
        {
            howToPlayTouchCounter++;
            canChangeHowToPlaySprite = false;
            currentHowToPlaySpriteIndex = (currentHowToPlaySpriteIndex + 1) % 4;

            // TODO: Display the next sprite for How To Play
        }

        StartCoroutine(EnableHowToPlaySpriteChange());
    }

    private void IncrementCreditsSprite()
    {
        if (creditsTouchCounter < 4)
        {
            creditsTouchCounter++;
            canChangeCreditsSprite = false;
            currentCreditsSpriteIndex = (currentCreditsSpriteIndex + 1) % 4;

            // TODO: Display the next sprite for Credits
        }

        StartCoroutine(EnableCreditsSpriteChange());
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
