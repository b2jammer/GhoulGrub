using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FillOrderPanel : MonoBehaviour {
    #region Public Variables
    public static FillOrderPanel instance;

    public UnityEvent OnOrderSet;

    public bool HasOrder {
        get {
            return hasOrder;
        }
        set {
            hasOrder = value;
        }
    }
    #endregion

    #region Private Variables
    [SerializeField]
    private InventoryPanel orderInventory;
    [SerializeField]
    private Text label;

    private Order currentOrder;
    private Closeable closeable;
    private bool hasOrder;
    #endregion

    #region MonoBehaviour Methods
    private void Awake() {
        if (instance != null) {
            Destroy(gameObject);
        }
        else {
            instance = this;
        }
        closeable = GetComponent<Closeable>();
        closeable.ClosePanel();
    }
    #endregion

    #region Script-Specific Methods
    public void SetOrder(Order order) {
        if (currentOrder != order) {
            hasOrder = true;
            currentOrder = order;
            orderInventory.InventoryData = order.preppedFoodItems;
            label.text = order.name;
            closeable.OpenPanel();

            OnOrderSet.Invoke();
        }
        
    }

    public void OpenFillOrderPanel() {
        closeable.OpenPanel();
    }

    public void CloseFillOrderPanel(Order order) {
        if (order == currentOrder) {
            closeable.ClosePanel();
        }
    }

    public void CloseFillOrderPanel() {
        closeable.ClosePanel();
    }

    public bool IsOpen() {
        return closeable.IsOpened();
    }

    #endregion
}
