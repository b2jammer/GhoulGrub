using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(EventTrigger))]
public class FoodItemDragPoint : MonoBehaviour
{
    #region Private Variables
    [SerializeField]
    private FoodItemPanel owner;
    [SerializeField]
    private FoodItemDraggable dragPrefab;

    private RectTransform rt;
    private EventTrigger trigger;
    #endregion

    #region MonoBehaviour Methods
    private void Awake()
    {
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
    public void CreateDraggable(PointerEventData eventData)
    {
        FoodItemDraggable dragObj = 
            Instantiate(dragPrefab.gameObject, transform.position, transform.rotation, transform.GetComponentInParent<Canvas>().transform)
            .GetComponent<FoodItemDraggable>();
        dragObj.originalOwner = owner.inventoryPanel.InventoryData;
        dragObj.mouseOffset = (Vector3)eventData.position - rt.position;
        dragObj.type = owner.GetFoodType();
        dragObj.UpdateLabel();
        dragObj.ChangeTarget(owner.inventoryPanel.dropTarget);
        eventData.pointerDrag = dragObj.gameObject;
        owner.inventoryPanel.InventoryData.RemoveInventoryItem(owner.GetFoodType());
    }
    #endregion
}
