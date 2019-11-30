using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;

public class TentacularLikes : MonoBehaviour {

    #region Public Variables
    //The restaurant's average rating
    [Range(1, 5)]
    public float likes = 0f;

    //The sum of all ratings given to the restaurant
    public float totalLikes = 0f;
   
    //The total number of customers
    public float totalCustomers = 0f;

    [HideInInspector]
    public static TentacularLikes Instance;
    #endregion

    #region Private Variables
    // UI text for the rating
    private Text likesText;
    #endregion

    private void Awake() {
        Instance = this;
    }

    #region Monobehavior Functions
    private void Start() {
        likesText = this.GetComponent<Text>();
    }

    private void Update() {
        likesText.text = likes.ToString("F2", CultureInfo.InvariantCulture);
    }
    #endregion

    #region Script Specific Functions
    /// <summary>
    /// Calculates the average rating
    /// </summary>
    public void CalculateLikeValue() {
        likes = totalLikes / totalCustomers;
    }
    #endregion
}
