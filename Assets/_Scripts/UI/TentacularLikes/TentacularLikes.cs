using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;

public class TentacularLikes : MonoBehaviour
{
    [Range(1, 5)]
    public static float likes = 1f;

    public static float totalLikes = 1f;
    public static float totalCustomers = 1f;

    private Text likesText;

    private void Start() {
        likesText = this.GetComponent<Text>();
    }

    private void Update() {
        likesText.text = likes.ToString("F2", CultureInfo.InvariantCulture);
    }

    private void CalculateLikeValue() {
        likes = totalLikes / totalCustomers;
    }
}
