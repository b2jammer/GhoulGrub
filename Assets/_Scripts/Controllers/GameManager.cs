using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public UnityEvent OnGameEnded;

    public bool GameEnded {
        get {
            return gameEnded;
        }
    }

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

    private bool gameEnded;
    private static GameManager instance;

    private List<GameObject> objectsToDisableOnGameEnd;
    private List<GameObject> objectsToEnableOnGameEnd;

    private float[] scores;
    private float[] ratings;
    private int[] meals;

    private void Awake() {
        OnGameEnded = new UnityEvent();
        gameEnded = false;

        scores = new float[5];
        ratings = new float[5];
        meals = new int[5];

        GetPlayerPrefs();
    }

    private void Start() {
        objectsToDisableOnGameEnd = GameObject.FindGameObjectsWithTag("DisableOnEnd").ToList();
        objectsToEnableOnGameEnd = GameObject.FindGameObjectsWithTag("EnableOnEnd").ToList();

        foreach (var objectToDisable in objectsToDisableOnGameEnd) {
            if (objectToDisable.name != "MenuButton") {
                objectToDisable.SetActive(false); 
            }
        }

        foreach (var objectToEnable in objectsToEnableOnGameEnd) {
            objectToEnable.SetActive(false);
        }
    }

    public void EndGame() {

        if (!gameEnded) {
            gameEnded = true;
            OnGameEnded.Invoke();

            Time.timeScale = 0f;

            foreach (var objectToDisable in objectsToDisableOnGameEnd) {
                objectToDisable.SetActive(false);
            }

            foreach (var objectToEnable in objectsToEnableOnGameEnd) {
                objectToEnable.SetActive(true);
            }

            UpdateHighScores();
        }
    }

    private void GetPlayerPrefs() {

        for (int i = 0; i < 5; i++) {
            GetPref(i);
        }
    }

    private void GetPref(int i) {

        string baseScoreString = "HighScore" + (i + 1);
        string baseRatingString = "Rating" + (i + 1);
        string baseMealString = "Meals" + (i + 1);

        // High Scores
        if (PlayerPrefs.HasKey(baseScoreString)) {
            scores[i] = PlayerPrefs.GetFloat(baseScoreString);
        }

        PlayerPrefs.SetFloat(baseScoreString, scores[i]);

        // Ratings
        if (PlayerPrefs.HasKey(baseRatingString)) {
            ratings[i] = PlayerPrefs.GetFloat(baseRatingString);
        }

        PlayerPrefs.SetFloat(baseRatingString, ratings[i]);

        // Completed Meals
        if (PlayerPrefs.HasKey(baseMealString)) {
            meals[i] = PlayerPrefs.GetInt(baseMealString);
        }

        PlayerPrefs.SetInt(baseMealString, meals[i]);
    }

    private void UpdateHighScores() {
        List<int> minScoreIndices = scores.Select((value, index) => new {value, index}).Where(target => target.value == scores.Min()).Select(target => target.index).ToList();
        int minScoreIndex = minScoreIndices.First();

        if (TentacularScore.Instance.Score > scores[minScoreIndex]) {
            scores[minScoreIndex] = Mathf.Round(TentacularScore.Instance.Score * TentacularLikes.Instance.likes);
            ratings[minScoreIndex] = Mathf.Round(TentacularLikes.Instance.likes * 100) / 100;
            meals[minScoreIndex] = TentacularScore.Instance.CompletedMeals;

            UpdatePref(minScoreIndex);
        }
    }

    //private void UpdatePrefs() {
    //    for (int i = 0; i < 5; i++) {
    //        UpdatePref(i);
    //    }

    //    PlayerPrefs.Save();
    //}

    private void UpdatePref(int i) {
        string baseScoreString = "HighScore" + (i + 1);
        string baseRatingString = "Rating" + (i + 1);
        string baseMealString = "Meals" + (i + 1);

        PlayerPrefs.SetFloat(baseScoreString, scores[i]);
        PlayerPrefs.SetFloat(baseRatingString, ratings[i]);
        PlayerPrefs.SetInt(baseMealString, meals[i]);

        PlayerPrefs.Save();
    }

    public string GetScoreText() {
        GetPlayerPrefs();

        string scoreString = "\n";

        var orderedScores = scores.OrderByDescending(value => value).ToList();

        for (int i = 0; i < orderedScores.Count; i++) {
            scoreString += string.Format("{0}. {1} \n\n", i + 1, orderedScores[i]);
        }

        scoreString += "\n";

        return scoreString;
    }


}
