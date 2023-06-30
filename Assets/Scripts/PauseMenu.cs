using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    public GameObject PauseButton;

    private bool isPaused = false;

    private void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
            PauseGame(true);
        }
    }

    public void PauseGame(bool PauseState)
    {
        isPaused = PauseState;

        if (isPaused)
        {
            PauseButton.gameObject.SetActive(false);
            gameObject.SetActive(true);
            Time.timeScale = 0f; // Disable time-based updates
                                 // Show the pause menu UI
                                 // You can use SetActive(true) on the canvas or panel GameObject to show the pause menu UI
        }
        else
        {
            PauseButton.gameObject.SetActive(true);
            gameObject.SetActive(false);
            Time.timeScale = 1f; // Resume time-based updates
                                 // Hide the pause menu UI
                                 // You can use SetActive(false) on the canvas or panel GameObject to hide the pause menu UI
        }
    }


    public void LoadCreditsScene()
    {
        // Load the credits scene
        SceneManager.LoadScene("CreditsScene");
    }

    public void ReturnToMainMenu()
    {
        // Load the main menu scene
        SceneManager.LoadScene("MainMenu");
    }
}
