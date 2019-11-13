using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class FoodItemDragPoint : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    #region Private Variables
    [SerializeField]
    private FoodItemPanel owner;
    [SerializeField]
    private FoodItemDraggable dragPrefab;

    private RectTransform rt;
    #endregion

    #region MonoBehaviour Methods
    private void Awake()
    {
        rt = GetComponent<RectTransform>();
    }
    #endregion

    #region Script-Specific Methdds
    public void CreateDraggable(PointerEventData eventData)
    {
        FoodItemDraggable dragObj = 
            Instantiate(dragPrefab.gameObject, transform.position, transform.rotation, transform.GetComponentInParent<Canvas>().transform)
            .GetComponent<FoodItemDraggable>();
        dragObj.originalOwner = owner.inventoryPanel.InventoryData;
        dragObj.type = owner.GetFoodType();
        dragObj.UpdateLabel();
        dragObj.ChangeTarget(owner.inventoryPanel.dropTarget);
        eventData.pointerDrag = dragObj.gameObject;
        owner.inventoryPanel.InventoryData.RemoveInventoryItem(owner.GetFoodType());
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        CreateDraggable(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        CreateDraggable(eventData);
    }
    #endregion
}
