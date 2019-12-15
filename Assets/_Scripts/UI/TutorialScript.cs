using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour {
    // Tracks whether the game is paused or not.
    public static bool GameIsPaused = true;

    public GameObject TutorialOne, TutorialTwo, TutorialThree, TutorialFour, TutorialFive;
    public PauseMenu pauseMenu;
    public Button menuButton;

    public static bool TutorialContinued = false;

    private int currentTutorial;
    private bool tutorialOnScreen;
    private bool tutorialFinished;

    public void Start() {
        Pause();
        tutorialFinished = false;
        ResumeTutorialControls();
        TutorialOneON();
    }

    private void SetButtonInteractability(bool interactability) {
        menuButton.interactable = interactability;
    }

    private void SetTutorialOnScreen(bool onScreen) {
        tutorialOnScreen = onScreen;
    }

    public void Pause() {
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Resume() {
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void ResumeStandardControls() {
        tutorialOnScreen = false;
        SetButtonInteractability(true);
        pauseMenu.SetIsRunningTutorial(false);
    }

    public void ResumeTutorialControls() {
        tutorialOnScreen = true;
        SetButtonInteractability(false);
        pauseMenu.SetIsRunningTutorial(true);
    }

    public void CurrentTutorialOff() {
        TutorialOff(currentTutorial);
    }

    private void TutorialOneON() {
        TutorialOne.SetActive(true);
        currentTutorial = 1;
    }

    private void TutorialOneOFF() {
        TutorialOne.SetActive(false);
    }

    private void TutorialTwoON() {
        TutorialTwo.SetActive(true);
        currentTutorial = 2;
    }

    private void TutorialTwoOFF() {
        TutorialTwo.SetActive(false);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.T) && !tutorialOnScreen && !tutorialFinished) {
            SwitchToNextTutorial();
            ResumeTutorialControls();
            Pause();
        }
    }

    private void TutorialOn(int tutorialID) {
        switch (tutorialID) {
            case 1:
                TutorialOneON();
                break;
            case 2:
                TutorialTwoON();
                break;
            case 3:
                TutorialThreeON();
                break;
            case 4:
                TutorialFourON();
                break;
            case 5:
                TutorialFiveON();
                break;
            default:
                break;
        }
    }

    private void TutorialOff(int tutorialID) {
        switch (tutorialID) {
            case 1:
                TutorialOneOFF();
                break;
            case 2:
                TutorialTwoOFF();
                break;
            case 3:
                TutorialThreeOFF();
                break;
            case 4:
                TutorialFourOFF();
                break;
            case 5:
                TutorialFiveOFF();
                break;
            default:
                break;
        }
    }

    public void SwitchToNextTutorial() {
        TutorialOff(currentTutorial);
        TutorialOn(currentTutorial + 1);
    }

    private void TutorialThreeON() {
        currentTutorial = 3;
        TutorialThree.SetActive(true);
    }

    private void TutorialThreeOFF() {
        TutorialThree.SetActive(false);
    }

    private void TutorialFourON() {
        currentTutorial = 4;
        TutorialFour.SetActive(true);
    }

    private void TutorialFourOFF() {
        TutorialFour.SetActive(false);
    }

    private void TutorialFiveON() {
        currentTutorial = 5;
        TutorialFive.SetActive(true);
    }

    private void TutorialFiveOFF() {
        tutorialFinished = true;
        TutorialFive.SetActive(false);
    }
}
