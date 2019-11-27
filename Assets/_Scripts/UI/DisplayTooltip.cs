using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DisplayTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string tooltip;
    private GrubTooltipManager tooltipManager;

    private void Start() {
        tooltipManager = GrubTooltipManager.Instance;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        tooltipManager.ShowTooltip(tooltip);
    }

    public void OnPointerExit(PointerEventData eventData) {
        tooltipManager.HideTooltip();
    }
}
