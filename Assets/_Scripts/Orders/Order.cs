using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order : MonoBehaviour {
    #region Public Variables
    public float totalTime;
    public float currentTime;

    public Dictionary<FoodItem, int> orderFoodItems;
    public Dictionary<FoodItem, int> preppedFoodItems;
    #endregion

    #region Default Methods
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
    #endregion

    #region Script Specific Methods
    public void AddPreppedItem(FoodItem preppedItem) {
        if (preppedFoodItems.ContainsKey(preppedItem)) {
            preppedFoodItems[preppedItem]++;
        }
        else {
            preppedFoodItems.Add(preppedItem, 1);
        }
    }

    public void RemovePreppedItem(FoodItem preppedItem) {
        if (preppedFoodItems.ContainsKey(preppedItem) && preppedFoodItems[preppedItem] > 1) {
            preppedFoodItems[preppedItem]--;
        }
        else {
            preppedFoodItems.Remove(preppedItem);
        }
    }
    #endregion
}
