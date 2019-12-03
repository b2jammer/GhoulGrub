using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class TentacularScore : MonoBehaviour
{
    #region Public Variables
    private float remainingTimeScore;

    public static TentacularScore Instance;
    #endregion

    #region Private Variables
    // UI text for the rating
    private Text scoreText;
    #endregion

    private void Awake() {
        Instance = this;
        remainingTimeScore = 0f;
    }

    #region Monobehavior Functions
    private void Start() {
        scoreText = this.GetComponent<Text>();
        scoreText.text = "0";
    }

    private void Update() {
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
    #endregion
}
