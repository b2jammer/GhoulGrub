using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    #region Public Variables
    [Header("Controllers/References")]
    [Tooltip("The level's keyboard controller")]
    public KeyboardInterfaceController keyboardInput;

    public InteractAgent myInteractAgent;


    //[Tooltip("Layer that all interactable objects are on")]
    //public LayerMask interactableLayer;
    #endregion

    #region Private Variables
    private bool isInteracting;
    private bool isOpeningStationInventory;
    #endregion


    #region MonoBehavior Methods
    private void Awake() {
        isInteracting = false;
        isOpeningStationInventory = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isInteracting = keyboardInput.IsInteracting;

        if (isInteracting) {
            myInteractAgent.Interact();
        }
    }
    #endregion

    #region Script Specific Methods
    
    #endregion
}
