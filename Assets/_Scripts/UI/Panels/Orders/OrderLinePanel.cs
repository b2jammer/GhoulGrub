using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OrderLinePanel : MonoBehaviour {

    public OrderMaker orderMaker;
    [HideInInspector]
    public Dictionary<OrderPanel, OrderDescriptionPanel> descriptionPanels;
    #region Private Variables

    [SerializeField]
    private RectTransform panelContainer;

    [SerializeField]
    private OrderPanel orderPanelPrefab;

    private Dictionary<Order, OrderPanel> orderPanels;


    #endregion

    #region MonoBehaviour Methods
    private void Awake() {
        orderPanels = new Dictionary<Order, OrderPanel>();
        descriptionPanels = new Dictionary<OrderPanel, OrderDescriptionPanel>();
    }

    private void Start() {
        if (orderMaker != null) {
            orderMaker.OnOrderMade.AddListener(AddOrder);
            orderMaker.OnOrderRemoved.AddListener(RemoveOrder);
        }
        
    }
    #endregion

    public void AddOrder(Order order) {

        OrderPanel newOrderPanel = GameObject.Instantiate(orderPanelPrefab, panelContainer);
        newOrderPanel.order = order;
        newOrderPanel.orderLinePanel = this;
        orderPanels.Add(order, newOrderPanel);

    }

    public void RemoveOrder(Order order) {
        if (orderPanels.ContainsKey(order)) {
            GameObject removedOrderObject = order.gameObject;

            OrderPanel relatedOrderPanel = orderPanels[order];
            GameObject relatedOrderPanelObject = relatedOrderPanel.gameObject;


            GameObject relatedDescriptionPanelObject = descriptionPanels[relatedOrderPanel].gameObject;

            orderPanels.Remove(order);
            descriptionPanels.Remove(relatedOrderPanel);

            Destroy(removedOrderObject);
            Destroy(relatedOrderPanelObject);
            Destroy(relatedDescriptionPanelObject);
        }
    }
}
