using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class TestCrate : MonoBehaviour {

    #region Public Variables
    public Inventory playerInventory;
    public FoodItem[] crateFood;
    #endregion

    #region Private Variables
    private Interactable interactable;
    //private Inventory stationInventory;

    #endregion

    #region MonoBehaviour Methods
    private void Awake() {
        interactable = GetComponent<Interactable>();
        //stationInventory = GetComponent<Inventory>();
    }

    #endregion

    #region Script Specific Scripts
    public void GivePlayerFood() {
        Debug.Log("Here is some food");

        foreach (var food in crateFood) {
            playerInventory.AddInventoryItem(food);
        }

    }

    public void RemoveFoodItemFromPlayer() {
        Debug.Log("You've lost some food");

        int randomFoodItemIndex = Random.Range(0, crateFood.Length);

        playerInventory.RemoveInventoryItem(crateFood[randomFoodItemIndex]);
    }

    #endregion
}


