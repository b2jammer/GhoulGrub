using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderDescriptionPanel : MonoBehaviour
{
    [SerializeField]
    private Text description;
    [SerializeField]
    private Closeable closeable;
    private Order order;
    private string orderItemDescription;

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
            order.OnPrepItemAdded.AddListener(UpdateOrderDescription);
            order.OnPrepItemRemoved.AddListener(UpdateOrderDescription);

            SetOrderItemDescription();
        }
    }

    private void Start() {
        if (order != null) {
            UpdateOrderDescription();
        }
    }

    private void SetOrderItemDescription() {
        orderItemDescription = string.Format("Order Number {0} \n", order.orderNumber);

        foreach (var orderItem in order.orderFoodItems) {
            orderItemDescription += string.Format("{0} {1} \n", orderItem.Value, orderItem.Key.name);
        }

        orderItemDescription += "\n";
    }

    private string SetPreppedItemDescription() {
        string preppedItemDescription = "Prepped Items \n";

        foreach (var item in order.preppedFoodItems) {
            preppedItemDescription += string.Format("{0} {1} \n", item.Value, item.Key.name);
        }

        return preppedItemDescription;
    }

    private void UpdateOrderDescription() {
        string preppedItemDescription = SetPreppedItemDescription();

        description.text = orderItemDescription + preppedItemDescription;
    }

    private void OnMouseDown() {
        closeable.ClosePanel();
    }

}
