using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OrderLinePanel : MonoBehaviour {

    #region Public variables
    [Tooltip("The scene's order maker")]
    public OrderMaker orderMaker;
    #endregion
    
    #region Private Variables
    [SerializeField]
    // the UI element that contains the order panels
    private RectTransform panelContainer;

    [SerializeField]
    private OrderPanel orderPanelPrefab;

    private Dictionary<Order, OrderPanel> orderPanels;
    #endregion

    #region MonoBehaviour Methods
    private void Awake() {
        orderPanels = new Dictionary<Order, OrderPanel>();
    }

    private void Start() {
        if (orderMaker != null) {
            orderMaker.OnOrderMade.AddListener(AddOrder);
            orderMaker.OnOrderRemoved.AddListener(RemoveOrder);
        }
    }
    #endregion

    #region Script specific functions
    /// <summary>
    /// Creates an order panel based on the order that's passed in
    /// </summary>
    /// <param name="order">The order being added</param>
    public void AddOrder(Order order) {

        OrderPanel newOrderPanel = GameObject.Instantiate(orderPanelPrefab, panelContainer);
        newOrderPanel.order = order;
        newOrderPanel.orderLinePanel = this;
        orderPanels.Add(order, newOrderPanel);

    }

    /// <summary>
    /// Removes the order, order panel and description panel related to the order
    /// that's passed in
    /// </summary>
    /// <param name="order">The order being removed</param>
    public void RemoveOrder(Order order) {
        if (orderPanels.ContainsKey(order)) {
            GameObject removedOrderObject = order.gameObject;

            OrderPanel relatedOrderPanel = orderPanels[order];
            GameObject relatedOrderPanelObject = relatedOrderPanel.gameObject;

            orderPanels.Remove(order);

            Destroy(removedOrderObject);
            Destroy(relatedOrderPanelObject);
        }
    }
    #endregion
}
