using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class OrderMaker : MonoBehaviour {
    [System.Serializable]
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
    [Range(1, 10)]
    public int maxMealRank = 10;

    [HideInInspector]
    public OrderMakerEvent OnOrderMade;
    [HideInInspector]
    public OrderMakerEvent OnOrderRemoved;

    public OrderMakerEvent OnOrderOut;
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
        
        int[] mealData = SetOrderItems(orderItems);
        SetTime(mealData, out float totalTime, out float currentTime);

        var order = Instantiate(orderPrefab);
        order.transform.position = transform.position;

        var orderComponent = order.GetComponent<Order>();
        orderComponent.totalTime = totalTime;
        orderComponent.currentTime = currentTime;
        orderComponent.orderFoodItems = orderItems;
        orderComponent.orderNumber = orderNumber;

        orderComponent.OnOrderTimedOut.AddListener(OrderTimedOut);
        orderComponent.OnOrderCompleted.AddListener(OrderOut);
        orderComponent.OnOrderTimedOut.AddListener(UpdateInteractablePanels);
        orderComponent.OnOrderCompleted.AddListener(UpdateInteractablePanels);

        order.name = "Order " + orderNumber;

        orders.Add(orderNumber++, orderComponent);

        OnOrderMade.Invoke(orderComponent);

        DetermineTimeUntilNextOrder(totalTime);
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

    private void OrderOut(int orderNumber) {
        TentacularScore.Instance.UpdateText(orders[orderNumber].currentTime);
        TentacularScore.Instance.UpdateCompletedMeals();
        OnOrderOut.Invoke(orders[orderNumber]);
        StartCoroutine(RemoveAfterSeconds(0.5f, orderNumber));
    }

    private void OrderTimedOut(int orderNumber) {
        TentacularScore.Instance.UpdateLostMeals();
        StartCoroutine(RemoveAfterSeconds(0.5f, orderNumber));
    }

    private IEnumerator RemoveAfterSeconds(float seconds, int orderNumber) {
        yield return new WaitForSeconds(seconds);
        RemoveOrder(orderNumber);
        yield return null;
    }

    private void UpdateInteractablePanels(int orderNumber) {
        Order order = orders[orderNumber];

        SingletonOrderDescriptionPanel.instance.CloseDescriptionPanel(order);
        FillOrderPanel.instance.HasOrder = false;
        FillOrderPanel.instance.CloseFillOrderPanel(order);
    }

    /// <summary>
    /// Determines how much time there will be to complete this order
    /// </summary>
    /// <param name="totalTime"></param>
    /// <param name="currentTime"></param>
    private void SetTime(int[] mealData, out float totalTime, out float currentTime) {
        float defaultTime = 75f;
        float rankBasedTime = 0f;
        float mealSizeBasedTime = 0f;
        float ratingModifier = 0f;

        foreach (var data in mealData) {
            switch (data) {
                case 1:
                    rankBasedTime += 5;
                    break;
                case 2:
                    rankBasedTime += 6;
                    break;
                case 3:
                    rankBasedTime += 7;
                    break;
                case 4:
                    rankBasedTime += 9;
                    break;
                case 5:
                    rankBasedTime += 11;
                    break;
                case 6:
                    rankBasedTime += 13;
                    break;
                case 7:
                    rankBasedTime += 15;
                    break;
                case 8:
                    rankBasedTime += 18;
                    break;
                case 9:
                    rankBasedTime += 21;
                    break;
                case 10:
                    rankBasedTime += 24;
                    break;
                default:
                    rankBasedTime += 25;
                    break;
            }
        }

        mealSizeBasedTime += (mealData.Length * 10f);
        ratingModifier -= (Mathf.Round(TentacularLikes.Instance.likes) - 1) * 4f;

        totalTime = defaultTime + rankBasedTime + mealSizeBasedTime + ratingModifier;
        currentTime = totalTime;
    }

    /// <summary>
    /// Determines what meal items an order will contain
    /// </summary>
    /// <param name="orderMealItems"></param>
    /// <param name="totalOrderRank"></param>
    private int[] SetOrderItems(Dictionary<MealItem, int> orderMealItems) {

        int[] mealRanks = GetMealRanks();

        foreach (var mealRank in mealRanks) {
            Debug.Log(mealRank);
            int numberOfMealItemsWithMealRank = rankedFoodItems[mealRank].Count;
            int randomMealWithMealRank = Random.Range(0, numberOfMealItemsWithMealRank);

            var mealItem = rankedFoodItems[mealRank][randomMealWithMealRank];

            AddMealItemToOrder(orderMealItems, mealItem);
        }

        return mealRanks;
    }

    /// <summary>
    /// Randomly picks the ranks for the order
    /// </summary>
    /// <param name="totalOrderRank"></param>
    /// <returns>An array containing the ranks of the meals that will be in the order</returns>
    private int[] GetMealRanks() {

        int numberOfMealItems = DetermineNumberOfMealItems();

        int[] mealRanks = new int[numberOfMealItems];

        for (int i = 0; i < numberOfMealItems; i++) {
            int mealRank = GetRandomMealRank();

            if (IncreaseRank()) {
                if (mealRank <= maxMealRank - 1) {
                    mealRank += rankIncreaseAmount;
                }
            }
            mealRanks[i] = mealRank;
        }
        return mealRanks;
    }

    private int GetRandomMealRank() {
        float chance = Random.value;

        if (chance >= .85f + (.1f * ((Mathf.Round(TentacularLikes.Instance.likes)-1f)/4f)) ) {
            return 1;
        }
        else if (chance >= .7f + (.2f * ((Mathf.Round(TentacularLikes.Instance.likes) - 1f) / 4f))) {
            return 2;
        }
        else if (chance >= .55f + (.25f * ((Mathf.Round(TentacularLikes.Instance.likes) - 1f) / 4f))) {
            return 3;
        }
        else if (chance >= .45f + (.2f * ((Mathf.Round(TentacularLikes.Instance.likes) - 1f) / 4f))) {
            return 4;
        }
        else if (chance >= .35f + (.15f * ((Mathf.Round(TentacularLikes.Instance.likes) - 1f) / 4f))) {
            return 5;
        }
        else if (chance >= .20f + (.2f * ((Mathf.Round(TentacularLikes.Instance.likes) - 1f) / 4f))) {
            return 6;
        }
        else if (chance >= .15f + (.15f * ((Mathf.Round(TentacularLikes.Instance.likes) - 1f) / 4f))) {
            return 7;
        }
        else if (chance >= .1f + (.1f * ((Mathf.Round(TentacularLikes.Instance.likes) - 1f) / 4f))) {
            return 8;
        }
        else if (chance >= .05 + (.05f * ((Mathf.Round(TentacularLikes.Instance.likes) - 1f) / 4f))) {
            return 9;
        }
        else {
            return 10;
        }
    }

    /// <summary>
    /// Determines the number of meal items in an order
    /// </summary>
    /// <returns></returns>
    private int DetermineNumberOfMealItems() {
        // TODO: Have the number of items in the order take into account restaurant rating
        int ratingModifier = 0;

        switch (Mathf.Round(TentacularLikes.Instance.likes)) {
            case 0:
                ratingModifier = 1;
                break;
            case 1:
                ratingModifier = Random.Range(0, 2);
                break;
            case 2:
                ratingModifier = Random.Range(0, 3);
                break;
            case 3:
                ratingModifier = Random.Range(0, 4);
                break;
            case 4:
                ratingModifier = Random.Range(1, 5);
                break;
            case 5:
                ratingModifier = Random.Range(2, 5);
                break;
            default:
                break;
        }
        int baseNumberOfMeals = 1;

        int numberOfMealItems = baseNumberOfMeals + ratingModifier;
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
        if (TentacularLikes.Instance.likes >= 4f) {
            increaseRankProbability = 0.8f;
        }
        else if (TentacularLikes.Instance.likes >= 3f) {
            increaseRankProbability = 0.4f;
        }
        else if (TentacularLikes.Instance.likes >= 2f) {
            increaseRankProbability = 0.1f;
        }
        else {
            increaseRankProbability = 0.0f;
        }
    }

    /// <summary>
    /// Determines how much time there is until the next order spawns
    /// </summary>
    private void DetermineTimeUntilNextOrder(float totalTime) {
        timeTilNextOrder = totalTime * (.75f - (TentacularLikes.Instance.likes * 3f)/100f);
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
