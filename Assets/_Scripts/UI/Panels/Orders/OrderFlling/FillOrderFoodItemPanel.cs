using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class FillOrderFoodItemPanel : MonoBehaviour
{
    #region Public Variables
    [HideInInspector]
    public FillOrderFoodPanel foodPanelContainer;

    private MealItem itemType;
    #endregion

    #region Private Variables
    [SerializeField]
    private Text label;
    [SerializeField]
    private Image icon;
    #endregion

    #region MonoBehaviour Variables
    private void Awake() {
        if (itemType != null) {
            UpdateLabel();
        }
    }
    #endregion

    #region Script-Specific Methods
    public void SetMealType(MealItem newType) {
        itemType = newType;
        UpdateLabel();
    }

    public MealItem GetMealType() {
        return itemType;
    }

    public void UpdateLabel() {
        if (itemType != null) {
            if (foodPanelContainer != null) {
                foodPanelContainer.CurrentOrder.preppedFoodItems.TryGetValue(itemType, out int count);
                label.text = itemType.itemName + "\nx " + count;
            }
            else {
                label.text = itemType.itemName + "\nx ?";
            }
            icon.sprite = itemType.sprite;
        }
    }
    #endregion
}
