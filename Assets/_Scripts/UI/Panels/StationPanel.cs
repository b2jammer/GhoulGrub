using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Closeable))]
public class StationPanel : MonoBehaviour
{
    #region Public Variables
    public static StationPanel Instance;
    public UnityEvent OnCombineAttempt;
    public RecipeList.RecipeEvent OnCombineSucceed;
    public UnityEvent OnCombineFail;
    #endregion

    #region Private Variables
    [SerializeField]
    private InventoryPanel itemPanel;

    [SerializeField]
    private Text label;
    [SerializeField]
    private Image icon;

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
        label.text = station.stationTitle;
        icon.sprite = station.recipeList.listIcon;
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
        RecipeList recipes = currentStation.recipeList;
        RecipeList.Recipe foundRecipe = RecipeList.FindMatchingRecipe(itemPanel.InventoryData.items, recipes);
        OnCombineAttempt.Invoke();
        if (foundRecipe != null)
        {
            for (int i=0; i<foundRecipe.targetQuantity; i++)
                playerInventory.AddInventoryItem(foundRecipe.target);
            OnCombineSucceed.Invoke(foundRecipe);
        }
        else
        {
            OnCombineFail.Invoke();
        }
        itemPanel.InventoryData.ClearInventory();
    }

    public bool IsOpen() {
        return closeable.IsOpened();
    }
    #endregion
}
