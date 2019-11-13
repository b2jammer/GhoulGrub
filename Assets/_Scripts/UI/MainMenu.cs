using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{ 
    public GameObject settingsMenuUI, controlsMenuUI;

    public void PlayGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Game");
    }

    public void SettingsMenuON()
    {
        settingsMenuUI.SetActive(true);
    }

    public void SettingsMenuOFF()
    {
        settingsMenuUI.SetActive(false);
    }

    public void ControlsMenuON()
    {
        controlsMenuUI.SetActive(true);
    }

    public void ControlsMenuOFF()
    {
        controlsMenuUI.SetActive(false);
    }

    public void CreditsMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Credits");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}