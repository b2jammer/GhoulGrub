using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class OrderPanel : MonoBehaviour, IPointerDownHandler {

    #region Public Variables
    [HideInInspector]
    // parent order line panel
    public OrderLinePanel orderLinePanel;

    [HideInInspector]
    // the related order
    public Order order;

    [HideInInspector]
    public InventoryDropTarget dropTarget;
    #endregion

    #region Private Variables
    [SerializeField]
    // UI text that displays a timer for the order
    private Text timerText;

    [SerializeField]
    private Text titleText;

    [SerializeField]
    private RectTransform orderPanelImageTransform;

    [SerializeField]
    private Image background;

    private Color originalBackgroundColor;
    private Color selectedColor;
    private SingletonOrderDescriptionPanel descriptionPanel;
    private FillOrderPanel fillOrderPanel;
    #endregion

    #region Monobehavior functions

    private void Start() {
        descriptionPanel = SingletonOrderDescriptionPanel.instance;
        fillOrderPanel = FillOrderPanel.instance;
        titleText.text = "Look at order " + order.orderNumber;
        originalBackgroundColor = background.color;
        selectedColor = new Color(255f / 255f, 200f / 255f, 0f / 255f, 200f / 255f);
    }

    private void Update() {
        UpdateTimer();
    }

    public void OnPointerDown(PointerEventData eventData) {
        if (eventData.button == PointerEventData.InputButton.Left) {
            //Debug.Log("Order panel left clicked");
            PanelPressed();
        }
    }
    #endregion

    #region Script specific functions

    public void PanelPressed() {
        UpdateOrderCompletionPanel();
        UpdateDescriptionPanel();
        ChangeBackgroundColor();
        orderLinePanel.UpdateSelectedOrderPanel(this);
        OpenPanels();
    }

    public void ChangeBackgroundColor() {
        background.color = selectedColor;
    }

    public void ResetBackgroundColor() {
        background.color = originalBackgroundColor;
    }

    /// <summary>
    /// Updates the panels timer
    /// </summary>
    private void UpdateTimer() {
        string time = order.currentTime.ToString("F0", CultureInfo.InvariantCulture);
        timerText.text = string.Format("Time: {0}", time);
    }

    /// <summary>
    /// 
    /// </summary>
    public void UpdateOrderCompletionPanel() {
        FillOrderPanel.instance.SetOrder(order);
        //InventoryInteractablesManager.Instance.SwitchPanel((int)InteractablePanels.Fill);
    }

    /// <summary>
    /// 
    /// </summary>
    public void UpdateDescriptionPanel() {
        SingletonOrderDescriptionPanel.instance.SetDescriptionPanel(order);
    }

    public void OpenPanels() {
        OrderFillingPanelsManager.Instance.OpenPanels();
    }
    #endregion
}
