using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;

public class TentacularLikes : MonoBehaviour {

    #region Public Variables
    //The restaurant's average rating
    [Range(1, 5)]
    public static float likes = 0f;

    //The sum of all ratings given to the restaurant
    public static float totalLikes = 0f;
   
    //The total number of customers
    public static float totalCustomers = 0f;
    #endregion

    #region Private Variables
    // UI text for the rating
    private Text likesText;
    #endregion

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
    public static void CalculateLikeValue() {
        likes = totalLikes / totalCustomers;
    }
    #endregion
}
