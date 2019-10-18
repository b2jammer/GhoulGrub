using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderMaker : MonoBehaviour
{
    public MealItem[] availableMeals;

    private float totalTime;
    private float currentTime;
    private Dictionary<MealItem, int> orderItems;

    private int totalMealRank;
    private float increaseRankProbability;
    private int rankIncreaseAmount;
    private Dictionary<int, List<MealItem>> rankedFoodItems;

    private void Start() {
        SetRankedItems();
        increaseRankProbability = 0.0f;
        rankIncreaseAmount = 1;
    }

    private void Update() {
        UpdateIncreaseRankProbability();
    }

    private void MakeOrder() {

    }

    private void SetTime() {
        // TODO: Have the time take into account restaurant rating, number of meal items 
        // and the rank of meal items
        totalTime = Random.Range(30, 60);
        currentTime = totalTime;
    }

    private void SetOrderItems() {
        // TODO: Have the number of items in the order take into account restaurant rating
        // as well as meal rank
        int[] mealRanks = GetMealRanks();


    }

    private int[] GetMealRanks() {
        int numberOfMealItems = Random.Range(1, 5);

        int[] mealRanks = new int[numberOfMealItems];

        for (int i = 0; i < numberOfMealItems; i++) {
            int mealRank = Random.Range(1, 7);

            if (IncreaseRank()) {
                if (mealRank <= 6) {
                    mealRank++;
                }
            }

            mealRanks[i] = mealRank;
        }

        return mealRanks;
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

    private void AddMealItem(MealItem mealItem) {
        if (orderItems.ContainsKey(mealItem)) {
            orderItems[mealItem]++;
        }
        else {
           orderItems.Add(mealItem, 1);
        }
    }
}
