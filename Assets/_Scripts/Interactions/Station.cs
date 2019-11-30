﻿using System.Collections;
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
    public float maxTooltipTime = 2f;
    public Image iconRenderer;

    [HideInInspector]
    public InventoryDropTarget dropTarget;

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
    private GrubTooltipManager tooltipManager;
    private float currentTooltipTime;

    #endregion

    #region MonoBehaviour Methods
    private void Awake()
    {
        interactable = GetComponent<Interactable>();
        _stationInventory = GetComponent<Inventory>();
        currentTooltipTime = 0f;
    }

    private void Start() {
        tooltipManager = GrubTooltipManager.Instance;
    }

    private void Update()
    {
        if (iconRenderer != null)
        {
            iconRenderer.sprite = stationInfo.icon;
        }
    }

    private void OnMouseEnter() {
        tooltipManager.ShowTooltip(stationInfo.title);
    }

    private void OnMouseOver() {
        currentTooltipTime += Time.deltaTime;
        if (currentTooltipTime > maxTooltipTime) {
            tooltipManager.HideTooltip();
        }
    }

    private void OnMouseExit() {
        currentTooltipTime = 0f;
        tooltipManager.HideTooltip();
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
