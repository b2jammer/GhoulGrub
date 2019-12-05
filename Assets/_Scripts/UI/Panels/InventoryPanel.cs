using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanel : MonoBehaviour
{
    #region Public Variables
    [HideInInspector]
    public Inventory InventoryData
    {
        get
        {
            return _inventory;
        }
        set
        {
            if (_inventory != null)
            {
                _inventory.OnAddItem.RemoveListener(AddItem);
                _inventory.OnRemoveItem.RemoveListener(RemoveItem);
                _inventory.OnClear.RemoveListener(ResetInventory);
            }

            if (value != null) {
                _inventory = value;
                _inventory.OnAddItem.AddListener(AddItem);
                _inventory.OnRemoveItem.AddListener(RemoveItem);
                _inventory.OnClear.AddListener(ResetInventory);
                ResetInventory(); 
            }
        }
    }

    [HideInInspector]
    public InventoryDropTarget dropTarget;
    #endregion

    #region Private Variables
    [SerializeField]
    private bool usePlayerInventory;

    [SerializeField]
    private RectTransform panelContainer;
    [SerializeField]
    private FoodItemPanel foodPanelPrefab;

    private Dictionary<FoodItem,FoodItemPanel> foodPanels;

    private Inventory _inventory;
    #endregion

    #region MonoBehaviour Methods
    private void Awake()
    {
        foodPanels = new Dictionary<FoodItem, FoodItemPanel>();
    }

    private void Start()
    {
        if (usePlayerInventory)
        {
            InventoryData = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        }
    }
    #endregion

    public void ResetInventory()
    {
        //Clear old children
        if (foodPanels != null && foodPanels.Count > 0)
        {
            foreach (FoodItem type in foodPanels.Keys)
            {
                Destroy(foodPanels[type].gameObject);
            }
        }
        foodPanels = new Dictionary<FoodItem, FoodItemPanel>();
        //Add UI panels based on inventory data.
        foreach (FoodItem foodType in _inventory.GetFoodTypes()) {
            for (int i = 0; i < _inventory.Count(foodType); i++) {
                AddItem(foodType);
            }
        }
    }

    public void AddItem(FoodItem item)
    {
        if (foodPanels.ContainsKey(item))
        {
            foodPanels[item].UpdateLabel();
        }
        else
        {
            FoodItemPanel newPanel = GameObject.Instantiate(foodPanelPrefab, panelContainer);
            newPanel.inventoryPanel = this;
            newPanel.SetFoodType(item);
            foodPanels[item] = newPanel;
        }
    }

    public void RemoveItem(FoodItem item)
    {
        if (foodPanels.ContainsKey(item))
        {
            if (_inventory.Count(item) <= 0)
            {
                GameObject removed = foodPanels[item].gameObject;
                foodPanels.Remove(item);
                Destroy(removed);
            }
            else
            {
                foodPanels[item].UpdateLabel();
            }
        }
    }
}
