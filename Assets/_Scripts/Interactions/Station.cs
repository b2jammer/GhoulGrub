using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Interactable))]
[RequireComponent(typeof(Inventory))]
[ExecuteInEditMode]
public class Station : MonoBehaviour
{
    #region Public Variables
    public StationInfo stationInfo;

    public Image iconRenderer;
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

    private void Update()
    {
        if (iconRenderer != null)
        {
            iconRenderer.sprite = stationInfo.icon;
        }
    }
    #endregion

    #region Script Specific Scripts
    public void OpenStationPanel()
    {
        StationPanel stationPanel = StationPanel.Instance;
        stationPanel.SetStation(this);
        InventoryInteractablesManager.Instance.SwitchPanel((int)InteractablePanels.Station);
    }

    public void CloseStationPanel()
    {
        StationPanel stationPanel = StationPanel.Instance;
        stationPanel.CloseStation(this);
    }
    #endregion
}
