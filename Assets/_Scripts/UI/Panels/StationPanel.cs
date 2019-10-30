using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Closeable))]
public class StationPanel : MonoBehaviour
{
    //int is # of batches, NOT total items produced
    [System.Serializable]
    public class CombineEvent : UnityEvent<RecipeList.Recipe, int> { }

    #region Public Variables
    public FoodItem mistakeItem;

    public static StationPanel Instance;
    public UnityEvent OnCombineAttempt;
    public CombineEvent OnCombineSucceed;
    public UnityEvent OnCombineFail;
    #endregion

    #region Private Variables
    [SerializeField]
    private InventoryPanel itemPanel;

    [SerializeField]
    private Text label;
    [SerializeField]
    private Image icon;
    [SerializeField]
    private Text actionLabel;

    private Station currentStation;
    private Closeable closeable;

    private Inventory playerInventory;
    #endregion

    #region MonoBehaviour Methods
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        closeable = GetComponent<Closeable>();
        closeable.ClosePanel();
        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }
    #endregion

    #region Script-Specific Methods
    public void OpenStation(Station station)
    {
        currentStation = station;
        itemPanel.InventoryData = station.StationInventory;
        label.text = station.stationInfo.title;
        icon.sprite = station.stationInfo.icon;
        actionLabel.text = station.stationInfo.action;
        closeable.OpenPanel();
    }

    public void CloseStation(Station station)
    {
        if (currentStation == station)
        {
            closeable.ClosePanel();
        }
    }

    public void Combine()
    {
        RecipeList recipes = currentStation.stationInfo.recipeList;
        RecipeList.Recipe foundRecipe = RecipeList.FindMatchingRecipe(itemPanel.InventoryData.items, recipes, out int batches);
        OnCombineAttempt.Invoke();
        if (foundRecipe != null)
        {
            for (int i=0; i<foundRecipe.targetQuantity*batches; i++)
                playerInventory.AddInventoryItem(foundRecipe.target);
            OnCombineSucceed.Invoke(foundRecipe, batches);
        }
        else
        {
            playerInventory.AddInventoryItem(mistakeItem);
            OnCombineFail.Invoke();
        }
        itemPanel.InventoryData.ClearInventory();
    }
    #endregion
}
