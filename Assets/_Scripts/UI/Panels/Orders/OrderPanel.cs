using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;

public class OrderPanel : MonoBehaviour
{
    private Order order;
    [SerializeField]
    private Text timerText;
    [SerializeField]
    private OrderDescriptionPanel descriptionPanelPrefab;
    [SerializeField]
    private RectTransform parent;

    public Order Order {
        get {
            return order;
        }
        set {
            order = value;
        }
    }

    private void Awake() {
        if (order != null) {
            CreateOrderDescriptionPanel();
        }
    }

    private void Update() {
        UpdateTimer();
    }

    private void UpdateTimer() {
        string time = order.currentTime.ToString("F0", CultureInfo.InvariantCulture);
        timerText.text = string.Format("Time: {0}", time);
    }

    private void CreateOrderDescriptionPanel() {
        OrderDescriptionPanel orderDescription = GameObject.Instantiate(descriptionPanelPrefab, parent);
        orderDescription.Order = order;
    }
}
