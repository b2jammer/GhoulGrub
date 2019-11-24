using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryInteractablesManager : MonoBehaviour {

    public FillOrderPanel fillPanel;
    public SingletonOrderDescriptionPanel descriptionPanel;
    public StationPanel stationPanel;

    [HideInInspector]
    public static InventoryInteractablesManager Instance;

    private InteractablePanels lastPanel;
    private bool isHidden;

    public bool IsHidden {
        set {
            isHidden = value;
        }
    }

    private void Awake() {
        lastPanel = InteractablePanels.Fill;
        isHidden = true;
    }

    // Start is called before the first frame update
    void Start() {
        ClosePanels();
        Instance = this;
    }

    public void SwitchPanel(int panel) {
        ClosePanels();

        switch ((InteractablePanels)panel) {
            case InteractablePanels.Fill:
                lastPanel = InteractablePanels.Fill;
                fillPanel.OpenFillOrderPanel();
                descriptionPanel.OpenDescriptionPanel();
                break;
            case InteractablePanels.Station:
                lastPanel = InteractablePanels.Station;
                stationPanel.OpenStation();
                descriptionPanel.OpenDescriptionPanel();
                break;
            default:
                break;
        }
    }

    public void TogglePanels() {
        if (isHidden) {
            OpenPanels();
        }
        else {
            ClosePanels();
            isHidden = true;
        }
    }

    private void ClosePanels() {
        fillPanel.CloseFillOrderPanel();
        descriptionPanel.CloseDescriptionPanel();
        stationPanel.CloseStation();
    }

    private void OpenPanels() {
        SwitchPanel((int)lastPanel);
    }

}

public enum InteractablePanels {
    Fill,
    Station
}
