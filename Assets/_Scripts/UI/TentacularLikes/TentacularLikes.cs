using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;

public class TentacularLikes : MonoBehaviour
{
    [Range(1, 5)]
    public static float likes = 0f;



    public static float totalLikes = 0f;
    public static float totalCustomers = 0f;

    private Text likesText;

    private void Start() {
        likesText = this.GetComponent<Text>();
    }

    private void Update() {
        likesText.text = likes.ToString("F2", CultureInfo.InvariantCulture);
    }

    public static void CalculateLikeValue() {
        likes = totalLikes / totalCustomers;
    }
}
