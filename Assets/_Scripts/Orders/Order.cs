﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Order : MonoBehaviour {

    public class OrderEvent : UnityEvent<int> { }
    //public class OrderMealEvent : UnityEvent<MealItem> { }
    
    #region Public Variables
    [HideInInspector]
    public OrderEvent OnOrderTimedOut;
    [HideInInspector]
    public OrderEvent OnOrderCompleted;
    [HideInInspector]
    public UnityEvent OnPrepItemAdded;
    [HideInInspector]
    public UnityEvent OnPrepItemRemoved;

    //[HideInInspector]
    //public OrderMealEvent OnPrepItemAdded;
    //[HideInInspector]
    //public OrderMealEvent OnPrepItemRemoved;

    [HideInInspector]
    // Timer starting time
    public float totalTime;
    [HideInInspector]
    // Current timer time
    public float currentTime;

    [HideInInspector]
    public int orderNumber;

    [HideInInspector]
    // items in the order
    public Dictionary<MealItem, int> orderFoodItems;

    [HideInInspector]
    // items that have been prepared for the order
    public Dictionary<MealItem, int> preppedFoodItems;
    #endregion

    #region Private variables
    private const float MAX_RATING = 5.0f;
    #endregion

    #region Monobehavior Methods
    private void Awake() {
        preppedFoodItems = new Dictionary<MealItem, int>();
        OnPrepItemAdded = new UnityEvent();
        OnPrepItemRemoved = new UnityEvent();
        OnOrderTimedOut = new OrderEvent();
        OnOrderCompleted = new OrderEvent();
    }

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
    /// <summary>
    /// Adds a meal item to the prepared items and invokes OnPrepItemAdded
    /// </summary>
    /// <param name="preppedItem">A meal item</param>
    public void AddPreppedItem(MealItem preppedItem) {
        if (preppedFoodItems.ContainsKey(preppedItem)) {
            preppedFoodItems[preppedItem]++;
        }
        else {
            preppedFoodItems.Add(preppedItem, 1);
        }
        OnPrepItemAdded.Invoke();
    }

    /// <summary>
    /// Removes a meal item from the prepared items and invokes OnPrepItemRemoved
    /// </summary>
    /// <param name="preppedItem"></param>
    public void RemovePreppedItem(MealItem preppedItem) {
        if (preppedFoodItems.ContainsKey(preppedItem) && preppedFoodItems[preppedItem] > 1) {
            preppedFoodItems[preppedItem]--;
        }
        else {
            preppedFoodItems.Remove(preppedItem);
        }
        OnPrepItemRemoved.Invoke();
    }

    /// <summary>
    /// Checks how much time is left on the timer and if it's less than
    /// zero it invokes OnOrderTimedOut and updates the restaurants rating accordingly
    /// </summary>
    private void CheckTime() {
        if (currentTime <= 0) {
            UpdateTentacularLikes(1.0f);
            OnOrderTimedOut.Invoke(orderNumber);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void ComparePreppedItemsToOrder() {
        //TODO: implement function to compare prepped items to order items
    }

    /// <summary>
    /// Updates the restaurant's total likes, total customers and updates the average 
    /// rating
    /// </summary>
    /// <param name="rating"></param>
    private void UpdateTentacularLikes(float rating) {
        TentacularLikes.totalLikes += rating;
        TentacularLikes.totalCustomers++;

        TentacularLikes.CalculateLikeValue();
    }

    /// <summary>
    /// Calculates a whole number rating based on remaining time
    /// </summary>
    /// <returns>A whole number rating</returns>
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

    /// <summary>
    /// Calculates a rating using a linear function based on remaining time
    /// </summary>
    /// <returns>A number rating</returns>
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

    /// <summary>
    /// Calculates the lowest time value that will result in a 5 tentacle rating
    /// </summary>
    /// <returns>A float time value</returns>
    private float MaxRatingTimeCutoff() {
        return totalTime * 0.8f;
    }
    #endregion
}
