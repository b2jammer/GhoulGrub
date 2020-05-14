using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Order : MonoBehaviour {

    public class OrderEvent : UnityEvent<int> { }
    
    #region Public Variables
    [HideInInspector]
    public OrderEvent OnOrderTimedOut;
    [HideInInspector]
    public OrderEvent OnOrderCompleted;
    [HideInInspector]
    public UnityEvent OnPrepItemAdded;
    [HideInInspector]
    public UnityEvent OnPrepItemRemoved;

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

    // items that have been prepared for the order
    public Inventory preppedFoodItems;
    #endregion

    #region Private variables
    private const float MAX_RATING = 5.0f;
    private bool isCompleted;
    private bool isTimedOut;
    #endregion

    #region Monobehavior Methods
    private void Awake() {
        OnPrepItemAdded = new UnityEvent();
        OnPrepItemRemoved = new UnityEvent();
        OnOrderTimedOut = new OrderEvent();
        OnOrderCompleted = new OrderEvent();
        isCompleted = false;
        isTimedOut = false;
    }

    // Start is called before the first frame update
    void Start() {
        OnPrepItemAdded.AddListener(ComparePreppedItemsToOrder);
        preppedFoodItems.OnAddItem.AddListener(AddPreppedItem);
        preppedFoodItems.OnRemoveItem.AddListener(RemovePreppedItem);
    }

    // Update is called once per frame
    void Update() {
        if (!isCompleted) {
            currentTime -= Time.deltaTime;
            CheckTime();
        }
        
    }

    private void OnDestroy() {
        OnPrepItemAdded.RemoveAllListeners();
        OnOrderTimedOut.RemoveAllListeners();
        OnPrepItemRemoved.RemoveAllListeners();
        OnOrderCompleted.RemoveAllListeners();
    }
    #endregion

    #region Script Specific Methods
    /// <summary>
    /// Adds a meal item to the prepared items and invokes OnPrepItemAdded
    /// </summary>
    /// <param name="preppedItem">A meal item</param>
    public void AddPreppedItem(FoodItem preppedItem) {
        OnPrepItemAdded.Invoke();
    }

    /// <summary>
    /// Removes a meal item from the prepared items and invokes OnPrepItemRemoved
    /// </summary>
    /// <param name="preppedItem"></param>
    public void RemovePreppedItem(FoodItem preppedItem) {
        OnPrepItemRemoved.Invoke();
    }

    /// <summary>
    /// Checks how much time is left on the timer and if it's less than
    /// zero it invokes OnOrderTimedOut and updates the restaurants rating accordingly
    /// </summary>
    private void CheckTime() {
        if (!isTimedOut) {
            if (currentTime <= 0) {
                isTimedOut = true;
                UpdateRating();
                OnOrderTimedOut.Invoke(orderNumber);
            }
        }
        
    }

    /// <summary>
    /// 
    /// </summary>
    private void ComparePreppedItemsToOrder() {
        if (PreppedItemsEqualOrderItems()) {
            isCompleted = true;
            UpdateRating();
            OnOrderCompleted.Invoke(orderNumber);
        }
    }

    private bool PreppedItemsEqualOrderItems() {
        if (orderFoodItems.Count != preppedFoodItems.items.Count) {
            return false;
        }

        HashSet<MealItem> preppedKeys = new HashSet<MealItem>();
        foreach (var food in preppedFoodItems.GetFoodTypes()) {
            MealItem mealItem = food as MealItem;

            if (mealItem == null) {
                return false;
            }

            preppedKeys.Add(mealItem);
        }

        foreach (MealItem meal in preppedKeys) {
            if (orderFoodItems.TryGetValue(meal, out int mealCount)) {
                if (mealCount != preppedFoodItems.Count(meal)) {
                    return false;
                }
            }
            else {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// 
    /// </summary>
    private void UpdateRating() {
        UpdateTentacularLikes(GetLinearCustomerRating());
    }

    /// <summary>
    /// Updates the restaurant's total likes, total customers and updates the average 
    /// rating
    /// </summary>
    /// <param name="rating"></param>
    private void UpdateTentacularLikes(float rating) {
        TentacularLikes.Instance.totalLikes += rating;
        TentacularLikes.Instance.totalCustomers++;

        TentacularLikes.Instance.CalculateLikeValue();
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
