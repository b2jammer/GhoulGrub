﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class OrderMaker : MonoBehaviour {
    public class OrderMakerEvent : UnityEvent<Order> { }

    #region Public variables
    [HideInInspector]
    // dictionary that maps order numbers to their respective orders
    public Dictionary<int, Order> orders;

    [Tooltip("Array of all meal items that can be cooked")]
    public MealItem[] availableMeals;
    [Tooltip("Prefab for an order")]
    public GameObject orderPrefab;
    [Tooltip("Time until the first order spawns")]
    public float timeTilFirstOrder = 10f;

    [Tooltip("The current maximum rank a meal can have")]
    [Range(1, 7)]
    public int maxMealRank = 7;

    [HideInInspector]
    public OrderMakerEvent OnOrderMade;
    [HideInInspector]
    public OrderMakerEvent OnOrderRemoved;
    #endregion

    #region Private variables
    // probability that the rank of a meal will be randomly increased
    private float increaseRankProbability;

    // number of ranks that are added when a meal rank is increased
    private int rankIncreaseAmount;

    // dictionary mapping ranks to meal items
    private Dictionary<int, List<MealItem>> rankedFoodItems;

    // the order number
    private int orderNumber;

    // the amount of time until the next order spawns
    private float timeTilNextOrder;

    // The current minimum rank a meal can have
    private const int minMealRank = 1;
    #endregion

    #region Monobehavior methods
    private void Awake() {
        increaseRankProbability = 0.0f;
        rankIncreaseAmount = 1;
        orderNumber = 0;
        orders = new Dictionary<int, Order>();
        rankedFoodItems = new Dictionary<int, List<MealItem>>();
        OnOrderMade = new OrderMakerEvent();
        OnOrderRemoved = new OrderMakerEvent();
    }

    private void Start() {
        SetRankedItems();
        Invoke("MakeOrder", timeTilFirstOrder);
    }

    private void Update() {
        UpdateIncreaseRankProbability();
    }
    #endregion

    #region Script specific methods
    /// <summary>
    /// Spawns a new order
    /// </summary>
    private void MakeOrder() {
        Dictionary<MealItem, int> orderItems = new Dictionary<MealItem, int>();
        int totalOrderRank = 0;

        SetTime(out float totalTime, out float currentTime);
        SetOrderItems(orderItems, ref totalOrderRank);

        var order = Instantiate(orderPrefab);
        order.transform.position = transform.position;

        var orderComponent = order.GetComponent<Order>();
        orderComponent.totalTime = totalTime;
        orderComponent.currentTime = currentTime;
        orderComponent.orderFoodItems = orderItems;
        orderComponent.orderNumber = orderNumber;
        orderComponent.OnOrderTimedOut.AddListener(RemoveOrder);

        orders.Add(orderNumber++, orderComponent);

        OnOrderMade.Invoke(orderComponent);

        DetermineTimeUntilNextOrder();
        Invoke("MakeOrder", timeTilNextOrder);
    }

    /// <summary>
    /// Removes an order and invokes OnOrderRemoved
    /// </summary>
    /// <param name="orderNumber">the order number of the order</param>
    private void RemoveOrder(int orderNumber) {
        if (orders.ContainsKey(orderNumber)) {
            OnOrderRemoved.Invoke(orders[orderNumber]);
            orders.Remove(orderNumber);
        }
    }

    /// <summary>
    /// Determines how much time there will be to complete this order
    /// </summary>
    /// <param name="totalTime"></param>
    /// <param name="currentTime"></param>
    private void SetTime(out float totalTime, out float currentTime) {
        // TODO: Have the time take into account restaurant rating, number of meal items 
        // and the rank of meal items
        totalTime = Random.Range(30, 60);
        currentTime = totalTime;
    }

    /// <summary>
    /// Determines what meal items an order will contain
    /// </summary>
    /// <param name="orderMealItems"></param>
    /// <param name="totalOrderRank"></param>
    private void SetOrderItems(Dictionary<MealItem, int> orderMealItems, ref int totalOrderRank) {

        int[] mealRanks = GetMealRanks(ref totalOrderRank);

        foreach (var mealRank in mealRanks) {
            int numberOfMealItemsWithMealRank = rankedFoodItems[mealRank].Count;
            int randomMealWithMealRank = Random.Range(0, numberOfMealItemsWithMealRank);

            var mealItem = rankedFoodItems[mealRank][randomMealWithMealRank];

            AddMealItemToOrder(orderMealItems, mealItem);
        }
    }

    /// <summary>
    /// Randomly picks the ranks for the order
    /// </summary>
    /// <param name="totalOrderRank"></param>
    /// <returns>An array containing the ranks of the meals that will be in the order</returns>
    private int[] GetMealRanks(ref int totalOrderRank) {

        int numberOfMealItems = DetermineNumberOfMealItems();

        int[] mealRanks = new int[numberOfMealItems];

        for (int i = 0; i < numberOfMealItems; i++) {
            int mealRank = Random.Range(minMealRank, maxMealRank + 1);

            if (IncreaseRank()) {
                if (mealRank <= maxMealRank - 1) {
                    mealRank += rankIncreaseAmount;
                }
            }
            totalOrderRank += mealRank;
            mealRanks[i] = mealRank;
        }
        return mealRanks;
    }

    /// <summary>
    /// Determines the number of meal items in an order
    /// </summary>
    /// <returns></returns>
    private int DetermineNumberOfMealItems() {
        // TODO: Have the number of items in the order take into account restaurant rating
        int numberOfMealItems = Random.Range(1, 6);
        return numberOfMealItems;
    }

    /// <summary>
    /// Increases the rank of a meal item based on the increase rank probability
    /// </summary>
    /// <returns></returns>
    private bool IncreaseRank() {
        float chance = Random.value;

        return chance < increaseRankProbability;
    }

    /// <summary>
    /// Updates the increase rank probability based on the restaurants rating; The 
    /// lower the rating, the lower the increase rank probability
    /// </summary>
    private void UpdateIncreaseRankProbability() {
        if (TentacularLikes.likes >= 4f) {
            increaseRankProbability = 0.8f;
        }
        else if (TentacularLikes.likes >= 3f) {
            increaseRankProbability = 0.4f;
        }
        else if (TentacularLikes.likes >= 2f) {
            increaseRankProbability = 0.1f;
        }
        else {
            increaseRankProbability = 0.0f;
        }
    }

    /// <summary>
    /// Determines how much time there is until the next order spawns
    /// </summary>
    private void DetermineTimeUntilNextOrder() {
        // TODO: Have this take into account the tentacular likes and 
        // total meals already out
        timeTilNextOrder = Random.Range(15, 25);
    }

    /// <summary>
    /// Initializes the rankedFoodItems dictionary
    /// </summary>
    private void SetRankedItems() {
        foreach (var foodItem in availableMeals) {
            if (rankedFoodItems.ContainsKey(foodItem.rank)) {
                rankedFoodItems[foodItem.rank].Add(foodItem);
            }
            else {
                rankedFoodItems.Add(foodItem.rank, new List<MealItem>() { foodItem });
            }
        }
    }

    /// <summary>
    /// Adds a meal item to the order
    /// </summary>
    /// <param name="orderItems"></param>
    /// <param name="mealItem"></param>
    private void AddMealItemToOrder(Dictionary<MealItem, int> orderItems, MealItem mealItem) {
        if (orderItems.ContainsKey(mealItem)) {
            orderItems[mealItem]++;
        }
        else {
            orderItems.Add(mealItem, 1);
        }
    }
    #endregion
}