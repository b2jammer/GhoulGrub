using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class EndGameScorePanel : MonoBehaviour
{
    private Text[] finalScoreTextboxes;

    private Text finalScore;
    private Text finalRating;
    private Text completedMeals;

    //private void Awake() {
        
    //}

    // Start is called before the first frame update
    void Start()
    {
        finalScoreTextboxes = GetComponentsInChildren<Text>();

        finalScore = finalScoreTextboxes[0];
        finalRating = finalScoreTextboxes[1];
        completedMeals = finalScoreTextboxes[2];

        GameManager.Instance.OnGameEnded.AddListener(SetValues);
    }

    private void SetValues() {
        finalScore.text = "Final Score: " + TentacularScore.Instance.Score;
        finalRating.text = "Final Rating: " + TentacularLikes.Instance.likes;
        completedMeals.text = "Completed Meals: " + TentacularScore.Instance.CompletedMeals;
    }
}
