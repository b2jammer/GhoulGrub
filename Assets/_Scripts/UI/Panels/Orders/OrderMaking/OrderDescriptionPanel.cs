using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OrderDescriptionPanel : MonoBehaviour, IPointerDownHandler
{
    #region Public Variables
    [HideInInspector]
    // the related order
    public Order order;

    [HideInInspector]
    // the UI text where the description is drawn
    public Text description;
    #endregion

    #region Private Variables
    // the portion of the order that contains what the customer wants
    private string orderItemDescription;
    #endregion

    #region Monobehavior/Inherited Functions
    private void Start() {
        if (order != null) {
            order.OnPrepItemAdded.AddListener(UpdateOrderDescription);
            order.OnPrepItemRemoved.AddListener(UpdateOrderDescription);

            SetOrderItemDescription();
            UpdateOrderDescription();
        }
    }

    /// <summary>
    /// Activates or deactivates the description panel if it is right clicked
    /// </summary>
    /// <param name="eventData">PointerEventData data</param>
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

    /// <summary>
    /// Removes the description panels listeners
    /// </summary>
    private void OnDestroy() {
        order.OnPrepItemAdded.RemoveListener(UpdateOrderDescription);
        order.OnPrepItemRemoved.RemoveListener(UpdateOrderDescription);
    }
    #endregion

    #region Script Specific Functions
    /// <summary>
    /// Sets and formats a string using the contents of the order's ordered items
    /// </summary>
    private void SetOrderItemDescription() {
        orderItemDescription = string.Format("Order Number {0} \n", order.orderNumber);

        foreach (var orderItem in order.orderFoodItems) {
            orderItemDescription += string.Format("{0} {1} \n", orderItem.Value, orderItem.Key.name);
        }

        orderItemDescription += "\n";
    }

    /// <summary>
    /// Creates and formats a string using the contents of the order's prepared items
    /// </summary>
    /// <returns>A formatted string of prepared items</returns>
    private string SetPreppedItemDescription() {
        string preppedItemDescription = "Prepped Items \n";

        foreach (var item in order.preppedFoodItems.items) {
            preppedItemDescription += string.Format("{0} {1} \n", item.Value, item.Key.name);
        }

        return preppedItemDescription;
    }

    /// <summary>
    /// Updates the description and passes it to the UI text
    /// </summary>
    private void UpdateOrderDescription() {
        string preppedItemDescription = SetPreppedItemDescription();

        description.text = orderItemDescription + preppedItemDescription;
    }
    #endregion
}
