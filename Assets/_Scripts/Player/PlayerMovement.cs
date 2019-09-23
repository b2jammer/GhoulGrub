using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Public Variables
    [Header("Controllers/References")]
    [Tooltip("The level's keyboard controller")]
    public KeyboardInterfaceController keyboardInput;

    [Header("Movement Variables")]
    [Tooltip("How fast, in unity units per second, the player can move")]
    public float speed = 5f;
    #endregion

    #region Private Variables
    private Vector3 direction;
    #endregion

    #region MonoBehavior Methods
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
        // get the direction from the keyboard controller
        direction = keyboardInput.Direction;

        Move();
    }
    #endregion

    #region Script Specific Methods
    /// <summary>
    /// Moves the player in the given direction at the given speed.
    /// </summary>
    private void Move() {
        transform.position += direction * speed * Time.deltaTime;
    }
    #endregion
}
