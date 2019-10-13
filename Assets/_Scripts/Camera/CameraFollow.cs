using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform;

    [Range(0.01f, 1f)]
    public float cameraSmoothness = 0.5f;

    private Vector3 cameraOffset;

    private void Awake() {
        cameraOffset = transform.position - playerTransform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 newPosition = cameraOffset + playerTransform.position;

        transform.position = Vector3.Slerp(transform.position, newPosition, cameraSmoothness);
    }
}
