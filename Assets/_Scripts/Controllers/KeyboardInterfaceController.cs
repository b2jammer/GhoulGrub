using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInterfaceController : MonoBehaviour
{
    private Vector3 direction;

    public Vector3 Direction {
        get {
            return direction;
        }
    }

    private void Awake() {
        direction = Vector3.zero;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckMovement();
    }

    private void CheckMovement() {
        if (Input.GetButton("Horizontal")) {
            direction.x = Input.GetAxisRaw("Horizontal");
        }
        else {
            direction.x = 0;
        }

        if (Input.GetButton("Forward")) {
            direction.z = Input.GetAxisRaw("Forward");
        }
        else {
            direction.z = 0;
        }

        direction.Normalize();

    }
}
