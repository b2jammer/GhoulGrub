using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodItemPanel : MonoBehaviour
{
    #region Public Variables
    [HideInInspector]
    public InventoryPanel inventoryPanel;

    private FoodItem itemType;
    #endregion

    #region Private Variables
    [SerializeField]
    private Text label;
    [SerializeField]
    private Image icon;
    [SerializeField]
    private DisplayTooltip tooltip;
    #endregion

    #region MonoBehaviour Variables
    private void Awake()
    {
        if (tooltip == null) {
            tooltip = GetComponent<DisplayTooltip>();
        }

        if (itemType != null)
        {
            UpdateLabel();
            SetDisplayedTooltip();

        }


        
    }
    #endregion

    #region Script-Specific Methods
    public void SetFoodType(FoodItem newType)
    {
        itemType = newType;
        UpdateLabel();
        SetDisplayedTooltip();
    }

    public FoodItem GetFoodType()
    {
        return itemType;
    }

    public void SetDisplayedTooltip() {

        if (itemType != null) {
            //Debug.Log(tooltip);
            tooltip.tooltip = itemType.itemName;
        }
    }

    public void UpdateLabel()
    {
        if (itemType != null)
        {
            if (inventoryPanel != null)
            {
                label.text = itemType.itemName + "\nx " + inventoryPanel.InventoryData.Count(itemType);
            }
            else
            {
                label.text = itemType.itemName + "\nx ?";
            }
            icon.sprite = itemType.sprite;
        }
    }
    #endregion
}
