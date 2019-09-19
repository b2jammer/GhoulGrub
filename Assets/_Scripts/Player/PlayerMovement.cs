using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public KeyboardInterfaceController keyboardInput;

    public float speed = 5f;

    private Vector3 direction;

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
        direction = keyboardInput.Direction;
        Move();
    }

    private void Move() {
        transform.position += direction * speed * Time.deltaTime;
    }
}
