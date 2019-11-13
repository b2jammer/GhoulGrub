using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [System.Serializable]
    public class ItemEvent : UnityEvent<FoodItem> { }

    #region Public Variables
    public UnityEvent OnClear;
    public ItemEvent OnAddItem;
    public ItemEvent OnRemoveItem;

    public Dictionary<FoodItem, int> items;
    #endregion

    #region MonoBehaviour Methods
    private void Awake()
    {
        items = new Dictionary<FoodItem, int>();
    }
    #endregion

    #region Script-Specific Methods
    public int Count(FoodItem item)
    {
        if (items.ContainsKey(item))
        {
            return items[item];
        }
        else //Does not have this item, thus 0 copies.
        {
            return 0;
        }
    }

    public int TotalItems()
    {
        int sum = 0;
        foreach (FoodItem foodType in items.Keys)
        {
            sum += Count(foodType);
        }
        return sum;
    }

    public void ClearInventory() {
        items = new Dictionary<FoodItem, int>();
        OnClear.Invoke();
    }

    /// <summary>
    /// Attempts to remove one copy of the specified item.
    /// </summary>
    /// <param name="item">The item to remove from this inventory.</param>
    /// <returns>True if the item was successfully removed, false otherwise.</returns>
    public bool RemoveInventoryItem(FoodItem item) {
        if (items.ContainsKey(item))
        {
            items[item] -= 1;
            if (items[item] <= 0)
            {
                items.Remove(item);
            }
            OnRemoveItem.Invoke(item);
            return true;
        }
        else
        {
            return false;
        }
    }

    public IEnumerable GetFoodTypes()
    {
        return items.Keys;
    }

    public void AddInventoryItem(FoodItem item) {
        if (items.ContainsKey(item))
        {
            items[item] += 1;
        }
        else
        {
            items.Add(item, 1);
        }
        OnAddItem.Invoke(item);
    }
    #endregion
}
