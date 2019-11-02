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
    #endregion

    #region Private Variables
    [SerializeField]
    // UI text that displays a timer for the order
    private Text timerText;

    //[SerializeField]
    //private OrderDescriptionPanel descriptionPanelPrefab;
    [SerializeField]
    private RectTransform orderPanelImageTransform;

    //private RectTransform canvas;
    private SingletonOrderDescriptionPanel descriptionPanel;
    private FillOrderPanel fillOrderPanel;
    //private OrderDescriptionPanel orderDescriptionPanel;
    #endregion

    #region Monobehavior functions
    private void Awake() {
        //canvas = GameObject.Find("Primary Canvas").GetComponent<RectTransform>();
    }

    //private IEnumerator Start() {
    //    if (order != null) {
    //        yield return WaitToCreateOrderDescription();
    //    }

    //    yield return null;
    //}

    private void Start() {
        descriptionPanel = SingletonOrderDescriptionPanel.instance;
        fillOrderPanel = FillOrderPanel.instance;
    }

    private void Update() {
        UpdateTimer();
        //CheckPositionChanged();
    }

    public void OnPointerDown(PointerEventData eventData) {
        if (eventData.button == PointerEventData.InputButton.Left) {
            Debug.Log("Order panel left clicked");
            UpdateOrderCompletionPanel();
            UpdateDescriptionPanel();
        }
    }
    #endregion

    #region Script specific functions
    ///// <summary>
    ///// Activates or deactivates the order description panel related to this order panel
    ///// </summary>
    //public void ToggleViewDescriptionPanel() {
    //    if (orderDescriptionPanel != null) {
    //        if (orderDescriptionPanel.gameObject.activeInHierarchy) {
    //            orderDescriptionPanel.gameObject.SetActive(false);
    //        }
    //        else {
    //            orderDescriptionPanel.gameObject.SetActive(true);
    //        }
    //    }
    //}

    ///// <summary>
    ///// Creates an order description panel at the end of the current frame
    ///// </summary>
    ///// <returns></returns>
    //IEnumerator WaitToCreateOrderDescription() {
    //    yield return new WaitForEndOfFrame();
    //    CreateOrderDescriptionPanel();
    //    yield return null;
    //}

    ///// <summary>
    ///// Creates an order description panel
    ///// </summary>
    //private void CreateOrderDescriptionPanel() {
    //    orderDescriptionPanel = GameObject.Instantiate(descriptionPanelPrefab, canvas);
    //    orderDescriptionPanel.order = order;
    //    orderDescriptionPanel.gameObject.SetActive(false);
    //    orderDescriptionPanel.transform.position = orderPanelImageTransform.position;

    //    orderLinePanel.descriptionPanels.Add(this, orderDescriptionPanel);
    //}

    /// <summary>
    /// Updates the description panels position at the end of the current frame
    /// </summary>
    /// <returns></returns>
    //IEnumerator WaitToUpdateDescriptionPanelPosition() {
    //    yield return new WaitForEndOfFrame();
    //    UpdateDescriptionPanelPosition();
    //    yield return null;
    //}

    ///// <summary>
    ///// Updates the description panels position
    ///// </summary>
    //private void UpdateDescriptionPanelPosition() {
    //    orderDescriptionPanel.transform.position = orderPanelImageTransform.position;
    //}

    /// <summary>
    /// Checks if this panels position has changed and if it has it also
    /// updates the related description panels position
    /// </summary>
    //private void CheckPositionChanged() {
    //    if (transform.hasChanged) {
    //        //StartCoroutine(WaitToUpdateDescriptionPanelPosition());
    //        transform.hasChanged = false;
    //    }
    //}

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
    }

    /// <summary>
    /// 
    /// </summary>
    public void UpdateDescriptionPanel() {
        SingletonOrderDescriptionPanel.instance.SetDescriptionPanel(order);
    }
    #endregion
}
