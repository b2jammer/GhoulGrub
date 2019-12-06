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
    private float standardHeight;

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
        standardHeight = 1080;   
    }

    private void Start() {
        tooltipText = tooltipObject.GetComponentInChildren<Text>();
        tooltipRect = tooltipObject.GetComponent<Image>().rectTransform;

        cam = Camera.main;

        mouseOffset = new Vector3(0, tooltipRect.rect.height / 2 + 10, 0);

        HideTooltip();
    }

    private void Update() {
        float heightScaler = Screen.height/standardHeight;
        float widthOffset = 0f;

        if (Input.mousePosition.x - tooltipRect.rect.width/2f < 0) {
            widthOffset = tooltipRect.rect.width / 2f;
        }
        else if (Input.mousePosition.x + tooltipRect.rect.width / 2f > Screen.currentResolution.width) {
            widthOffset = -tooltipRect.rect.width / 2f;
        }
        else {
            widthOffset = 0f;
        }

        mouseOffset = new Vector3(widthOffset, (tooltipRect.rect.height / 2 + 10) * heightScaler, 0);
        tooltipObject.transform.position = Input.mousePosition + mouseOffset;
    }

    public void ShowTooltip(string tooltip) {
        tooltipObject.SetActive(true);
        tooltipText.text = tooltip;
    }

    public void HideTooltip() {
        tooltipObject.SetActive(false);
    }
}
