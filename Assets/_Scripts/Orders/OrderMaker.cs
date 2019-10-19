using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderMaker : MonoBehaviour {
    public MealItem[] availableMeals;
    public GameObject orderPrefab;

    private int totalMealRank;
    private float increaseRankProbability;
    private int rankIncreaseAmount;
    private Dictionary<int, List<MealItem>> rankedFoodItems;

    private void Awake() {
        increaseRankProbability = 0.0f;
        rankIncreaseAmount = 1;
        rankedFoodItems = new Dictionary<int, List<MealItem>>();
    }

    private void Start() {
        SetRankedItems();

    }

    private void Update() {
        UpdateIncreaseRankProbability();
    }

    private void MakeOrder() {
        Dictionary<MealItem, int> orderItems = new Dictionary<MealItem, int>();

        SetTime(out float totalTime, out float currentTime);
        SetOrderItems(orderItems);

        var order = Instantiate(orderPrefab);
        order.transform.position = transform.position;

        var orderComponent = order.GetComponent<Order>();
        orderComponent.totalTime = totalTime;
        orderComponent.currentTime = currentTime;
        orderComponent.orderFoodItems = orderItems;
    }

    private void SetTime(out float totalTime, out float currentTime) {
        // TODO: Have the time take into account restaurant rating, number of meal items 
        // and the rank of meal items
        totalTime = Random.Range(30, 60);
        currentTime = totalTime;
    }

    private void SetOrderItems(Dictionary<MealItem, int> orderMealItems) {

        int[] mealRanks = GetMealRanks();

        foreach (var mealRank in mealRanks) {
            int numberOfMealItemsWithMealRank = rankedFoodItems[mealRank].Count;
            int randomMealWithMealRank = Random.Range(0, numberOfMealItemsWithMealRank - 1);

            var mealItem = rankedFoodItems[mealRank][randomMealWithMealRank];

            AddMealItemToOrder(orderMealItems, mealItem);
        }
    }

    private int[] GetMealRanks() {

        int numberOfMealItems = DetermineNumberOfMealItems();

        int[] mealRanks = new int[numberOfMealItems];

        for (int i = 0; i < numberOfMealItems; i++) {
            int mealRank = Random.Range(1, 7);

            if (IncreaseRank()) {
                if (mealRank <= 6) {
                    mealRank += rankIncreaseAmount;
                }
            }

            mealRanks[i] = mealRank;
        }

        return mealRanks;
    }

    private int DetermineNumberOfMealItems() {
        // TODO: Have the number of items in the order take into account restaurant rating
        // as well as meal rank
        int numberOfMealItems = Random.Range(1, 5);
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
