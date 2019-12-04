using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool gameEnded;
    private static GameManager instance;

    public static GameManager Instance {
        get {
            if (instance != null) {
                return instance;
            }
            else {
                instance = GameObject.FindObjectOfType<GameManager>();

                if (instance == null) {
                    GameObject manager = new GameObject();
                    instance = manager.AddComponent<GameManager>();
                }

                return instance;
            }
        }
    }

    private void Awake() {
        gameEnded = false;
    }

    public void EndGame() {
        if (gameEnded) {
            Time.timeScale = 0f;
            // Display Score/rating and completed meals
            // Present a small menu that allows the user to restart or go back to the main menu
            // Store the score?
        }
    }
}
