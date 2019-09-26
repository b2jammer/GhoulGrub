using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    #region Public Variables
    [Header("Controllers/References")]
    [Tooltip("The level's keyboard controller")]
    public KeyboardInterfaceController keyboardInput;

    [Tooltip("Layer that all interactable objects are on")]
    public LayerMask interactableLayer;

    [Header("Interaction Variables")]
    [Tooltip("How close the player needs to be before being able to interact with an object")]
    public float interactionRange = 1f;
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
    }
    #endregion

    #region Script Specific Methods
    /// <summary>
    /// Checks whether an object in the interactable layer is within interaction range and if there is, 
    /// it runs this objects Interact() function.
    /// </summary>
    private void CheckInteraction() {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit, interactionRange, interactableLayer)) {
            // TODO: perform an action when an interactable object is within range - most likely grab a script and run a
            // function inside this script (probably the InteractionBehavior)
        }
        
    }
    #endregion
}
