using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class FaceCamera : MonoBehaviour
{
    #region Private Variables
    private Camera mainCamera;
    private RectTransform rect;
    #endregion

    #region MonoBehaviour Methods
    private void Awake()
    {
        if (Camera.main != null)
        {
            mainCamera = Camera.main;   
        }
        if (GetComponent<RectTransform>() != null)
        {
            rect = GetComponent<RectTransform>();
        }
    }

    private void Update()
    {
        if (mainCamera != null)
        {
            Quaternion cameraFacing = mainCamera.transform.rotation;
            if (rect != null || (rect = GetComponent<RectTransform>()) != null)
            {
                rect.rotation = cameraFacing;
            }
            else
            {
                transform.rotation = cameraFacing;
            }
        }
        else
        {
            mainCamera = Camera.main;
        }
    }
    #endregion
}
