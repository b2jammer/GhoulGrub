using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Closeable))]
public class StationPanel : MonoBehaviour {
    //int is # of batches, NOT total items produced
    [System.Serializable]
    public class CombineEvent : UnityEvent<RecipeList.Recipe, int> { }

    #region Public Variables
    public FoodItem mistakeItem;
    public Sprite defaultStationSprite;

    public static StationPanel Instance;
    public UnityEvent OnCombineAttempt;
    public CombineEvent OnCombineSucceed;
    public UnityEvent OnCombineFail;
    #endregion

    #region Private Variables
    [SerializeField]
    private InventoryPanel itemPanel;

    [SerializeField]
    private Text label;
    [SerializeField]
    private Image icon;
    [SerializeField]
    private Text actionLabel;

    private Station currentStation;
    private Closeable closeable;

    private Inventory _nullInventory;
    private Inventory playerInventory;
    private RectTransform panelRect;
    #endregion

    #region MonoBehaviour Methods
    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
        }
        else {
            Instance = this;
        }
        _nullInventory = GetComponent<Inventory>();
        closeable = GetComponent<Closeable>();
        panelRect = GetComponent<RectTransform>();

        closeable.ClosePanel();
        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }
    #endregion

    #region Script Specific Methods
    public void SetStation(Station station) {
        currentStation = station;
        itemPanel.InventoryData = currentStation.StationInventory;
        label.text = currentStation.stationInfo.title;
        icon.sprite = currentStation.stationInfo.icon;
        actionLabel.text = currentStation.stationInfo.action;
    }

    public void OpenStation() {
        //InventoryInteractablesManager.Instance.IsHidden = false;
        UpdatePanelPosition();
        closeable.OpenPanel();
        itemPanel.ResetInventory();
    }

    public void CloseStation(Station station) {
        if (currentStation == station) {
            CloseStation();
            //InventoryInteractablesManager.Instance.TogglePanels();
        }
    }

    public void CloseStation() {
        closeable.ClosePanel();
    }

    public void Combine() {
        if (currentStation != null) {
            if (currentStation.stationInfo.title != "Refrigerator") {
                RecipeList recipes = currentStation.stationInfo.recipeList;
                RecipeList.Recipe foundRecipe = RecipeList.FindMatchingRecipe(itemPanel.InventoryData.items, recipes, out int batches);
                OnCombineAttempt.Invoke();
                if (foundRecipe != null) {
                    for (int i = 0; i < foundRecipe.targetQuantity * batches; i++) {
                        playerInventory.AddInventoryItem(foundRecipe.target);
                    }
                    OnCombineSucceed.Invoke(foundRecipe, batches);
                }
                else {
                    if (itemPanel.InventoryData.items.Count > 0) {
                        playerInventory.AddInventoryItem(mistakeItem);
                        OnCombineFail.Invoke();
                    }
                }
                itemPanel.InventoryData.ClearInventory();  
            }
            else {
                foreach (var item in itemPanel.InventoryData.items) {
                    for (int i = 0; i < item.Value; i++) {
                        playerInventory.AddInventoryItem(item.Key);
                    }
                }
                itemPanel.InventoryData.ClearInventory();
            }
        }
    }

    public bool IsOpen() {
        return closeable.IsOpened();
    }

    public void UpdatePanelPosition() {
        float screenHeight = Screen.height;
        float screenWidth = Screen.width;

        float resolutionHeight = Screen.currentResolution.height;
        float resolutionWidth = Screen.currentResolution.width;

        //Debug.Log(resolutionWidth + " " + resolutionHeight);

        if (currentStation != null) {
            float heightModifier = screenHeight/resolutionHeight;
            float widthModifier = screenWidth/resolutionWidth;

            Collider stationCollider = currentStation.StationCollider;
            Vector3 max = stationCollider.bounds.max;
            Vector3 min = stationCollider.bounds.min;

            Vector3 viewportMax = Camera.main.WorldToViewportPoint(max);
            Vector3 viewportMin = Camera.main.WorldToViewportPoint(min);

            //Debug.Log(viewportMax + " " + viewportMin);

            Vector2 updatedPosition = new Vector2((viewportMax.x * resolutionWidth + panelRect.rect.width/8 * widthModifier), (viewportMin.y * resolutionHeight - panelRect.rect.height/2.5f * heightModifier));
            //Debug.Log(updatedPosition);
            panelRect.anchoredPosition = updatedPosition;

        }
        else {
            Vector2 updatedPosition = new Vector2(.5f * resolutionWidth, .5f * resolutionHeight);
            panelRect.anchoredPosition = updatedPosition;
        }
    }
    #endregion
}
