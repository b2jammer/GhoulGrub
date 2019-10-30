using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FillOrderItemDraggable : MonoBehaviour, IDragHandler, IEndDragHandler {
    
    #region Public Variables
    [HideInInspector]
    public Order originalOwner;
    [HideInInspector]
    public MealItem type;
    [HideInInspector]
    public Vector3 mouseOffset;
    #endregion

    #region Private Variables
    private bool dragging;

    [SerializeField]
    private Image icon;

    FillOrderDropTarget currentDropTarget;
    private RectTransform rt;
    #endregion

    #region MonoBehaviour Methods
    private void Awake() {
        rt = GetComponent<RectTransform>();
        dragging = true;
    }
    #endregion

    #region Interface Methods
    public void OnDrag(PointerEventData eventData) {
        if (dragging) {
            rt.position = (Vector3)eventData.position - mouseOffset;
        }
    }
    #endregion

    #region Script-Specific Methods
    public void UpdateLabel() {
        icon.sprite = type.sprite;
    }

    public void ChangeTarget(FillOrderDropTarget newTarget) {
        //Leave current target
        LeaveTarget(currentDropTarget);
        //Enter new target
        currentDropTarget = newTarget;
        if (currentDropTarget != null) {

        }
    }

    public void LeaveTarget(FillOrderDropTarget tryTarget) {
        if (tryTarget == currentDropTarget) {
            if (currentDropTarget != null) {

            }
            currentDropTarget = null;
        }
    }

    public void OnEndDrag(PointerEventData eventData) {
        //If over valid inventory, add to it.
        if (currentDropTarget != null) {
            currentDropTarget.panelOwner.CurrentOrder.AddPreppedItem(type);
            LeaveTarget(currentDropTarget);
        }
        else {
            originalOwner.AddPreppedItem(type);
        }
        Destroy(gameObject);
    }
    #endregion
}
