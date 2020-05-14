using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Tracks whether the game is paused or not.
    public static bool GameIsPaused = false;

    public GameObject pauseButton, pauseMenuUI, settingsMenuUI, controlsMenuUI, recipeBookUI;

    private KeyboardInterfaceController interfaceController;
    private bool isRunningTutorial;

    private void Start() {
        interfaceController = GameObject.FindObjectOfType<KeyboardInterfaceController>();
        isRunningTutorial = false;
    }

    private void Update() {
        if (!GameManager.Instance.GameEnded) {
            CheckForPauseMenuHotkey();
        }
    }

    public void SetIsRunningTutorial(bool tutorialStatus) {
        isRunningTutorial = tutorialStatus;
    }

    private void CheckForPauseMenuHotkey() {
        if (interfaceController.ActivatePauseMenu && !isRunningTutorial) {
            if (GameIsPaused) {
                Resume();
            }
            else {
                Pause();
            }
        }
    }

    public void Resume ()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void RestartGame()
    {
        GameManager.Instance.CheckScore();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //Loads current scene
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause ()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        GameManager.Instance.CheckScore();
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void SettingsMenuON()
    {
        settingsMenuUI.SetActive(true);
    }

    public void SettingsMenuOFF()
    {
        settingsMenuUI.SetActive(false);
    }

    public void RecipeBookON()
    {
        recipeBookUI.SetActive(true);
    }

    public void RecipeBookOFF()
    {
        recipeBookUI.SetActive(false);
    }

    public void ControlsMenuON()
    {
        controlsMenuUI.SetActive(true);
    }

    public void ControlsMenuOFF()
    {
        controlsMenuUI.SetActive(false);
    }

    public void QuitGame()
    {
        GameManager.Instance.CheckScore();
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
