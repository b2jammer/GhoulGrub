using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public KeyboardInterfaceController controller;
    public Transform playerTransform;

    private Vector3 cameraOffset;
    
    // Start is called before the first frame update
    void Start()
    {
        cameraOffset = transform.position - playerTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        RotateCamera();
    }

    void RotateCamera() {

        
        float rotationAmount;

        if (controller.Rotation == KeyboardInterfaceController.RotationDirection.Right) {
            rotationAmount = -90f;
        }
        else if (controller.Rotation == KeyboardInterfaceController.RotationDirection.Left) {
            rotationAmount = 90f;
        }
        else {
            rotationAmount = 0f;
        }

        Quaternion rotation = Quaternion.AngleAxis(rotationAmount, Vector3.up);
        cameraOffset = rotation * cameraOffset;

        transform.position = playerTransform.position + cameraOffset;
        transform.LookAt(playerTransform);
        
    }
}
