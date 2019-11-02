using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderCompletionAndStationPanelManager : MonoBehaviour
{
    private StationPanel stationPanel;
    private FillOrderPanel fillOrderPanel;

    private void Start() {
        stationPanel = StationPanel.Instance;
        fillOrderPanel = FillOrderPanel.instance;
    }

    private void Update() {
        if (stationPanel.IsOpen() && fillOrderPanel.IsOpen()) {
            fillOrderPanel.CloseFillOrderPanel();
        }
        else if (!stationPanel.IsOpen() && !fillOrderPanel.IsOpen() && fillOrderPanel.HasOrder) {
            fillOrderPanel.OpenFillOrderPanel();
        }
        //else if (!fillOrderPanel.HasOrder) {
        //    fillOrderPanel.CloseFillOrderPanel();
        //}
    }
}
