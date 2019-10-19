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
            OnOrderTimedOut.Invoke(orderNumber);
        }
    }
    #endregion
}
