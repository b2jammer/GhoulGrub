using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(EventTrigger))]
public class FillOrderItemDragPoint : MonoBehaviour
{
    #region Private Variables
    [SerializeField]
    private FillOrderFoodItemPanel owner;
    [SerializeField]
    private FillOrderItemDraggable dragPrefab;

    private RectTransform rt;
    private EventTrigger trigger;
    #endregion

    #region MonoBehaviour Methods
    private void Awake() {
        rt = GetComponent<RectTransform>();
        trigger = GetComponent<EventTrigger>();

        //Add listener for SnapToMouse
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.Drag;
        entry.callback.AddListener((data) => { CreateDraggable((PointerEventData)data); });
        trigger.triggers.Add(entry);
    }
    #endregion

    #region Script-Specific Methdds
    public void CreateDraggable(PointerEventData eventData) {
        FillOrderItemDraggable dragObj =
            Instantiate(dragPrefab.gameObject, transform.position, transform.rotation, transform.GetComponentInParent<Canvas>().transform)
            .GetComponent<FillOrderItemDraggable>();
        dragObj.originalOwner = owner.foodPanelContainer.CurrentOrder;
        dragObj.mouseOffset = (Vector3)eventData.position - rt.position;
        dragObj.type = owner.GetMealType();
        dragObj.UpdateLabel();
        dragObj.ChangeTarget(owner.foodPanelContainer.dropTarget);
        eventData.pointerDrag = dragObj.gameObject;
        owner.foodPanelContainer.CurrentOrder.RemovePreppedItem(owner.GetMealType());
    }
    #endregion
}
