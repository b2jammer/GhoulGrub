using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrubTooltipManager : MonoBehaviour
{
    [SerializeField]
    private GameObject tooltipObject;

    private Camera cam;
    private Text tooltipText;
    private RectTransform tooltipRect;
    private Vector3 mouseOffset;
    private static GrubTooltipManager _instance;

    public static GrubTooltipManager Instance {
        get {
            if (_instance == null) {
                _instance = GameObject.FindObjectOfType<GrubTooltipManager>();

                if (_instance == null) {
                    GameObject temp = new GameObject();
                    _instance = temp.AddComponent<GrubTooltipManager>();
                }

                return _instance;
            }
            else {
                return _instance;
            }
        }
    }

    private void Awake() {
        
    }

    private void Start() {
        tooltipText = tooltipObject.GetComponentInChildren<Text>();
        tooltipRect = tooltipText.GetComponent<RectTransform>();

        cam = Camera.main;

        mouseOffset = new Vector3(0, tooltipRect.rect.height / 2 + 10, 0);

        HideTooltip();
    }

    private void Update() {
        //Vector2 tooltipPos = RectTransformUtility.WorldToScreenPoint(cam, Input.mousePosition);
        tooltipObject.transform.position = Input.mousePosition + mouseOffset;
        //Debug.Log(tooltipPos);
    }

    public void ShowTooltip(string tooltip) {
        tooltipObject.SetActive(true);
        tooltipText.text = tooltip;
    }

    public void HideTooltip() {
        tooltipObject.SetActive(false);
    }
}
