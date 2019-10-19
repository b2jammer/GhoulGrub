using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderLinePanel : MonoBehaviour {
    public OrderMaker orderMaker;

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
        newOrderPanel.Order = order;
        orderPanels.Add(order, newOrderPanel);

    }

    public void RemoveOrder(Order order) {
        if (orderPanels.ContainsKey(order)) {
            GameObject removed = order.gameObject;
            orderPanels.Remove(order);
            Destroy(removed);
        }
    }
}
