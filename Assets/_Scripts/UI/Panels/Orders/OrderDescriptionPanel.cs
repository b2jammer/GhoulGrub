using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OrderDescriptionPanel : MonoBehaviour, IPointerDownHandler
{
    [HideInInspector]
    public Order order;

    [HideInInspector]
    public Text description;


    private string orderItemDescription;

    private void Start() {
        if (order != null) {
            order.OnPrepItemAdded.AddListener(UpdateOrderDescription);
            order.OnPrepItemRemoved.AddListener(UpdateOrderDescription);

            SetOrderItemDescription();
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

    public void OnPointerDown(PointerEventData eventData) {
        if (eventData.button == PointerEventData.InputButton.Right) {
            if (this.gameObject.activeInHierarchy) {
                this.gameObject.SetActive(false);
            }
            else {
                this.gameObject.SetActive(true);
            }
        }
    }

    private void OnDestroy() {
        order.OnPrepItemAdded.RemoveListener(UpdateOrderDescription);
        order.OnPrepItemRemoved.RemoveListener(UpdateOrderDescription);
    }
}
