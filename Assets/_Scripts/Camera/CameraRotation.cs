using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public KeyboardInterfaceController controller;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RotateCamera();
    }

    void RotateCamera() {
        Debug.Log("Hi");
        Debug.Log(controller.Rotation);

        if (controller.Rotation == KeyboardInterfaceController.RotationDirection.Right) {
            transform.Rotate(new Vector3(0, 90, 0));
        }
        else if (controller.Rotation == KeyboardInterfaceController.RotationDirection.Left) {
            transform.Rotate(new Vector3(0, -90, 0));
        }
        
    }
}
