using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryDropTarget : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public InventoryPanel panelOwner;

    public void OnPointerEnter(PointerEventData eventData)
    {
        FoodItemDraggable draggable;
        if (eventData.pointerDrag != null && (draggable = eventData.pointerDrag.GetComponent<FoodItemDraggable>()) != null)
        {
            draggable.ChangeTarget(this);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        FoodItemDraggable draggable;
        if (eventData.pointerDrag != null && (draggable = eventData.pointerDrag.GetComponent<FoodItemDraggable>()) != null)
        {
            draggable.LeaveTarget(this);
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        panelOwner.dropTarget = this;
    }
}
