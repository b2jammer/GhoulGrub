using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeItemPanel : MonoBehaviour
{
    #region Public Variables
    public RecipePanel owner;
    #endregion

    #region Private Variables
    [SerializeField]
    private Text label;
    [SerializeField]
    private Image icon;

    private FoodItem itemType;
    private int? count;
    private int? maxCount;
    #endregion

    #region MonoBehaviour Variables
    private void Awake()
    {
        count = null;
        if (itemType != null)
        {
            UpdateLabel();
        }
    }
    #endregion

    #region Script-Specific Methods
    public void SetFoodType(FoodItem newType)
    {
        itemType = newType;
        UpdateLabel();
    }

    public void SetCount(int? newCount)
    {
        count = newCount;
        UpdateLabel();
    }

    public void SetMaxCount(int? newMaxCount)
    {
        maxCount = newMaxCount;
        UpdateLabel();
    }

    public FoodItem GetFoodType()
    {
        return itemType;
    }

    public void UpdateLabel()
    {
        string countString = null;
        if (count != null)
        {
            countString = "\nx" + count;
            if (maxCount != null && maxCount > count)
            {
                countString += "-" + maxCount;
            }
        }

        if (itemType != null)
        {
            label.text = itemType.itemName + countString;
            icon.sprite = itemType.sprite;
        }
    }

    public void ShowRecipes()
    {
        owner.FindRecipes(itemType);
    }
    #endregion
}
