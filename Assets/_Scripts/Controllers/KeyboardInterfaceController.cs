using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInterfaceController : MonoBehaviour
{
    #region Private Variables
    private Vector3 direction;
    private bool isInteracting;
    private bool isOpeningStation;
    #endregion

    #region Properties
    /// <summary>
    /// Returns the direction the player is moving
    /// </summary>
    public Vector3 Direction {
        get {
            return direction;
        }
    }

    /// <summary>
    /// Returns whether or not the player pressed the key bound to 'Interact'
    /// </summary>
    public bool IsInteracting {
        get {
            return isInteracting;
        }
    }

    /// <summary>
    /// Returns whether or not the player pressed the key bound to 'OpenStationInventory'
    /// </summary>
    public bool IsOpeningStation {
        get {
            return isOpeningStation;
        }
    }
    #endregion

    #region MonoBehavior Methods
    private void Awake() {
        direction = Vector3.zero;
        isInteracting = false;
        isOpeningStation = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckMovement();
        CheckInteract();
        CheckOpenStationInventory();
    }
    #endregion

    #region Script Specific Methods
    /// <summary>
    /// Checks whether the keys bound to player movement are pressed and sets a
    /// direction Vector based on these keys. This vector is normalized.
    /// </summary>
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

    /// <summary>
    /// Checks whether the key bound to player interaction was pressed down
    /// and sets a boolean.
    /// </summary>
    private void CheckInteract() {
        isInteracting = Input.GetButtonDown("Interact");
    }

    /// <summary>
    /// Checks whether the key bound to the player opening a station inventory
    /// was pressed down and sets a boolean
    /// </summary>
    private void CheckOpenStationInventory() {
        isOpeningStation = Input.GetButtonDown("OpenStationInventory");
    }
    #endregion
}
