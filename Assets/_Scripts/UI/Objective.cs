using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Objective : MonoBehaviour
{
    private Text objectiveText;

    private void Awake() {
        objectiveText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        SetText();
    }

    void SetText() {
        string objective = string.Format("Complete as many meals as possible before losing {0} meals \n", TentacularScore.Instance.maxLostMeals);
        objective += string.Format("Meals lost: {0}/{1}", TentacularScore.Instance.LostMeals, TentacularScore.Instance.maxLostMeals);

        objectiveText.text = objective;
    }
}
