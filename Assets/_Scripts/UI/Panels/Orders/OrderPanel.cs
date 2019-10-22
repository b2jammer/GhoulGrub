using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;
using UnityEngine.Events;

public class OrderPanel : MonoBehaviour {
    [HideInInspector]
    public UnityEvent OnDescriptionButtonPressed;
    [HideInInspector]
    public OrderLinePanel orderLinePanel;
    [HideInInspector]
    public Order order;


    [SerializeField]
    private Text timerText;
    [SerializeField]
    private OrderDescriptionPanel descriptionPanelPrefab;
    [SerializeField]
    private RectTransform orderPanelImageTransform;

    private RectTransform canvas;
    private OrderDescriptionPanel orderDescriptionPanel;


    private void Awake() {
        OnDescriptionButtonPressed = new UnityEvent();
        canvas = GameObject.Find("Primary Canvas").GetComponent<RectTransform>();
    }

    private IEnumerator Start() {


        if (order != null) {
            yield return WaitToCreateOrderDescription();
        }

        yield return null;
    }

    private void Update() {
        UpdateTimer();
        CheckPositionChanged();
    }

    private void UpdateTimer() {
        string time = order.currentTime.ToString("F0", CultureInfo.InvariantCulture);
        timerText.text = string.Format("Time: {0}", time);
    }

    private void CreateOrderDescriptionPanel() {
        orderDescriptionPanel = GameObject.Instantiate(descriptionPanelPrefab, canvas);
        orderDescriptionPanel.order = order;
        orderDescriptionPanel.gameObject.SetActive(false);
        orderDescriptionPanel.transform.position = orderPanelImageTransform.position;

        orderLinePanel.descriptionPanels.Add(this, orderDescriptionPanel);
    }

    private void UpdateDescriptionPanelPosition() {
        orderDescriptionPanel.transform.position = orderPanelImageTransform.position;
    }

    IEnumerator WaitToCreateOrderDescription() {
        yield return new WaitForEndOfFrame();

        CreateOrderDescriptionPanel();

        yield return null;
    }

    IEnumerator WaitToUpdateDescriptionPanelPosition() {
        yield return new WaitForEndOfFrame();
        UpdateDescriptionPanelPosition();
        yield return null;
    }

    private void CheckPositionChanged() {
        if (transform.hasChanged) {
            StartCoroutine(WaitToUpdateDescriptionPanelPosition());
            transform.hasChanged = false;
        }
    }

    public void ToggleViewDescriptionPanel() {
        if (orderDescriptionPanel != null) {
            if (orderDescriptionPanel.gameObject.activeInHierarchy) {
                orderDescriptionPanel.gameObject.SetActive(false);
            }
            else {
                orderDescriptionPanel.gameObject.SetActive(true);
            }
        }
    }


}
