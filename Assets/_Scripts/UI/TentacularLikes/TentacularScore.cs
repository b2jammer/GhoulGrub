using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class TentacularScore : MonoBehaviour
{
    #region Public Variables
    
    public static TentacularScore Instance;
    public int maxLostMeals = 15;

    public int CompletedMeals {
        get {
            return completedMeals;
        }
    }

    public int LostMeals {
        get {
            return lostMeals;
        }
    }
    #endregion

    #region Private Variables
    // UI text for the rating
    private Text scoreText;
    private float remainingTimeScore;
    private int completedMeals;
    private int lostMeals;

    #endregion

    private void Awake() {
        Instance = this;
        remainingTimeScore = 0f;
        completedMeals = 0;
        lostMeals = 0;
    }

    private void Update() {
        CheckLostMealsForLoss();
    }

    #region Monobehavior Functions
    private void Start() {
        scoreText = this.GetComponent<Text>();
        scoreText.text = "0";
    }
    #endregion

    #region Script Specific Functions
    /// <summary>
    /// Calculates the average rating
    /// </summary>
    public void UpdateText(float remainingTime) {
        remainingTimeScore += Mathf.Round(remainingTime * 100f);
        scoreText.text = remainingTimeScore.ToString();
    }

    public void UpdateCompletedMeals() {
        completedMeals++;
    }

    public void UpdateLostMeals() {
        lostMeals++;
    }

    public void CheckLostMealsForLoss() {
        if (lostMeals >= maxLostMeals) {
            GameManager.Instance.EndGame();
        }
    }
    #endregion
}
