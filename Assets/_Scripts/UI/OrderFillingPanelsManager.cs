using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderFillingPanelsManager : MonoBehaviour
{
    public FillOrderPanel fillPanel;
    public SingletonOrderDescriptionPanel descriptionPanel;

    [HideInInspector]
    public static OrderFillingPanelsManager Instance;

    private bool isHidden;

    private void Awake() {
        isHidden = true;
    }

    // Start is called before the first frame update
    void Start() {
        ClosePanels();
        Instance = this;
    }

    public void TogglePanels() {
        if (isHidden) {
            OpenPanels();
        }
        else {
            ClosePanels();
        }
    }

    public void ClosePanels() {
        fillPanel.CloseFillOrderPanel();
        descriptionPanel.CloseDescriptionPanel();
        isHidden = true;

    }

    public void OpenPanels() {
        fillPanel.OpenFillOrderPanel();
        descriptionPanel.OpenDescriptionPanel();
        isHidden = false;
    }
}
