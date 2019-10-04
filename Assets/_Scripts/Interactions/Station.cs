using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
[RequireComponent(typeof(Inventory))]
public class Station : MonoBehaviour
{
    #region Public Variables
    public string stationTitle;
    public RecipeList recipeList;
    public Inventory StationInventory
    {
        get
        {
            return _stationInventory;
        }
    }
    #endregion

    #region Private Variables
    private Interactable interactable;
    private Inventory _stationInventory;
    
    #endregion

    #region MonoBehaviour Methods
    private void Awake()
    {
        interactable = GetComponent<Interactable>();
        _stationInventory = GetComponent<Inventory>();
    }
    #endregion

    #region Script Specific Scripts
    public void OpenStationPanel()
    {
        StationPanel stationPanel = StationPanel.Instance;
        stationPanel.OpenStation(this);
    }
    #endregion
}
