using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScorePanel : MonoBehaviour
{
    public Text scoreText;

    public void SetScoreText() {
        scoreText.text = GameManager.Instance.GetScoreText();
    }
}
