using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DisplayTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [TextArea]
    public string tooltip;

    public float maxTooltipTime = 2f;

    private GrubTooltipManager tooltipManager;
    private float currentTooltipTime;
    private bool mouserOver;
    private bool tooltipOn;

    private void Awake() {
        currentTooltipTime = 0f;
        mouserOver = false;
        tooltipOn = false;
    }

    private void Start() {
        tooltipManager = GrubTooltipManager.Instance;
    }

    private void Update() {
        if (mouserOver) {
            if (tooltipOn) {
                currentTooltipTime += Time.deltaTime;
                if (currentTooltipTime > maxTooltipTime) {
                    tooltipManager.HideTooltip();
                    tooltipOn = false;
                }
            }
            
        }
    }

    public void OnPointerEnter(PointerEventData eventData) {
        tooltipManager.ShowTooltip(tooltip);
        mouserOver = true;
        tooltipOn = true;
    }

    public void OnPointerExit(PointerEventData eventData) {
        tooltipManager.HideTooltip();
        mouserOver = false;
        tooltipOn = false;
        currentTooltipTime = 0f;
    }
}
