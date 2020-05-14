using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Interactable))]
[RequireComponent(typeof(Inventory))]
[ExecuteInEditMode]
public class Station : MonoBehaviour {
    #region Public Variables
    public StationInfo stationInfo;
    public float maxTooltipTime = 2f;
    public Image iconRenderer;
    public WorldText worldText;

    [HideInInspector]
    public InventoryDropTarget dropTarget;
    [HideInInspector]

    public Inventory StationInventory {
        get {
            return _stationInventory;
        }
    }

    public Collider StationCollider {
        get {
            return stationCollider;
        }
    }
    #endregion

    #region Private Variables
    private Interactable interactable;
    private Inventory _stationInventory;
    private GrubTooltipManager tooltipManager;
    private float currentTooltipTime;
    private Collider stationCollider;

    #endregion

    #region MonoBehaviour Methods
    private void Awake() {
        interactable = GetComponent<Interactable>();
        _stationInventory = GetComponent<Inventory>();
        stationCollider = GetComponent<Collider>();
        currentTooltipTime = 0f;
    }

    private void Start() {
        tooltipManager = GrubTooltipManager.Instance;

        worldText.Disappear();
    }

    private void Update() {
        if (iconRenderer != null) {
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
    public void OpenStationPanel() {
        StationPanel stationPanel = StationPanel.Instance;
        stationPanel.SetStation(this);
        stationPanel.OpenStation();

        worldText.SetPosition(transform.position);
        //InventoryInteractablesManager.Instance.SwitchPanel((int)InteractablePanels.Station);
    }

    public void CloseStationPanel() {
        StationPanel stationPanel = StationPanel.Instance;
        stationPanel.CloseStation(this);
    }

    public void ToggleInteractText() {
        StationPanel stationPanel = StationPanel.Instance;
        if (stationPanel.IsOpen()) {
            HideInteractText();
        }
        else {
            ShowInteractText();
        }
    }

    public void ShowInteractText() {
        worldText.SetPosition(transform.position);
        worldText.Appear();
    }

    public void HideInteractText() {
        worldText.Disappear();
    }

    public void ToggleStationPanel() {
        StationPanel stationPanel = StationPanel.Instance;

        if (stationPanel.IsOpen()) {
            CloseStationPanel();
        }
        else {
            OpenStationPanel();
        }
    }
    #endregion
}
