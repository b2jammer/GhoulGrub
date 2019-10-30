using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FillOrderDropTarget : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public FillOrderFoodPanel panelOwner;

    public void OnPointerEnter(PointerEventData eventData) {
        FillOrderItemDraggable draggable;
        if (eventData.pointerDrag != null && (draggable = eventData.pointerDrag.GetComponent<FillOrderItemDraggable>()) != null) {
            draggable.ChangeTarget(this);
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        FillOrderItemDraggable draggable;
        if (eventData.pointerDrag != null && (draggable = eventData.pointerDrag.GetComponent<FillOrderItemDraggable>()) != null) {
            draggable.LeaveTarget(this);
        }
    }

    // Start is called before the first frame update
    private void Start() {
        panelOwner.dropTarget = this;
    }
}
