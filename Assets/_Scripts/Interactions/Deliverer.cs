using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Deliverer : MonoBehaviour
{
    public int timeBetweenDeliveries = 60;
    public int baseFoodQuantityOfEachType = 10;

    public FoodItem[] deliveredFoodTypes;
    public Text deliveryText;

    private Inventory fridgeInventory;

    private void Start() {
        deliveryText.color = Color.green;
        fridgeInventory = GetComponent<Inventory>();
        MakeDelivery();
    }

    private void MakeDelivery() {
        int currentFoodQuantityOfEachType = baseFoodQuantityOfEachType + (int)Mathf.Round(TentacularLikes.Instance.likes);
        string delivery = "";

        foreach (var food in deliveredFoodTypes) {
            delivery += string.Format("Received delivery of: {0} x {1} \n", food.itemName, currentFoodQuantityOfEachType);
            for (int i = 0; i < currentFoodQuantityOfEachType; i++) {
                fridgeInventory.AddInventoryItem(food);
            }
        }
        SetDeliveryText(delivery);

        Invoke("MakeDelivery", timeBetweenDeliveries);
    }

    private void SetDeliveryText(string delivery) {
        deliveryText.text = delivery;
    }


}
