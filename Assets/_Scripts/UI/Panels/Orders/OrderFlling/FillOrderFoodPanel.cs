using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillOrderFoodPanel : MonoBehaviour {
    #region Public Variables
    [HideInInspector]
    public Order CurrentOrder {
        get {
            return currentOrder;
        }
        set {
            currentOrder = value;
            ResetOrder();
        }
    }

    [HideInInspector]
    public FillOrderDropTarget dropTarget;
    #endregion

    #region Private Variables

    [SerializeField]
    private RectTransform panelContainer;
    [SerializeField]
    private FillOrderFoodItemPanel foodPanelPrefab;
    private Dictionary<MealItem, FillOrderFoodItemPanel> foodPanels;

    private Order currentOrder;
    #endregion

    #region MonoBehaviour Methods
    private void Awake() {
        foodPanels = new Dictionary<MealItem, FillOrderFoodItemPanel>();
    }
    #endregion

    public void ResetOrder() {
        if (foodPanels != null && foodPanels.Count > 0) {
            foreach (MealItem type in foodPanels.Keys) {
                Destroy(foodPanels[type].gameObject);
            }
        }

        foodPanels = new Dictionary<MealItem, FillOrderFoodItemPanel>();
        foreach (MealItem foodType in currentOrder.preppedFoodItems.Keys) {
            for (int i = 0; i < currentOrder.preppedFoodItems[foodType]; i++) {
                AddItem(foodType);
            }
        }
    }

    public void AddItem(MealItem item) {
        if (foodPanels.ContainsKey(item)) {
            foodPanels[item].UpdateLabel();
        }
        else {
            FillOrderFoodItemPanel newPanel = GameObject.Instantiate(foodPanelPrefab, panelContainer);
            newPanel.foodPanelContainer = this;
            newPanel.SetMealType(item);
            foodPanels[item] = newPanel;
        }
    }

    public void RemoveItem(MealItem item) {
        if (foodPanels.ContainsKey(item)) {
            if (currentOrder.preppedFoodItems[item] <= 0) {
                GameObject removed = foodPanels[item].gameObject;
                foodPanels.Remove(item);
                Destroy(removed);
            }
            else {
                foodPanels[item].UpdateLabel();
            }
        }
    }
}
