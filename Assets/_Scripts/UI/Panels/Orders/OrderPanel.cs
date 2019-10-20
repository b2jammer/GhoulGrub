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

    private void Start() {
        if (order != null) {
            CreateOrderDescriptionPanel();
        }
    }

    private void Update() {
        UpdateTimer();
    }

    private void UpdateTimer() {
        string time = order.currentTime.ToString("F0", CultureInfo.InvariantCulture);
        timerText.text = string.Format("Time: {0}", time);
    }

    private void CreateOrderDescriptionPanel() {
        orderDescriptionPanel = GameObject.Instantiate(descriptionPanelPrefab, canvas);
        orderDescriptionPanel.order = order;
        orderDescriptionPanel.gameObject.SetActive(false);
        orderDescriptionPanel.transform.position = orderPanelImageTransform.transform.position;
        //orderDescriptionPanel.transform.position = new Vector3(
        //    this.transform.position.x,
        //    this.transform.position.y - 20f,
        //    this.transform.position.z
        //    );
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
