using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{ 
    public GameObject settingsMenuUI, controlsMenuUI, HighScoreUI;
    private HighScorePanel scorePanel;

    private void Start() {
        scorePanel = HighScoreUI.GetComponent<HighScorePanel>();
    }

    public void PlayGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("DeliveryTest");
    }

    public void HighScoreON() {
        HighScoreUI.SetActive(true);
        scorePanel.SetScoreText();
    }

    public void HighScoreOff() {
        HighScoreUI.SetActive(false);
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