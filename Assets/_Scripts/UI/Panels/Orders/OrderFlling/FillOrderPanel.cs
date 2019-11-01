using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FillOrderPanel : MonoBehaviour {
    #region Public Variables
    public static FillOrderPanel instance;

    public UnityEvent OnOrderSet;
    #endregion

    #region Private Variables
    [SerializeField]
    private InventoryPanel orderInventory;
    [SerializeField]
    private Text label;

    private Order currentOrder;
    #endregion

    #region MonoBehaviour Methods
    private void Awake() {
        if (instance != null) {
            Destroy(gameObject);
        }
        else {
            instance = this;
        }
    }
    #endregion

    #region Script-Specific Methods
    public void SetOrder(Order order) {
        currentOrder = order;
        orderInventory.InventoryData = order.preppedFoodItems;
        label.text = order.name;

        OnOrderSet.Invoke();
    }
    #endregion
}
