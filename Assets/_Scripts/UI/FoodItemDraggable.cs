using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class FoodItemDraggable : MonoBehaviour, IDragHandler, IEndDragHandler
{
    #region Public Variables
    [HideInInspector]
    public Inventory originalOwner;
    [HideInInspector]
    public FoodItem type;
    [HideInInspector]
    public Vector3 mouseOffset;
    #endregion

    #region Private Variables
    private bool dragging;

    [SerializeField]
    private Image icon;
    
    InventoryDropTarget currentDropTarget;
    private RectTransform rt;
    #endregion

    #region MonoBehaviour Methods
    private void Awake()
    {
        rt = GetComponent<RectTransform>();
        dragging = true;
    }
    #endregion

    #region Interface Methods
    public void OnDrag(PointerEventData eventData)
    {
        if (dragging)
        {
            rt.position = (Vector3)eventData.position - mouseOffset;
        }
    }
    #endregion

    #region Script-Specific Methods
    public void UpdateLabel()
    {
        icon.sprite = type.sprite;
    }

    public void ChangeTarget(InventoryDropTarget newTarget)
    {
        //Leave current target
        LeaveTarget(currentDropTarget);
        //Enter new target
        currentDropTarget = newTarget;
        if (currentDropTarget != null)
        {
            
        }
    }

    public void LeaveTarget(InventoryDropTarget tryTarget)
    {
        if (tryTarget == currentDropTarget)
        {
            if (currentDropTarget != null)
            {
                
            }
            currentDropTarget = null;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //If over valid inventory, add to it.
        if (currentDropTarget != null)
        {
            if (currentDropTarget.dropType == InventoryDropTarget.DropTargetType.InventoryPanel) {
                currentDropTarget.panelOwner.InventoryData.AddInventoryItem(type);
                LeaveTarget(currentDropTarget);
            }
            else if (currentDropTarget.dropType == InventoryDropTarget.DropTargetType.OrderPanel) {
                Debug.Log("dropped over order panel");
                currentDropTarget.orderPanel.order.preppedFoodItems.AddInventoryItem(type);
                Debug.Log(type.name);
                LeaveTarget(currentDropTarget);
            }
            
        }
        else
        {
            originalOwner.AddInventoryItem(type);
        }
        Destroy(gameObject);
    }
    #endregion
}
