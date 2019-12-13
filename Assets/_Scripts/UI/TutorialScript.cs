using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{ 
    // Tracks whether the game is paused or not.
public static bool GameIsPaused = true;

    public GameObject TutorialOne, TutorialTwo, TutorialThree, TutorialFour, TutorialFive;

    public static bool TutorialContinued = false;

    public void Start()
    {
        Time.timeScale = 0f;
    }

    public void Pause()
    {
    Time.timeScale = 0f;
    GameIsPaused = true;
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void TutorialOneON()
    {
        TutorialOne.SetActive(true);
    }

    public void TutorialOneOFF()
    {
        TutorialOne.SetActive(false);
    }

    public void TutorialTwoON()
    {
        TutorialTwo.SetActive(true);
    }

    public void TutorialTwoOFF()
    {
        TutorialTwo.SetActive(false);
    }

    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            TutorialThree.SetActive(true);
            Time.timeScale = 0f;
            GameIsPaused = true;
        }
    }

    public void TutorialThreeON()
    {
        TutorialThree.SetActive(true);
    }

    public void TutorialThreeOFF()
    {
        TutorialThree.SetActive(false);
    }

    public void TutorialFourON()
    {
        TutorialFour.SetActive(true);
    }

    public void TutorialFourOFF()
    {
        TutorialFour.SetActive(false);
    }

    public void TutorialFiveON()
    {
        TutorialFive.SetActive(true);
    }

    public void TutorialFiveOFF()
    {
        TutorialFive.SetActive(false);
    }
}
