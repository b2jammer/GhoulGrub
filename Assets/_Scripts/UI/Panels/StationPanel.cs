using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Closeable))]
public class StationPanel : MonoBehaviour
{
    #region Public Variables
    public static StationPanel Instance;
    #endregion

    #region Private Variables
    [SerializeField]
    private InventoryPanel itemPanel;

    [SerializeField]
    private Text label;

    private Station currentStation;
    private Closeable closeable;
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
    }
    #endregion

    #region Script-Specific Methods
    public void OpenStation(Station station)
    {
        currentStation = station;
        itemPanel.InventoryData = station.StationInventory;
        label.text = station.stationTitle;
        closeable.OpenPanel();
    }

    public void Closetation(Station station)
    {
        if (currentStation == station)
        {
            closeable.ClosePanel();
        }
    }
    #endregion
}
