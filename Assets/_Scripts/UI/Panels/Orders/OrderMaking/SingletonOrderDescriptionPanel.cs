using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SingletonOrderDescriptionPanel : MonoBehaviour, IPointerDownHandler {

    #region Public Variables
    public static SingletonOrderDescriptionPanel instance;

    [HideInInspector]
    // the related order
    public Order order;

    [SerializeField]
    // the UI text where the description is drawn
    private Text description;
    #endregion

    #region Private Variables
    //// the portion of the order that contains what the customer wants
    //private string orderItemDescription;
    private Closeable closeable;
    #endregion

    #region Monobehavior/Inherited Functions

    private void Awake() {
        if (instance != null) {
            Destroy(gameObject);
        }
        else {
            instance = this;
        }
        closeable = GetComponent<Closeable>();
        description.text = "No Order";
        closeable.ClosePanel();
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
            //else {
            //    this.gameObject.SetActive(true);
            //}
        }
    }
    #endregion

    #region Script Specific Functions
    public void CloseDescriptionPanel(Order order) {
        if (order == this.order) {
            closeable.ClosePanel();
        }
    }

    public void CloseDescriptionPanel() {
        closeable.ClosePanel();
    }

    public void OpenDescriptionPanel() {
        closeable.OpenPanel();
        //InventoryInteractablesManager.Instance.IsHidden = false;
    }

    public void SetDescriptionPanel(Order order) {
        //Debug.Log("Hello");
        if (order != this.order) {
            this.order = order;

            order.OnPrepItemAdded.AddListener(UpdateOrderDescription);
            order.OnPrepItemRemoved.AddListener(UpdateOrderDescription);

            UpdateOrderDescription();

            closeable.OpenPanel();
            //InventoryInteractablesManager.Instance.IsHidden = false;
        }
    }

    /// <summary>
    /// Sets and formats a string using the contents of the order's ordered items
    /// </summary>
    private string SetOrderItemDescription() {
        if (order != null) {
            string orderItemDescription = string.Format("Order Number {0} \n", order.orderNumber);

            foreach (var orderItem in order.orderFoodItems) {
                orderItemDescription += string.Format("{0} {1} \n", orderItem.Value, orderItem.Key.name);
            }

            orderItemDescription += "\n";
            return orderItemDescription;
        }
        else {
            return "No order selected \n";
        }
        
    }

    /// <summary>
    /// Creates and formats a string using the contents of the order's prepared items
    /// </summary>
    /// <returns>A formatted string of prepared items</returns>
    private string SetPreppedItemDescription() {
        if (order != null) {
            string preppedItemDescription = "Prepped Items \n";

            foreach (var item in order.preppedFoodItems.items) {
                preppedItemDescription += string.Format("{0} {1} \n", item.Value, item.Key.name);
            }

            return preppedItemDescription;
        }

        return "";

    }

    /// <summary>
    /// Updates the description and passes it to the UI text
    /// </summary>
    private void UpdateOrderDescription() {
        string orderItemDescription = SetOrderItemDescription();
        string preppedItemDescription = SetPreppedItemDescription();

        description.text = orderItemDescription + preppedItemDescription;
    }
    #endregion
}
