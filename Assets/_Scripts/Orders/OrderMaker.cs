using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class OrderMaker : MonoBehaviour {
    public class OrderMakerEvent : UnityEvent<Order> { }

    [HideInInspector]
    public Dictionary<int, Order> orders;
    public MealItem[] availableMeals;
    public GameObject orderPrefab;
    public float timeTilFirstOrder = 10f;
    [Range(1, 7)]
    public int maxMealRank = 7;

    [HideInInspector]
    public OrderMakerEvent OnOrderMade;
    [HideInInspector]
    public OrderMakerEvent OnOrderRemoved;

    private int totalMealRank;
    private float increaseRankProbability;
    private int rankIncreaseAmount;
    private Dictionary<int, List<MealItem>> rankedFoodItems;
    private int orderNumber;
    private float timeTilNextOrder;
    private int minMealRank = 1;


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

    private void RemoveOrder(int orderNumber) {
        if (orders.ContainsKey(orderNumber)) {
            OnOrderRemoved.Invoke(orders[orderNumber]);
            orders.Remove(orderNumber);
        }
    }

    private void SetTime(out float totalTime, out float currentTime) {
        // TODO: Have the time take into account restaurant rating, number of meal items 
        // and the rank of meal items
        totalTime = Random.Range(30, 60);
        currentTime = totalTime;
    }

    private void SetOrderItems(Dictionary<MealItem, int> orderMealItems, ref int totalOrderRank) {

        int[] mealRanks = GetMealRanks(ref totalOrderRank);

        foreach (var mealRank in mealRanks) {
            int numberOfMealItemsWithMealRank = rankedFoodItems[mealRank].Count;
            int randomMealWithMealRank = Random.Range(0, numberOfMealItemsWithMealRank);

            var mealItem = rankedFoodItems[mealRank][randomMealWithMealRank];

            AddMealItemToOrder(orderMealItems, mealItem);
        }
    }

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

    private int DetermineNumberOfMealItems() {
        // TODO: Have the number of items in the order take into account restaurant rating
        int numberOfMealItems = Random.Range(1, 6);
        return numberOfMealItems;
    }

    private bool IncreaseRank() {
        float chance = Random.value;

        return chance < increaseRankProbability;
    }

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

    private void DetermineTimeUntilNextOrder() {
        // TODO: Have this take into account the tentacular likes and 
        // total meals already out
        timeTilNextOrder = Random.Range(15, 25);
    }

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

    private void AddMealItemToOrder(Dictionary<MealItem, int> orderItems, MealItem mealItem) {
        if (orderItems.ContainsKey(mealItem)) {
            orderItems[mealItem]++;
        }
        else {
            orderItems.Add(mealItem, 1);
        }
    }
}
