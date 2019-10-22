using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Order : MonoBehaviour {

    public class OrderEvent : UnityEvent<int> { }

    [HideInInspector]
    public OrderEvent OnOrderTimedOut;
    [HideInInspector]
    public OrderEvent OnOrderCompleted;
    [HideInInspector]
    public UnityEvent OnPrepItemAdded;
    [HideInInspector]
    public UnityEvent OnPrepItemRemoved;

    #region Public Variables
    [HideInInspector]
    public float totalTime;
    [HideInInspector]
    public float currentTime;
    [HideInInspector]
    public int orderNumber;

    [HideInInspector]
    public Dictionary<MealItem, int> orderFoodItems;
    [HideInInspector]
    public Dictionary<MealItem, int> preppedFoodItems;
    #endregion

    private const float MAX_RATING = 5.0f;

    private void Awake() {
        preppedFoodItems = new Dictionary<MealItem, int>();
        OnPrepItemAdded = new UnityEvent();
        OnPrepItemRemoved = new UnityEvent();
        OnOrderTimedOut = new OrderEvent();
        OnOrderCompleted = new OrderEvent();
    }

    #region Default Methods
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        currentTime -= Time.deltaTime;

        CheckTime();
    }
    #endregion

    #region Script Specific Methods
    public void AddPreppedItem(MealItem preppedItem) {
        if (preppedFoodItems.ContainsKey(preppedItem)) {
            preppedFoodItems[preppedItem]++;
        }
        else {
            preppedFoodItems.Add(preppedItem, 1);
        }
        OnPrepItemAdded.Invoke();
    }

    public void RemovePreppedItem(MealItem preppedItem) {
        if (preppedFoodItems.ContainsKey(preppedItem) && preppedFoodItems[preppedItem] > 1) {
            preppedFoodItems[preppedItem]--;
        }
        else {
            preppedFoodItems.Remove(preppedItem);
        }
        OnPrepItemRemoved.Invoke();
    }

    private void CheckTime() {
        if (currentTime <= 0) {
            UpdateTentacularLikes(1.0f);
            OnOrderTimedOut.Invoke(orderNumber);
        }
    }

    private void ComparePreppedItemsToOrder() {

    }

    private void UpdateTentacularLikes(float rating) {
        TentacularLikes.totalLikes += rating;
        TentacularLikes.totalCustomers++;

        TentacularLikes.CalculateLikeValue();
    }

    private float GetPiecewiseCustomerRating() {
        if (currentTime > totalTime * 0.8f) {
            return MAX_RATING;
        }
        else if (currentTime > totalTime * 0.6f) {
            return 4.0f;
        }
        else if (currentTime > totalTime * 0.4f) {
            return 3.0f;
        }
        else if (currentTime > totalTime * 0.2f) {
            return 2.0f;
        }
        else {
            return 1.0f;
        }
    }

    private float GetLinearCustomerRating() {
        float maxStarTimeCutoff = MaxRatingTimeCutoff();
        if (currentTime > maxStarTimeCutoff) {
            return MAX_RATING;
        }
        else {
            var rating = MAX_RATING * (currentTime / maxStarTimeCutoff);
            return (rating > 1.0f) ? rating : 1.0f;
        }
    }

    private float MaxRatingTimeCutoff() {
        return totalTime * 0.8f;
    }
    #endregion
}
